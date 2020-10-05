using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainScript : MonoBehaviour
{
    public void TryAgain()
    {
        AudioManager.instance.Play("GameStart");
        GameManager.instance.LoadMainMenu();
    }
}
