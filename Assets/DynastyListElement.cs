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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void testbuttonstuff()
    {
        ChangingText.GetComponent<Text>().text = GenerateText();
    }   
    public void StoreDynastyData()
    {
        int length = GameManager.instance.state.allDynasties.Count;
        names = new string[length];
        durations = new int[length];
        for(int i = 0; i < length; i++)
        {
            names[i] = GameManager.instance.state.allDynasties[i].GetName();
            durations[i] = GameManager.instance.state.allDynasties[i].GetDuration();
        }
    }

    public void MakeTestDynasties()
    {
        Dynasty newtestdynasty = new Dynasty("jerry", 0, 3);
        GameManager.instance.state.allDynasties.Add(newtestdynasty);
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
