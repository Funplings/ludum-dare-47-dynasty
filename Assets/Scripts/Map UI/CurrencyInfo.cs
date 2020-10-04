using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void UpdateCount(int value){
        text.text = value.ToString();
    }
}
