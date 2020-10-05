using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private const int ALERT_TIME = 1;
    public const int SIEGE_ALERT_TIME = 1;

    [Header("UIs")]
    [SerializeField] private CanvasGroup mapUI;
    [SerializeField] private GameObject rebellionUI;
    [SerializeField] private RandomEventPopup randomEventPopup;
    
    [Header("Info UI")]
    [SerializeField] private CurrencyInfo m_HappinessInfo;
    [SerializeField] private CurrencyInfo m_MoneyInfo;
    [SerializeField] private CurrencyInfo m_FoodInfo;
    [SerializeField] private CurrencyInfo m_SoldiersInfo;

    [Header("Map UI")]
    [SerializeField] private TMP_Text m_DynastyText;
    [SerializeField] private TMP_Text m_TerritoryText;
    [SerializeField] private TMP_Text m_FarmText;
    [SerializeField] private TMP_Text m_LabText;
    [SerializeField] private EmpireControl m_feedControl;
    [SerializeField] private EmpireControl m_investControl;
    [SerializeField] private TMP_Text m_alertText;
    [SerializeField] private TMP_Text m_noticeText;

    [Header("Map Manager")]
    [SerializeField] private MapManager m_MapManager;

    private RebellionManager m_rebellion;
    private Animator animator;



    void Start(){
        animator = GetComponent<Animator>();
        m_rebellion = GetComponent<RebellionManager>();
        UpdateAll();
    }

    public void UpdateAll(){
        UpdateDynasty();
        UpdateHappinessCount();
        UpdateMoneyCount();
        UpdateFoodCount();
        UpdateSoldiersCount();
        UpdateTerritoryCount();
        UpdateFarmCount();
        UpdateLabCount();
    }
    

    #region Map UI

    public void UpdateDynasty(){
        if(GameManager.instance.state.m_currentDynasty != null) m_DynastyText.text = string.Format("<b>The <i>{0}</i> Dynasty<b>", GameManager.instance.state.m_currentDynasty.name);
    }

    public void UpdateHappinessCount() {
        m_HappinessInfo.UpdateCount(GameManager.instance.state.m_Happiness);
    }

    public void UpdateMoneyCount() {
        m_MoneyInfo.UpdateCount(GameManager.instance.state.m_Money);
        m_investControl.UpdateText();
    }

    public void UpdateFoodCount() {
        m_FoodInfo.UpdateCount(GameManager.instance.state.m_Food);
        m_feedControl.UpdateText();
    }

    public void UpdateSoldiersCount() {
        m_SoldiersInfo.UpdateCount(GameManager.instance.state.m_Soldiers);
    }

    public void UpdateTerritoryCount() {
        m_TerritoryText.text = string.Format("Territories Owned: {0}", Faction.GetPlayer().TerritoryCount());
    }

    public void UpdateFarmCount() {
        m_FarmText.text = string.Format("Farms Owned: {0}", Faction.GetPlayer().FarmCount());
    }

    public void UpdateLabCount() {
        m_LabText.text = string.Format("Labs Owned: {0}", Faction.GetPlayer().LabCount());

    }

    public void UpdateRebellion(int foodLost, int soldiersLost, int territoriesLost){
        m_rebellion.UpdateText(foodLost, soldiersLost, territoriesLost);
    }
    
    #endregion Map UI
    
    public void ShowMapUI(bool state, float transitionTime = 0){
        mapUI.interactable = state;
        mapUI.blocksRaycasts = state;
        mapUI.DOFade(state ? 1 : 0, transitionTime);
    }

    public void ShowRebellionUI(bool state){
        rebellionUI.SetActive(state);
    }

    private void NextRound(){
        TileController.DefaultMode();
        UpdateAll();
        GameManager.instance.state.m_turn++;
        ShowMapUI(true, .5f);
    }

    public void PlayRound(){
        TileController.ClearAllExpansionOptions();
        TileController.ClearTilePopup();
        TileController.ClearSelectedTile();
        TileController.OffMode();
        
        if(m_feedControl.AbleToPay() && m_investControl.AbleToPay()){
            ShowMapUI(false, .5f);
            StartCoroutine(PlayRoundCoroutine());
        }
        else{
            Notice("Invalid Empire Control!");
        }
    }

    IEnumerator PlayRoundCoroutine(){
        GameState state = GameManager.instance.state;

        yield return new WaitForSeconds(.5f);

        m_feedControl.Pay();
        UpdateFoodCount();
        UpdateHappinessCount();

        Alert("Feeding the empire...");
        yield return new WaitForSeconds(ALERT_TIME);

        m_investControl.Pay();
        UpdateMoneyCount();
        UpdateHappinessCount();

        Alert("Managing investments...");
        yield return new WaitForSeconds(ALERT_TIME);

        int taxes = m_MapManager.CollectTaxes();
        state.m_Money += taxes;
        UpdateMoneyCount();

        Alert("Collecting taxes...");
        yield return new WaitForSeconds(ALERT_TIME);

        int harvest = m_MapManager.HarvestFarms();
        if(harvest > 0){
            state.m_Food += harvest;
            UpdateFoodCount();

            Alert("Harvesting farms...");
            yield return new WaitForSeconds(ALERT_TIME);
        }

        int mined = m_MapManager.Mine();
        if(mined > 0){
            state.m_Money += mined;
            UpdateMoneyCount();

            Alert("Carving jade...");
            yield return new WaitForSeconds(ALERT_TIME);
        }

        yield return StartCoroutine(TileController.ExpandTiles());

        if(state.enemyFactions.Count > 0){
            foreach(Faction faction in state.enemyFactions){
                faction.AddSoldier();
            }
            Alert("The enemies have expanded their forces!");
            yield return new WaitForSeconds(ALERT_TIME);

            foreach(Faction faction in state.enemyFactions){
                bool expand = Random.value < Constants.CHANCE_TO_EXPAND;
                if(expand){
                    // -1 = unable to siege, 0 = basic expansion, 1 = successful siege to player, 2 = successful siege to nonplayer, 3 = failed siege
                    int result = faction.MaybeSiege();
                    switch(result){
                        case 0:
                            Alert("The enemy has expanded their territory!", SIEGE_ALERT_TIME);
                            break;
                        case 1:
                            Alert("The enemy has taken some of your land!", SIEGE_ALERT_TIME);
                            state.m_Happiness +=  Constants.FAILED_INVADE_HAPPINESS;
                             state.m_Happiness = Mathf.Clamp(state.m_Happiness, 0, 100);
                            UpdateHappinessCount();
                            break;
                        case 2:
                            Alert("The enemies fought each other!", SIEGE_ALERT_TIME);
                            break;
                        case 3:
                            Alert("The enemies failed to expand their land...", SIEGE_ALERT_TIME);
                            break;
                    }
                    if(result != -1) yield return new WaitForSeconds(SIEGE_ALERT_TIME);
                }
            }
        }

        //Random Events
        if(Random.value < Constants.CHANCE_RANDOM_EVENT){
            RandomEvents.RandomEvent randomEvent = RandomEvents.GetEvent();
            yield return StartCoroutine(EventPopup(randomEvent.message));
            randomEvent.eventFunc();
            Alert(randomEvent.alert);
            yield return new WaitForSeconds(ALERT_TIME);
        }

        if(Faction.GetPlayer().TerritoryCount() == 0){
            GameManager.instance.LoadLose();
        }
        else if(GameManager.instance.state.m_Happiness == 0){
            StartRebellion();
        }
        else if(GameManager.instance.state.m_Happiness == 100){
            GameManager.instance.LoadWin();
        }

        NextRound();

    }

    IEnumerator EventPopup(string message){
        randomEventPopup.Show(message);
        while(randomEventPopup.active){
            yield return null;
        }
    }

    public void StartRebellion(){
        animator.SetTrigger("rebellion");
    }

    //Animation Event
    public void SwitchToRebellion(){
        m_MapManager.StartRebellion();

        ShowMapUI(false);
        ShowRebellionUI(true);
        m_MapManager.SetRebelling(true);
    }

    public void EndRebellion(){
        //check for territories, if not satisfied return
        if (TileController.GetAbandonedTilesCount() < MapManager.GetAbandonCount()) {
            Notice("You haven't abandoned enough tiles!");
            return;
        }
        TileController.AbandonTiles();
        TileController.SetMode(Constants.DEFAULT_MODE);

        if(Faction.GetPlayer().TerritoryCount() == 0){
            GameManager.instance.LoadLose();
        }

        ShowRebellionUI(false);
        m_MapManager.EndDynasty();
    }

    public void Alert(string alert, float alertTime = ALERT_TIME){
       
        m_alertText.text = alert;
        m_alertText.DOKill(true);
        m_alertText.transform.DOKill(true);
        m_alertText.rectTransform.DOKill(true);
        m_alertText.DOFade(1, alertTime/2).OnComplete(() => m_alertText.DOFade(0, alertTime/2));
        m_alertText.rectTransform.DOAnchorPosY(100, alertTime).OnComplete(() => m_alertText.rectTransform.anchoredPosition = Vector2.zero);
    }

    public void Notice(string notice){
        m_noticeText.text = notice;
        m_noticeText.DOKill();
        m_noticeText.DOFade(1, .1f).OnComplete(() => m_noticeText.DOFade(0, 1f).SetDelay(.2f));
    }
}
