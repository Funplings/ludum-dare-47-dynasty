using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void DisplayDynastyStats()
    {
        List<string> DynastyStats = new List<string>(); 
    }
}
