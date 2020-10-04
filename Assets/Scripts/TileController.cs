using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController: MonoBehaviour
{
    #region Static variables
    static GameObject m_TilePopup;
    static TileController m_CurrSelectedTile;
    static int m_Mode;
    static List<TileController> m_ExpansionOptions = new List<TileController>();
    static List<TileController> m_AbandonedTiles = new List<TileController>();
    #endregion

    #region Sprites
    [SerializeField] Sprite m_NoFactionSprite;
    [SerializeField] Sprite m_PlayerFactionSprite;
    [SerializeField] Sprite[] m_EnemyFactionSprites;
    #endregion

    #region Prefab variables
    [SerializeField] GameObject m_TilePopupPrefab;
    [SerializeField] GameObject m_SelectCoverPrefab;
    [SerializeField] GameObject m_ExpansionIconPrefab;
    [SerializeField] GameObject m_ExpandArrowPrefab;
    [SerializeField] GameObject m_AbandonMarkerPrefab;
    #endregion

    #region Instance variables
    // Position
    int m_XIndex;
    int m_YIndex;

    // Metadata
    int m_TileType; // 0 = None, 1 = Farm, 2 = Lab, 3 = Mine
    int m_Faction; // -1 = Player faction; -2 = no faction
    public bool m_WillExpand;
    public TileController m_ExpandTarget;
    GameObject m_CurrSoldier;

    // Markers
    public GameObject m_SelectCover;
    public GameObject m_ExpansionIcon;
    public GameObject m_ExpandArrow;
    public GameObject m_AbandonMarker;

    // Sprite renderer
    SpriteRenderer m_SpriteRenderer;
    #endregion

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

        switch (m_Mode) {
            case Constants.DEFAULT_MODE:
            // If this is a player tile, create tile popup
            if (m_Faction == Constants.PLAYER_FACTION_INDEX || true) {
                CreateTilePopup();

                m_Mode = Constants.SELECTING_MODE;
            }
            break;

            case Constants.SELECTING_MODE:
            // If this is the currently selected tile, deselect
            if (m_CurrSelectedTile == this) {
                ClearTilePopup();
                ClearSelectedTile();

                m_Mode = Constants.DEFAULT_MODE;
            }
            // If this is a player tile, deselect the previously selected tile and create tile popup
            else if (m_Faction == Constants.PLAYER_FACTION_INDEX || true) {
                ClearTilePopup();
                ClearSelectedTile();

                CreateTilePopup();

                m_Mode = Constants.SELECTING_MODE;
            }
            // Otherwise, deselect the currently selected tile
            else {
                ClearTilePopup();
                ClearSelectedTile();

                m_Mode = Constants.DEFAULT_MODE;
            }
            break;

            case Constants.EXPANDING_MODE:
            // If this is an expandable tile, choose this tile to expand to
            if (m_ExpansionOptions.Contains(this)) {
                m_WillExpand = true;
                m_CurrSelectedTile.m_ExpandTarget = this;

                // Create arrow
                GameObject arrow = Instantiate(m_ExpandArrowPrefab, m_CurrSelectedTile.transform);
                m_CurrSelectedTile.m_ExpandArrow = arrow;

                // Facing above
                if (m_YIndex > m_CurrSelectedTile.m_YIndex) {
                    arrow.transform.Rotate(new Vector3(0f, 0f, 0f));
                }
                // Facing left
                else if (m_XIndex < m_CurrSelectedTile.m_XIndex) {
                    arrow.transform.Rotate(new Vector3(0f, 0f, 90f));
                }
                // Facing below
                else if (m_YIndex < m_CurrSelectedTile.m_YIndex) {
                    arrow.transform.Rotate(new Vector3(0f, 0f, 180f));
                }
                // Facing right
                else if (m_XIndex > m_CurrSelectedTile.m_XIndex) {
                    arrow.transform.Rotate(new Vector3(0f, 0f, 270f));
                }
            }

            while (m_ExpansionOptions.Count > 0) {
                m_ExpansionOptions[0].ClearExpansionOption();
            }

            ClearSelectedTile();

            m_Mode = Constants.DEFAULT_MODE;
            break;

            case Constants.ABANDONING_MODE:
            if (!m_AbandonedTiles.Contains(this)) {
                if (m_AbandonedTiles.Count < Constants.REBELLION_TILE_LOSS) {
                    m_AbandonedTiles.Add(this);
                    m_AbandonMarker = Instantiate(m_AbandonMarkerPrefab, transform);
                }
                else {
                    print("Cannot select any more tiles to abandon");
                }
            }
            else {
                m_AbandonedTiles.Remove(this);
                Destroy(m_AbandonMarker);
                m_AbandonMarker = null;
            }
            break;
        }
    }

    void CreateTilePopup() {
        m_CurrSelectedTile = this;
        m_SelectCover = Instantiate(m_SelectCoverPrefab, transform);
        m_TilePopup = Instantiate(m_TilePopupPrefab, MapManager.m_StaticPopupCanvas.transform);
        m_TilePopup.transform.position = transform.position;
        m_TilePopup.GetComponent<TilePopup>().SetTile(this);

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

    public static void ClearTilePopup() {
        Destroy(m_TilePopup);
        m_TilePopup = null;
    }

    public static void ClearSelectedTile() {
        if (m_CurrSelectedTile != null) {
            Destroy(m_CurrSelectedTile.m_SelectCover);
        }
        m_CurrSelectedTile = null;
    }

    #region Getters and Setters
    public static int GetMode() {
        return m_Mode;
    }

    public static void SetMode(int mode) {
        m_Mode = mode;
    }
    #endregion

    #region Expansion
    public void SetExpansionOption() {
        // Put expansion cover over self
        m_ExpansionIcon = Instantiate(m_ExpansionIconPrefab, transform);

        // Add self to expansion options list
        m_ExpansionOptions.Add(this);
    }

    public void ClearExpansionOption() {
        // Remove expansion cover
        Destroy(m_ExpansionIcon);

        // Remove self from expansions options list
        m_ExpansionOptions.Remove(this);
    }

    public void ExpandTile() {
        // Cannot expand if this is not the selected tile
        if (m_CurrSelectedTile != this) {
            print("Error: this tile is not selected");
        }

        // Clear the tile popup
        ClearTilePopup();

        // Set mode
        m_Mode = Constants.EXPANDING_MODE;

        // Determine which tiles can be expanded to
        // Above
        if (m_YIndex < Constants.NUM_ROWS - 1) {
            TileController aboveTile = MapManager.m_TileMap[m_XIndex, m_YIndex + 1];
            if (aboveTile.GetFaction() == Constants.NO_FACTION_INDEX && !aboveTile.m_WillExpand) {
                aboveTile.SetExpansionOption();
            }
        }
        // Below
        if (m_YIndex > 0) {
            TileController belowTile = MapManager.m_TileMap[m_XIndex, m_YIndex - 1];
            if (belowTile.GetFaction() == Constants.NO_FACTION_INDEX && !belowTile.m_WillExpand) {
                belowTile.SetExpansionOption();
            }
        }
        // Left
        if (m_XIndex > 0) {
            TileController leftTile = MapManager.m_TileMap[m_XIndex - 1, m_YIndex];
            if (leftTile.GetFaction() == Constants.NO_FACTION_INDEX && !leftTile.m_WillExpand) {
                leftTile.SetExpansionOption();
            }
        }
        // Right
        if (m_XIndex < Constants.NUM_COLS - 1) {
            TileController rightTile = MapManager.m_TileMap[m_XIndex + 1, m_YIndex];
            if (rightTile.GetFaction() == Constants.NO_FACTION_INDEX && !rightTile.m_WillExpand) {
                rightTile.SetExpansionOption();
            }
        }
    }

    public void CancelExpansion() {
        // Cancel expansion
        m_ExpandTarget.m_WillExpand = false;
        m_ExpandTarget = null;
        Destroy(m_ExpandArrow);
        m_ExpandArrow = null;

        // Clear the tile popup
        ClearTilePopup();

        // Clear selection
        ClearSelectedTile();

        // Set mode
        m_Mode = Constants.DEFAULT_MODE;
    }
    #endregion

    #region Abandoning

    #endregion
}
