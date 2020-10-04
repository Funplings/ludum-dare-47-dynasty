using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private GameObject mapUI;
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
    }

    public void UpdateFoodCount() {
        m_FoodInfo.UpdateCount(GameManager.instance.state.m_Food);
    }

    public void UpdateSoldiersCount() {
        m_SoldiersInfo.UpdateCount(GameManager.instance.state.m_Soldiers);
    }

    public void UpdateTerritoryCount() {
        m_TerritoryText.text = string.Format("Territories Owned: {0}", m_MapManager.TerritoryCount());
    }

    public void UpdateFarmCount() {
        m_FarmText.text = string.Format("Farms Owned: {0}", m_MapManager.FarmsCount());
    }

    public void UpdateLabCount() {
        m_LabText.text = string.Format("Labs Owned: {0}", m_MapManager.LabsCount());
    }
    
    #endregion Map UI
    
    public void ShowMapUI(bool state){
        mapUI.SetActive(state);
    }

    public void ShowRebellionUI(bool state){
        rebellionUI.SetActive(state);
    }

    public void PlayRound(){
        StartRebellion();
    }

    public void StartRebellion(){
        animator.SetTrigger("rebellion");
    }

    //Animation Event
    public void SwitchToRebellion(){
        ShowMapUI(false);
        ShowRebellionUI(true);
        m_MapManager.SetRebelling(true);
    }

    public void EndRebellion(){
        //check for territories, if not satisfied return
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
}
