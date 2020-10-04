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


    void Awake() {
        m_ExpandButton.onClick.AddListener(Expand);
    }

    public void SetTile(TileController tile) {
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

    void Expand() {
        m_Tile.ExpandTile();
    }

    void CancelExpansion() {
        m_Tile.CancelExpansion();
    }
}
