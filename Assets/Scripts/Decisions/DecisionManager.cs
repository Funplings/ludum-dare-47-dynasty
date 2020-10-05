using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text labOwnedCount; 
    [SerializeField] private TMP_Text dynastyCount; 
    [SerializeField] private CanvasGroup showMapButton; 
    [SerializeField] private CanvasGroup mainGroup; 
    [SerializeField] private TMP_Text nameWarning; 
    [SerializeField] private TMP_Text empireName; 
    [SerializeField] private MapManager mapManager; 
    private CanvasGroup decisionCanvas;
    
    private HashSet<PerkChoice> perkChoices = new HashSet<PerkChoice>();
    private int labsCount = 0;

    void Start(){
        decisionCanvas = GetComponent<CanvasGroup>();
        empireName.text = String.Format("The <i>{0}</i> Empire", GameManager.instance.state.m_EmpireName);
        SetupDecision();
    }

    public void SetupDecision(){
        ShowSelf(true);
        GameState state = GameManager.instance.state;
                
        //update lab count
        labsCount = Faction.GetPlayer().LabCount();
        labOwnedCount.text = "Universities Owned (Perk Count): " + labsCount.ToString();

        //set dynasty number
        dynastyCount.text = "Dynasty " + (state.m_allDynasties.Count + 1).ToString();
    }

    public void ShowDecision(bool state){
        gameObject.SetActive(state);
    }

    public void PeekMap(bool state){
        mainGroup.alpha = state ? 0 : 1;
        showMapButton.alpha = state ? .5f : 1;
    }

    public void ShowSelf(bool state){
        float fadeTo = state ? 1 : 0;
        decisionCanvas.DOFade(fadeTo, .5f);
        decisionCanvas.interactable = state;
        decisionCanvas.blocksRaycasts = state;
    }

    public void BeginDynasty(){    
        GameState state = GameManager.instance.state;

        if( String.IsNullOrWhiteSpace(inputField.text)){
            nameWarning.text = "Please enter a dynasty name!";
            nameWarning.DOKill();
            nameWarning.DOFade(1, .1f).OnComplete(() =>nameWarning.DOFade(0, 1f).SetDelay(.2f));
            return;
        }
        else if(state.CheckForReusedName(inputField.text)) {
            nameWarning.text = "Please enter a new dynasty name!";
            nameWarning.DOKill();
            nameWarning.DOFade(1, .1f).OnComplete(() =>nameWarning.DOFade(0, 1f).SetDelay(.2f));
            return;
        }
        
        //create dynasty
        Dynasty newDynasty = new Dynasty(inputField.text, state.m_turn, 0);
        state.m_currentDynasty = newDynasty;
        state.m_allDynasties.Add(newDynasty);

        //Set up perks
        state.m_perks.Clear();
        foreach(PerkChoice choice in perkChoices){
            state.m_perks.Add(choice.perk);
            choice.Toggle();
        }
        perkChoices.Clear();

        inputField.text = "";

        //Hide self and tell map to start
        ShowSelf(false);
        mapManager.StartDynasty();
    }

    public void TogglePerk(PerkChoice choice){
        if(choice.status){
            choice.Toggle();
            perkChoices.Remove(choice);
        }
        else if(perkChoices.Count < labsCount){
            choice.Toggle();
            perkChoices.Add(choice);
        }
    }
}
