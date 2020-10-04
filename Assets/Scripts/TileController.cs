using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController: MonoBehaviour
{
    static GameObject m_TilePopup;
    static TileController m_CurrSelectedTile;

    [SerializeField] Sprite m_NoFactionSprite;
    [SerializeField] Sprite m_PlayerFactionSprite;
    [SerializeField] Sprite[] m_EnemyFactionSprites;
    [SerializeField] GameObject m_TilePopupPrefab;
    [SerializeField] GameObject m_SelectCoverPrefab;

    int m_XIndex;
    int m_YIndex;
    int m_TileType; // 0 = None, 1 = Farm, 2 = Lab, 3 = Mine
    int m_Faction; // -1 = Player faction; -2 = no faction
    GameObject m_CurrSoldier;
    public GameObject m_SelectCover;
    SpriteRenderer m_SpriteRenderer;

    public void Awake() {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Initalize tile attributes
    public void SetAttributes(int xIndex, int yIndex, int tileType) {
        m_XIndex = xIndex;
        m_YIndex = yIndex;
        m_TileType = tileType;
        SetFaction(Constants.NO_FACTION_INDEX);
    }

    public void SetFaction(int faction) {
        // Error checking: setting an invalid faction index
        if (faction < Constants.NO_FACTION_INDEX || faction >= m_EnemyFactionSprites.Length) {
            print(string.Format("CANNOT SET TILE AS FACTION {0}", faction));
        }

        // Set faction
        m_Faction = faction;

        // Set tile sprite
        if (faction == Constants.NO_FACTION_INDEX) {
            m_SpriteRenderer.sprite = m_NoFactionSprite;
        } else if (faction == Constants.PLAYER_FACTION_INDEX) {
            m_SpriteRenderer.sprite = m_PlayerFactionSprite;
        } else {
            m_SpriteRenderer.sprite = m_EnemyFactionSprites[faction];
        }
    }

    public int GetFaction() {
        return m_Faction;
    }

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        // Debug logging
        print(string.Format("Clicked tile [{0}, {1}] of faction {2}", m_XIndex, m_YIndex, m_Faction));

        // If this is a player tile, create tile popup (or destroy it, if already selected)
        if (m_Faction == Constants.PLAYER_FACTION_INDEX || true) {
            if (m_CurrSelectedTile == this) {
                m_CurrSelectedTile = null;
                Destroy(m_TilePopup);
                Destroy(m_SelectCover);
                m_TilePopup = null;
            }
            else {
                Destroy(m_TilePopup);
                if (m_CurrSelectedTile != null) {
                    Destroy(m_CurrSelectedTile.m_SelectCover);
                }
                m_CurrSelectedTile = this;
                m_SelectCover = Instantiate(m_SelectCoverPrefab, transform);
                m_TilePopup = Instantiate(m_TilePopupPrefab, MapManager.m_StaticPopupCanvas.transform);
                m_TilePopup.transform.position = transform.position;

                // Determine vertical position
                if (m_YIndex >= Constants.NUM_ROWS / 2) {
                    m_TilePopup.transform.position -= new Vector3(0, 2f, 0);
                }
                else {
                    m_TilePopup.transform.position += new Vector3(0, 2f, 0);
                }

                // Determine horizontal position
                if (m_XIndex < 2) {
                    m_TilePopup.transform.position += new Vector3(1.5f, 0, 0);
                }
                else if (m_XIndex > Constants.NUM_COLS - 3) {
                    m_TilePopup.transform.position -= new Vector3(1.5f, 0, 0);
                }
            }
        }
    }
}
