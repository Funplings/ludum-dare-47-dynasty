using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_HappinessCount;
    [SerializeField] TMP_Text m_MoneyCount;
    [SerializeField] TMP_Text m_FoodCount;
    [SerializeField] TMP_Text m_SoldiersCount;
    
    public void UpdateHappinessCount(int happiness) {
        m_HappinessCount.text = happiness.ToString();
    }

    public void UpdateMoneyCount(int money) {
        m_MoneyCount.text = money.ToString();
    }

    public void UpdateFoodCount(int food) {
        m_FoodCount.text = food.ToString();
    }

    public void UpdateSoldiersCount(int soldiers) {
        m_SoldiersCount.text = soldiers.ToString();
    }
}
