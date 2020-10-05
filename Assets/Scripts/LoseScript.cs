using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScript : MonoBehaviour
{
    public void Start()
    {
        foreach(Dynasty dynasty in GameManager.instance.state.m_allDynasties)
        {

        }
    }
    public void TryAgain()
    {
        GameManager.instance.LoadMainMenu();
    }
    public void DynastyInfo()
    {
        
    }
}
