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
}
