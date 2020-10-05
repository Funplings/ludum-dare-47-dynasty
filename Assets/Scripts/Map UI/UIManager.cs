using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private CanvasGroup mapUI;
    [SerializeField] private GameObject rebellionUI;
    
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
        m_DynastyText.text = string.Format("<b>The <i>{0}</i> Dynasty<b>", GameManager.instance.state.m_currentDynasty.name);
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
        mapUI.DOFade(state ? 1 : 0, transitionTime);
    }

    public void ShowRebellionUI(bool state){
        rebellionUI.SetActive(state);
    }

    private void NextRound(){
        GameManager.instance.state.m_turn++;
        ShowMapUI(true, .5f);
    }

    public void PlayRound(){
        if(m_feedControl.AbleToPay() && m_investControl.AbleToPay()){
            ShowMapUI(false, .5f);
            StartCoroutine(PlayRoundCoroutine());
        }
        else{
            Notice("Invalid Empire Control!");
        }
    }

    IEnumerator PlayRoundCoroutine(){

        yield return new WaitForSeconds(.5f);

        m_feedControl.Pay();
        m_investControl.Pay();
        UpdateFoodCount();
        UpdateMoneyCount();
        UpdateHappinessCount();

        TileController.ExpandTiles();

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

    public void Alert(string alert){
        m_alertText.text = alert;
        m_alertText.DOComplete();
        m_alertText.transform.DOComplete();
        m_alertText.DOFade(1, .5f).OnComplete(() => m_alertText.DOFade(0, .5f));
        m_alertText.rectTransform.DOAnchorPosY(100, 1).OnComplete(() => m_alertText.rectTransform.anchoredPosition = Vector2.zero);
    }

    public void Notice(string notice){
        m_noticeText.text = notice;
        m_noticeText.DOKill();
        m_noticeText.DOFade(1, .1f).OnComplete(() => m_noticeText.DOFade(0, 1f).SetDelay(.2f));
    }
}
