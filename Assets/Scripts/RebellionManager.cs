using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RebellionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text FoodCount;
    [SerializeField] private TMP_Text SoldierCount;
    [SerializeField] private TMP_Text TerritoryCount;
    [SerializeField] private GameObject UI;
    [SerializeField] private UIManager mapCanvas;

    private Animator animator;
    

    void Start(){
        animator = GetComponent<Animator>();
    }

    public void UpdateText(int foodLost, int soldiersLost, int territoriesLost){
        FoodCount.text = string.Format("{0} Food", foodLost);
        SoldierCount.text = string.Format("{0} Soldiers", soldiersLost);
        TerritoryCount.text = string.Format("Due to instability,\n{0} territories have fallen.", territoriesLost);
    }

    public void StartFade(){
        animator.SetTrigger("fade");
    }

    public void FadeSwitch(){
        UI.SetActive(true);
        mapCanvas.Hide();

    }
}
