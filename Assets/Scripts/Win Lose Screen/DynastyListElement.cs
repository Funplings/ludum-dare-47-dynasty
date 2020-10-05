using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class DynastyListElement : MonoBehaviour
{
    
    public Text text;
    public void SetText(string info)
    {
        text.text = info;
    }
}
