using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController: MonoBehaviour
{
    #region Enums
    public enum TileType {
        NONE,
        FARM,
        LAB,
        MINE
    }
    #endregion

    #region Static variables
    static GameObject m_TilePopup;
    static TileController m_CurrSelectedTile;
    static int m_Mode;
    static List<TileController> m_ExpansionOptions = new List<TileController>();
    static List<TileController> m_AbandonedTiles = new List<TileController>();
    static List<TileController> m_ExpandingTiles = new List<TileController>();

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
    TileType m_TileType; 
    Faction m_Faction = Faction.None;
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
    public void SetAttributes(int xIndex, int yIndex, TileType tileType) {
        m_XIndex = xIndex;
        m_YIndex = yIndex;
        m_TileType = tileType;
        SetFaction(Faction.None);
    }

    public void SetFaction(Faction faction) {
        // Set faction
        m_Faction = faction;
        m_SpriteRenderer.color = faction.m_Color;
    }

    public Faction GetFaction() {
        return m_Faction;
    }

    public TileType GetTileType(){
        return m_TileType;
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
            if (m_Faction.IsPlayer()) {
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
            else if (m_Faction.IsPlayer()) {
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
            bool expanded = false;

            // If this is an expandable tile, choose this tile to expand to
            if (m_ExpansionOptions.Contains(this)) {
                // Handle expansion state for this tile and and the tile that will expand to it
                expanded = true;
                m_WillExpand = true;
                m_CurrSelectedTile.m_ExpandTarget = this;
                m_ExpandingTiles.Add(m_CurrSelectedTile);
                Destroy(m_ExpansionIcon);
                m_ExpansionIcon = null;

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

            // Clear all expansion options
            while (m_ExpansionOptions.Count > 0) {
                m_ExpansionOptions[0].ClearExpansionOption();
            }

            // Clear the selected tile
            ClearSelectedTile();

            // Return the mode to default
            m_Mode = Constants.DEFAULT_MODE;

            break;

            case Constants.ABANDONING_MODE:
            // Cannot lose a non-player faction tile
            if ( !m_Faction.IsPlayer() ) {
                break;
            }

            // Handle tile abandoning
            if (!m_AbandonedTiles.Contains(this)) {
                if (m_AbandonedTiles.Count < MapManager.GetAbandonCount() ) {
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
    public static void ExpandTiles() {
        while (m_ExpandingTiles.Count > 0) {
            TileController tile = m_ExpandingTiles[0];
            tile.m_ExpandTarget.SetFaction(Faction.GetPlayer());
            tile.CancelExpansion();
        }
    }

    public void SetExpansionOption() {
        // Put expansion cover over self
        m_ExpansionIcon = Instantiate(m_ExpansionIconPrefab, transform);

        // Expansion cover should be a semi-transparent version of the player faction color
        m_ExpansionIcon.GetComponent<SpriteRenderer>().color = Color.red;

        // Add self to expansion options list
        m_ExpansionOptions.Add(this);
    }

    public void ClearExpansionOption() {
        // Remove expansion cover
        Destroy(m_ExpansionIcon);
        m_ExpansionIcon = null;

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
            if (aboveTile.GetFaction() == Faction.None && !aboveTile.m_WillExpand) {
                aboveTile.SetExpansionOption();
            }
        }
        // Below
        if (m_YIndex > 0) {
            TileController belowTile = MapManager.m_TileMap[m_XIndex, m_YIndex - 1];
            if (belowTile.GetFaction() == Faction.None && !belowTile.m_WillExpand) {
                belowTile.SetExpansionOption();
            }
        }
        // Left
        if (m_XIndex > 0) {
            TileController leftTile = MapManager.m_TileMap[m_XIndex - 1, m_YIndex];
            if (leftTile.GetFaction() == Faction.None && !leftTile.m_WillExpand) {
                leftTile.SetExpansionOption();
            }
        }
        // Right
        if (m_XIndex < Constants.NUM_COLS - 1) {
            TileController rightTile = MapManager.m_TileMap[m_XIndex + 1, m_YIndex];
            if (rightTile.GetFaction() == Faction.None && !rightTile.m_WillExpand) {
                rightTile.SetExpansionOption();
            }
        }
    }

    public void CancelExpansion() {
        // Cancel expansion
        Destroy(m_ExpandTarget.m_ExpansionIcon);
        m_ExpandTarget.m_ExpansionIcon = null;
        m_ExpandTarget.m_WillExpand = false;
        m_ExpandTarget = null;
        Destroy(m_ExpandArrow);
        m_ExpandArrow = null;
        m_ExpandingTiles.Remove(this);

        // Clear the tile popup
        ClearTilePopup();

        // Clear selection
        ClearSelectedTile();

        // Set mode
        m_Mode = Constants.DEFAULT_MODE;
    }
    #endregion

    #region Abandoning
    public static void AbandonTiles() {
        while (m_AbandonedTiles.Count > 0) {
            TileController tile = m_AbandonedTiles[0];
            tile.SetFaction(Faction.None);
            tile.FinishAbandoning();
        }
    }

    void FinishAbandoning() {
        Destroy(m_AbandonMarker);
        m_AbandonMarker = null;
        m_AbandonedTiles.Remove(this);
    }

    public static int GetAbandonedTilesCount() {
        return m_AbandonedTiles.Count;
    }
    #endregion
}
