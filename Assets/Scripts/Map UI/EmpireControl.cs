using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmpireControl : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Color overColor;
    private int costPerCount = 1;
    private int count = 0;
    private int max = 0;
    private int maxCost = 10;

    public void UpdateText(){
        counterText.text = string.Format("{0}/{1}", count, max);

        int result = costPerCount * count;
        resultText.text = string.Format("{0}x", result);
        resultText.color = result > maxCost ? overColor : Color.black;
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
