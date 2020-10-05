using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListGenerator : MonoBehaviour
{
    public GameObject DynastyInfoPrefab;
    public Transform listParent;

    void Start()
    {
        for(int i = 0; i < GameManager.instance.state.m_allDynasties.Count; i++){
            Dynasty dynasty = GameManager.instance.state.m_allDynasties[i];
            GameObject obj = Instantiate(DynastyInfoPrefab, listParent);
            DynastyListElement element = obj.GetComponent<DynastyListElement>();
            element.SetInfo(i+1, dynasty.name, dynasty.turnStarted * Constants.YEARS_PER_TURN, dynasty.turnEnded * Constants.YEARS_PER_TURN);
        }
    }
}
