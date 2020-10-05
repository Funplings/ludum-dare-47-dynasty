using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEventPopup : MonoBehaviour
{
    [SerializeField] TMP_Text description;
    public bool active;

    public void Show(string message){
        active = true;
        description.text = message;
        gameObject.SetActive(true);
    }

    public void OK(){
        active = false;
        gameObject.SetActive(false);
    }
}
