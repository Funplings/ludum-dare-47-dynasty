using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public float r;
    public float g;
    public float b;

    private Color _colorSelected;
    
    void Start()
    {
        this.r = 0;
        this.g = 0;
        this.b = 0;
    }

    public void PlayGame() {
        GameManager.instance.LoadGame();
    }


    public void sliderCallbackR(float value)
    {
        r = value;
    }
    
    public void sliderCallbackG(float value)
    {
        g = value;
    }
    
    public void sliderCallbackB(float value)
    {
        b = value;
    }
    
}
