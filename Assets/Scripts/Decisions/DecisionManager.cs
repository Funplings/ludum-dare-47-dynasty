using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text labOwnedCount; 
    [SerializeField] private TMP_Text dynastyCount; 
    [SerializeField] private CanvasGroup showMapButton; 
    [SerializeField] private CanvasGroup mainGroup; 

    private int labsCount = 0;

    void Start(){
        SetupDecision();
    }

    public void SetupDecision(){
        GameState state = GameManager.instance.state;
        
        //update lab count
        // labsCount = map.labsCount();
        labOwnedCount.text = "Labs Owned (Perk Count): " + labsCount.ToString();

        //set dynasty number
        dynastyCount.text = "Dynasty " + (state.allDynasties.Count + 1).ToString();
    }

    public void ShowDecision(bool state){
        gameObject.SetActive(state);
    }

    public void ShowMap(bool state){
        mainGroup.alpha = state ? 0 : 1;
        showMapButton.alpha = state ? .5f : 1;
    }

    public void BeginDynasty(){        
        if( String.IsNullOrWhiteSpace(inputField.text) ) return;

        //create dynasty
        Dynasty newDynasty = new Dynasty(inputField.text, GameManager.instance.state.turn, 0);
        GameManager.instance.state.currentDynasty = newDynasty;
        GameManager.instance.state.allDynasties.Add(newDynasty);

        //Hide self and tell map to start
    }
}
