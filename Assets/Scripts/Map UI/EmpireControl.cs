using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmpireControl : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    private int count = 0;
    private int max = 0;

    public void UpdateText(){
        counterText.text = string.Format("{0}/{1}", count, max);
    }
    
    public void Minus(){
        count = Mathf.Max(0, count - 1);
        UpdateText();
    }

    public void Plus(){
        count = Mathf.Min(max, count + 1);
        UpdateText();
    }
}
