using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilePopup : MonoBehaviour {
    [SerializeField] Button m_PlaceSoldierButton;
    [SerializeField] Button m_PlaceFarmButton;
    [SerializeField] Button m_PlaceLabButton;
    [SerializeField] Button m_ExpandButton;
    TileController m_Tile;

    void Awake() {
        m_PlaceSoldierButton.onClick.AddListener(PlaceSoldier);
        m_PlaceFarmButton.onClick.AddListener(PlaceFarm);
        m_PlaceLabButton.onClick.AddListener(PlaceLab);
        m_ExpandButton.onClick.AddListener(Expand);
    }

    public void SetTile(TileController tile) {
        m_Tile = tile;
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
        if (!m_Tile.m_Expanding) {
            m_Tile.ExpandTile();
        }
    }
}
