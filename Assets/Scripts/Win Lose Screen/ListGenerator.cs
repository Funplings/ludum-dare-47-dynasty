using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListGenerator : MonoBehaviour
{
    public GameObject DynastyInfoPrefab;
    public Transform listParent;

    private void CreateTestDynasties()
    {
        Dynasty newtestdynasty = new Dynasty("Jerry", 0, 7);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty);
        Dynasty newtestdynasty1 = new Dynasty("Imon", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty1);
        Dynasty newtestdynasty2 = new Dynasty("Ansa", 0, 5);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty2);
        Dynasty newtestdynasty3 = new Dynasty("Matthew", 0, 23);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty3);
        Dynasty newtestdynasty4 = new Dynasty("Aryaman", 3, 4);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty4);
        Dynasty newtestdynasty5 = new Dynasty("Krischan", 0, 7);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty5);
    }

    void Start()
    {
        CreateTestDynasties();
        for(int i = 0; i < GameManager.instance.state.m_allDynasties.Count; i++){
            Dynasty dynasty = GameManager.instance.state.m_allDynasties[i];
            GameObject obj = Instantiate(DynastyInfoPrefab, listParent);
            DynastyListElement element = obj.GetComponent<DynastyListElement>();
            element.SetInfo(i+1, dynasty.name, dynasty.turnStarted * Constants.YEARS_PER_TURN, dynasty.turnEnded * Constants.YEARS_PER_TURN);
        }
    }
}
