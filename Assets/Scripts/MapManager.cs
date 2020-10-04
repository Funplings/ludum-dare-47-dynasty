using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    // Serializable fields
    [SerializeField] GameObject m_TilePrefab;
    [SerializeField] int m_TileSize;
    [SerializeField] Vector3 m_MapCenter;
    [SerializeField] int m_NumEnemyFactions;
    [SerializeField] Canvas m_PopupCanvas;

    public static Canvas m_StaticPopupCanvas = null;

    // Tile map
    TileController[,] m_TileMap;
    Dictionary<int, List<(int, int)>> m_FactionTiles; // Maps faction indexes to a list of (int, int) tuples indicating the tiles that belong to that faction (-2 is player)
    System.Random m_Random;

    void Start() {
        m_StaticPopupCanvas = m_PopupCanvas;
        m_Random = new System.Random();
        InitalizeMap();
    }

    // Initalizes the Constants.NUM_ROWS x Constants.NUM_COLS tile map in the game.
    void InitalizeMap() {
        // Instantiate tile map
        m_TileMap = new TileController[Constants.NUM_ROWS, Constants.NUM_COLS];

        // Instantiate faction hash table
        m_FactionTiles = new Dictionary<int, List<(int, int)>>();
        m_FactionTiles.Add(Constants.PLAYER_FACTION_INDEX, new List<(int, int)>());
        for (int i = 0; i < m_NumEnemyFactions; i++) {
            m_FactionTiles.Add(i, new List<(int, int)>());
        }

        // Calculate bottom right starting corner
        Vector3 bottomRight = m_MapCenter;
        bottomRight.x -= m_TileSize * (Constants.NUM_ROWS - 1) / 2f;
        bottomRight.y -= m_TileSize * (Constants.NUM_COLS - 1) / 2f;

        // Instantiate tiles
        for (int x = 0; x < Constants.NUM_ROWS; x += 1) {
            for (int y = 0; y < Constants.NUM_COLS; y += 1) {
                Vector3 tilePosition = bottomRight;
                tilePosition.x += m_TileSize * x;
                tilePosition.y += m_TileSize * y;
                GameObject tile = Instantiate(m_TilePrefab, tilePosition, Quaternion.identity);
                m_TileMap[x, y] = tile.GetComponent<TileController>();
                m_TileMap[x, y].SetAttributes(x, y, 0);
            }
        }

        // Instantiate player faction
        (int, int) playerFactionPosition = (m_Random.Next(Constants.NUM_ROWS), m_Random.Next(Constants.NUM_COLS));
        SetTileFaction(playerFactionPosition, Constants.PLAYER_FACTION_INDEX);

        // Instantiate enemy factions
        for (int i = 0; i < m_NumEnemyFactions; i++) {
            bool instantiated = false;
            while (!instantiated) {
                (int, int) pos = (m_Random.Next(Constants.NUM_ROWS), m_Random.Next(Constants.NUM_COLS));
                if (m_TileMap[pos.Item1, pos.Item2].GetFaction() == Constants.NO_FACTION_INDEX) {
                    SetTileFaction(pos, i);
                    instantiated = true;
                }
            }
        }
    }

    void SetTileFaction((int, int) position, int faction) {
        m_TileMap[position.Item1, position.Item2].SetFaction(faction);
        m_FactionTiles[faction].Add(position);
    }

    private void OnMouseDown() {
        
    }
}
