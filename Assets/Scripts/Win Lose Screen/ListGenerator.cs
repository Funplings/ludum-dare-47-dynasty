using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListGenerator : MonoBehaviour
{
    public string[] names;
    public int[] durations;
    public GameObject DynastyInfoTemplate;
    public void StoreDynastyData()
    {
        int length = GameManager.instance.state.m_allDynasties.Count;
        names = new string[length]; 
        durations = new int[length];
        for (int i = 0; i < length; i++)
        {
            names[i] = GameManager.instance.state.m_allDynasties[i].GetName();
            durations[i] = GameManager.instance.state.m_allDynasties[i].GetDuration();
        }
    }

    public void CreateDynastyObj()
    {
        for(int i = 0; i < names.Length; i++)
        {
            GameObject DynastyInfo = Instantiate(DynastyInfoTemplate) as GameObject;
            DynastyInfo.SetActive(true);
            DynastyInfo.GetComponent<DynastyListElement>().SetText("Dynasty Name: " + names[i] + "\n" + "Turns Lasted: " + durations[i]);
            DynastyInfo.transform.SetParent(DynastyInfoTemplate.transform.parent, false);
        }
    }
    public void CreateTestDynasties()
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
        //CreateTestDynasties();
        StoreDynastyData();
        CreateDynastyObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
