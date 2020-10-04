using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Constants.ON_SALE itemType;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private int cost;
    
    private MapManager mapManager;

    void Start(){
        costText.text = cost.ToString();
    }

    public void Buy(){
        if(mapManager == null){
            mapManager = FindObjectOfType<MapManager>();
        }
        mapManager.Buy(itemType, cost);
    }
}
