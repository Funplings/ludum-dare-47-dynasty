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
        m_ExpandButton.onClick.AddListener(Expand);
    }

    public void SetTile(TileController tile) {
        m_Tile = tile;
    }

    void Expand() {
        if (!m_Tile.m_Expanding) {
            m_Tile.ExpandTile();
        }
    }
}
