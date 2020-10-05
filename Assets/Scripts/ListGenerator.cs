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
            DynastyInfo.GetComponent<DynastyListElement>().SetText("Dynasty Name:" + names[i]);
            DynastyInfo.transform.SetParent(DynastyInfoTemplate.transform.parent, false);
        }
    }

    void Start()
    {
        Dynasty newtestdynasty = new Dynasty("jerry", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty);
        Dynasty newtestdynasty1 = new Dynasty("imon", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty1);
        Dynasty newtestdynasty2 = new Dynasty("ansa", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty2);
        Dynasty newtestdynasty3 = new Dynasty("matthew", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty3);
        int length = GameManager.instance.state.m_allDynasties.Count;
        names = new string[length];
        durations = new int[length];
        for (int i = 0; i < length; i++)
        {
            names[i] = GameManager.instance.state.m_allDynasties[i].GetName();
            durations[i] = GameManager.instance.state.m_allDynasties[i].GetDuration();
        }
        for (int i = 0; i < names.Length; i++)
        {
            GameObject DynastyInfo = Instantiate(DynastyInfoTemplate) as GameObject;
            DynastyInfo.SetActive(true);
            DynastyInfo.GetComponent<DynastyListElement>().SetText("Dynasty Name:" + names[i]);
            DynastyInfo.transform.SetParent(DynastyInfoTemplate.transform.parent, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
