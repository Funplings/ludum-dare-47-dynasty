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
    [SerializeField] private MapManager mapManager;

    private RebellionManager m_rebellion;
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
        m_rebellion = GetComponent<RebellionManager>();
    }
    

    #region Map UI

    public void UpdateDynasty(){
        m_DynastyText.text = string.Format("<b>The <i>{0}</i> Dynasty<b>", GameManager.instance.state.m_currentDynasty.name);
    }

    public void UpdateHappinessCount(int happiness) {
        m_HappinessInfo.UpdateCount(happiness);
    }

    public void UpdateMoneyCount(int money) {
        m_MoneyInfo.UpdateCount(money);
    }

    public void UpdateFoodCount(int food) {
        m_FoodInfo.UpdateCount(food);
    }

    public void UpdateSoldiersCount(int soldiers) {
        m_SoldiersInfo.UpdateCount(soldiers);
    }

    public void UpdateTerritoryCount(int territories) {
        m_TerritoryText.text = string.Format("Territories Owned: {0}", territories);
    }

    public void UpdateFarmCount(int farms) {
        m_FarmText.text = string.Format("Farms Owned: {0}", farms);
    }

    public void UpdateLabCount(int labs) {
        m_LabText.text = string.Format("Labs Owned: {0}", labs);
    }
    
    #endregion Map UI
    
    public void ShowMapUI(bool state){
        mapUI.SetActive(state);
    }

    public void ShowRebellionUI(bool state){
        rebellionUI.SetActive(state);
    }

    public void PlayRound(){
        // Alert("Pizza");
        StartRebellion();
    }

    public void StartRebellion(){
        animator.SetTrigger("rebellion");
    }

    //Animation Event
    public void SwitchToRebellion(){
        ShowMapUI(false);
        ShowRebellionUI(true);
    }

    public void EndRebellion(){
        //check for territories, if not satisfied return
        ShowRebellionUI(false);
        mapManager.EndDynasty();
    }

    public void Alert(string alert){
        m_alertText.text = alert;
        m_alertText.DOComplete();
        m_alertText.transform.DOComplete();
        m_alertText.DOFade(1, .5f).OnComplete(() => m_alertText.DOFade(0, .5f));
        m_alertText.rectTransform.DOAnchorPosY(100, 1).OnComplete(() => m_alertText.rectTransform.anchoredPosition = Vector2.zero);
    }
}
