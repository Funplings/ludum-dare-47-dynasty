using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilePopup : MonoBehaviour {
    [SerializeField] Button m_PlaceSoldierButton;
    [SerializeField] Button m_PlaceFarmButton;
    [SerializeField] Button m_PlaceLabButton;
    [SerializeField] Button m_ExpandButton;
    [SerializeField] Text m_ExpandText;
    TileController m_Tile;

    public void SetTile(TileController tile) {
        m_PlaceSoldierButton.onClick.AddListener(PlaceSoldier);
        m_PlaceFarmButton.onClick.AddListener(PlaceFarm);
        m_PlaceLabButton.onClick.AddListener(PlaceLab);
        m_Tile = tile;
        if (m_Tile.m_ExpandTarget == null) {
            m_ExpandText.text = "Expand";
            m_ExpandButton.onClick.AddListener(Expand);
        }
        else {
            m_ExpandText.text = "Cancel Expansion";
            m_ExpandButton.onClick.AddListener(CancelExpansion);
        }
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

    void Expand() {
        m_Tile.ExpandTile();
    }

    void CancelExpansion() {
        m_Tile.CancelExpansion();
    }
}
