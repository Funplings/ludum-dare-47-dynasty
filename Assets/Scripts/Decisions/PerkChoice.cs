using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkChoice : MonoBehaviour
{
    [SerializeField] private Image check;
    public GameState.Perk perk;

    //public Perk perk;
    public bool status = false;

    void Start(){
        check.enabled = status;
    }

    public void Toggle(){
        AudioManager.instance.Play("Checkbox");
        status = !status;
        check.enabled = status;
    }
}
