using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController: MonoBehaviour
{
    [SerializeField] Sprite m_NoFactionSprite;
    [SerializeField] Sprite m_PlayerFactionSprite;
    [SerializeField] Sprite[] m_EnemyFactionSprites;
    int m_XIndex;
    int m_YIndex;
    int m_TileType; // 0 = None, 1 = Farm, 2 = Lab, 3 = Mine
    int m_Faction; // -1 = Player faction; -2 = no faction
    GameObject m_CurrSoldier;
    SpriteRenderer m_SpriteRenderer;

    public void Awake() {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Initalize tile attributes
    public void SetAttributes(int xIndex, int yIndex, int tileType) {
        m_XIndex = xIndex;
        m_YIndex = yIndex;
        m_TileType = tileType;
        SetFaction(-2);
    }

    public void SetFaction(int faction) {
        // Error checking: setting an invalid faction index
        if (faction < -2 || faction >= m_EnemyFactionSprites.Length) {
            print(string.Format("CANNOT SET TILE AS FACTION {0}", faction));
        }

        // Set faction
        m_Faction = faction;

        // Set tile sprite
        if (faction == -2) {
            m_SpriteRenderer.sprite = m_NoFactionSprite;
        } else if (faction == -1) {
            m_SpriteRenderer.sprite = m_PlayerFactionSprite;
        } else {
            m_SpriteRenderer.sprite = m_EnemyFactionSprites[faction];
        }
    }

    public int GetFaction() {
        return m_Faction;
    }

    void OnMouseDown() {
        print(string.Format("Clicked tile [{0}, {1}] of faction {2}", m_XIndex, m_YIndex, m_Faction));
    }
}
