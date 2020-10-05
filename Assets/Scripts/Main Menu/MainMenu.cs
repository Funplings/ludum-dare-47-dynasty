using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public float r;
    public float g;
    public float b;

    private Color _colorSelected;
    [SerializeField] private Image image;
    [SerializeField] private RectTransform play;
    [SerializeField] private RectTransform tutorial;
    [SerializeField] private RectTransform colorPickerGroup;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text nameWarning;

    void Start()
    {
        this.r = 0;
        this.g = 0;
        this.b = 0;
        image.color = new Color(r, g, b);

        tutorial.DOAnchorPosX(500, 1f).From();
        play.DOAnchorPosX(500, 1f).From();
        colorPickerGroup.DOAnchorPosX(-500, 1f).From();
    }

    private void KillTweens(){
        tutorial.DOKill();
        play.DOKill();
        colorPickerGroup.DOKill();
    }

    public void PlayGame() {
        if( String.IsNullOrWhiteSpace(input.text)){
            nameWarning.text = "Please enter an empire name!";
            nameWarning.DOKill();
            nameWarning.DOFade(1, .1f).OnComplete(() =>nameWarning.DOFade(0, 1f).SetDelay(.2f));
            return;
        }

        AudioManager.instance.Play("GameStart");

        KillTweens();
        GameManager.instance.state.m_EmpireName = input.text;
        Faction playerFaction = new Faction(image.color);
        playerFaction.SetAsPlayer();
        GameManager.instance.LoadGame();
        
    }
    
    public void PlayTutorial()
    {

        AudioManager.instance.Play("GameStart");
        KillTweens();
        GameManager.instance.LoadTutorial();
    }
    
    public void sliderCallbackR(float value)
    {
        r = value;
        image.color = new Color(r, g, b);
    }
    
    public void sliderCallbackG(float value)
    {
        g = value;
        image.color = new Color(r, g, b);
    }
    
    public void sliderCallbackB(float value)
    {
        b = value;
        image.color = new Color(r, g, b);
    }
    
}
