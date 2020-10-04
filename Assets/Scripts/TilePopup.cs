using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilePopup : MonoBehaviour
{
    [SerializeField] Button m_PlaceSoldierButton;
    [SerializeField] Button m_PlaceFarmButton;
    [SerializeField] Button m_PlaceLabButton;

    void Awake() {
        m_PlaceSoldierButton.onClick.AddListener(PlaceSoldier);
        m_PlaceFarmButton.onClick.AddListener(PlaceFarm);
        m_PlaceLabButton.onClick.AddListener(PlaceLab);
    }

    void PlaceSoldier() {
        print("Place soldier");
    }

    void PlaceFarm() {
        print("Place farm");
    }

    void PlaceLab() {
        print("Place lab");
    }
}
