using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class DynastyListElement : MonoBehaviour
{
    public GameObject ChangingText;
    string[] names;
    int[] durations;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetText(string info)
    {
        text.text = info;
    }
    public void testbuttonstuff()
    {
        ChangingText.GetComponent<Text>().text = GenerateText();
    }   
    public void StoreDynastyData()
    {
        int length = GameManager.instance.state.m_allDynasties.Count;
        names = new string[length];
        durations = new int[length];
        for(int i = 0; i < length; i++)
        {
            names[i] = GameManager.instance.state.m_allDynasties[i].GetName();
            durations[i] = GameManager.instance.state.m_allDynasties[i].GetDuration();
        }
    }

    public void MakeTestDynasties()
    {
        Dynasty newtestdynasty = new Dynasty("jerry", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty);
        Dynasty newtestdynasty1 = new Dynasty("imon", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty1);
        Dynasty newtestdynasty2 = new Dynasty("ansa", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty2);
        Dynasty newtestdynasty3 = new Dynasty("matthew", 0, 3);
        GameManager.instance.state.m_allDynasties.Add(newtestdynasty3);

    }
    public string GenerateText()
    {
        string text = "";
        for(int i = 0; i < names.Length; i++)
        {
            text += names[i] + "\n"; 
        }
        return text;
    }
}
