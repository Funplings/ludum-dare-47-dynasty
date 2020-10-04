using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CurrencyInfo m_HappinessInfo;
    [SerializeField] private CurrencyInfo m_MoneyInfo;
    [SerializeField] private CurrencyInfo m_FoodInfo;
    [SerializeField] private CurrencyInfo m_SoldiersInfo;
    [SerializeField] private TMP_Text m_TerritoryText;
    [SerializeField] private TMP_Text m_FarmText;
    [SerializeField] private TMP_Text m_LabText;
    [SerializeField] private EmpireControl feedControl;
    [SerializeField] private EmpireControl investControl;
    [SerializeField] private RebellionManager rebellion;
    [SerializeField] private TMP_Text alertText;
    
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
    
    public void Hide(){
        gameObject.SetActive(false);
    }

    public void PlayRound(){
        Alert("Pizza");
        // rebellion.StartFade();
    }

    public void Alert(string alert){
        alertText.text = alert;
        alertText.DOComplete();
        alertText.transform.DOComplete();
        alertText.DOFade(1, .5f).OnComplete(() => alertText.DOFade(0, .5f));
        alertText.rectTransform.DOAnchorPosY(100, 1).OnComplete(() => alertText.rectTransform.anchoredPosition = Vector2.zero);
    }
}
