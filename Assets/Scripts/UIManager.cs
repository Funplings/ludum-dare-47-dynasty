using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] CurrencyInfo m_HappinessInfo;
    [SerializeField] CurrencyInfo m_MoneyInfo;
    [SerializeField] CurrencyInfo m_FoodInfo;
    [SerializeField] CurrencyInfo m_SoldiersInfo;
    [SerializeField] TMP_Text m_TerritoryText;
    [SerializeField] TMP_Text m_FarmText;
    [SerializeField] TMP_Text m_LabText;
    [SerializeField] EmpireControl feedControl;
    [SerializeField] EmpireControl investControl;
    
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
    
}
