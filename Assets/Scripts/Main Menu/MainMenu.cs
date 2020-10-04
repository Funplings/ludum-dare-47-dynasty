using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public float r;
    public float g;
    public float b;

    private Color _colorSelected;
    private string _nationName;
    [SerializeField] private Image image;
    [SerializeField] private Button play;
    [SerializeField] private Button tutorial;
    [SerializeField] private TMP_InputField input;

    void Start()
    {
        this.r = 0;
        this.g = 0;
        this.b = 0;
        image.color = new Color(r, g, b);
        play.transform.localPosition = new Vector3(225, -100, 0);
        tutorial.transform.localPosition = new Vector3(200, -30, 0);
        input.onEndEdit.AddListener(SubmitName);
    }

    void Update()
    {
        float ppos = play.transform.localPosition[0];
        float tpos = tutorial.transform.localPosition[0];
        if (tpos > 115)
        {
            play.transform.localPosition = new Vector3(ppos - 1, -100, 0);
            tutorial.transform.localPosition = new Vector3(tpos - 1, -30, 0);
        } else if (ppos > 115)
        {
            play.transform.localPosition = new Vector3(ppos - 1, -100, 0);
        }

    }

    public void PlayGame() {
        GameManager.instance.LoadGame();
        _colorSelected = image.color;
        Debug.Log(_colorSelected);
        Debug.Log(_nationName);
    }
    
    public void PlayTutorial()
    {
        GameManager.instance.LoadTutorial();
        _colorSelected = image.color;
    }

    private void SubmitName(string text)
    {
        _nationName = text;
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
