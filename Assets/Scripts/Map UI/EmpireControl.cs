﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmpireControl : MonoBehaviour
{
    public enum CONTROL_TYPE {
        FEED,
        INVEST
    }

    [SerializeField] private CONTROL_TYPE controlType;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Color overColor;

    private int count = 0;
    private int max = 0;
    private MapManager mapManager;

    void Start(){
        max = GetMapManager().TerritoryCount();
        UpdateText();
    }

    private MapManager GetMapManager(){
        if(mapManager != null) return mapManager;
        mapManager = FindObjectOfType<MapManager>();
        return mapManager;
    }

    public void UpdateText(){
        counterText.text = string.Format("{0}/{1}", count, GetMapManager().TerritoryCount());

        resultText.text = string.Format("{0}x", TotalCost());
        resultText.color = AbleToPay() ?  Color.black : overColor;
    }
    
    public void Minus(){
        count = Mathf.Max(0, count - 1);
        UpdateText();
    }

    public void Plus(){
        count = Mathf.Min(max, count + 1);
        UpdateText();
    }

    public void UpdateMax(int newMax){
        max = newMax;
        UpdateText();
    }

    public void Pay(){
        if( !AbleToPay() ) return;

        GameState state = GameManager.instance.state;
        int complement = max - count;

        if(controlType == CONTROL_TYPE.FEED){
            state.m_Food -= TotalCost();
            state.m_Happiness += Constants.STARVING_HAPPINESS * complement + Constants.FED_HAPPINESS * count;
        }
        else{
            state.m_Money -= TotalCost();
            state.m_Happiness += Constants.UNINVESTED_HAPPINESS * complement + Constants.INVESTED_HAPPINESS * count;
        }
        count = 0;
        UpdateText();
    }

    public bool AbleToPay(){
        GameState state = GameManager.instance.state;

        if(controlType == CONTROL_TYPE.FEED){
            return TotalCost() <= state.m_Food;
        }
        else{
            return TotalCost() <= state.m_Money;
        }
    }

    private int CostPerCount(){
        if(controlType == CONTROL_TYPE.FEED){
            return GameManager.instance.state.GetFeedCost();
        }
        else{
            return Constants.INVEST_COST;
        }
    }

    private int TotalCost(){
        return CostPerCount() * count;
    }
}
