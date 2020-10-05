using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainScript : MonoBehaviour
{
    public void TryAgain()
    {
        GameManager.instance.LoadMainMenu();
    }
}
