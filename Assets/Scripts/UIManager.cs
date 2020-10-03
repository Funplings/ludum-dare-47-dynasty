using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject m_HappinessCount;
    [SerializeField] GameObject m_MoneyCount;
    [SerializeField] GameObject m_FoodCount;
    [SerializeField] GameObject m_SoldiersCount;
    public void UpdateHappinessCount(int happiness) {
        m_HappinessCount.GetComponent<Text>().text = "Happiness: " + happiness.ToString();
    }

    public void UpdateMoneyCount(int money) {
        m_MoneyCount.GetComponent<Text>().text = "Money: " + money.ToString();
    }

    public void UpdateFoodCount(int food) {
        m_FoodCount.GetComponent<Text>().text = "Food: " + food.ToString();
    }

    public void UpdateSoldiersCount(int soldiers) {
        m_SoldiersCount.GetComponent<Text>().text = "Soldiers: " + soldiers.ToString();
    }
}
