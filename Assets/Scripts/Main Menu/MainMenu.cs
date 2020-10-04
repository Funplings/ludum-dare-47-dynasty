using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static Color selected;
    
    void Start()
    {
        
    }
    
    
    
    public void PlayGame() {
        GameManager.instance.LoadGame();
    }
    
}
