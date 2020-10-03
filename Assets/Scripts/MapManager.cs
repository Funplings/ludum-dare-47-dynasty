using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // Serializable fields
    [SerializeField] GameObject m_TilePrefab;
    [SerializeField] int m_RowCount;
    [SerializeField] int m_ColCount;
    [SerializeField] int m_TileSize;
    [SerializeField] Vector3 m_MapCenter;
    [SerializeField]


    // Tile map
    TileController[,] m_TileMap;
    Hashtable m_FactionHashtable = new Hashtable();

    void Start() {
        InitalizeMap();
    }

    // Initalizes the m_RowCount x m_ColCount tile map in the game.
    void InitalizeMap() {
        // Instantiate tile map
        m_TileMap = new TileController[m_RowCount, m_ColCount];

        // Calculate bottom right starting corner
        Vector3 bottomRight = m_MapCenter;
        bottomRight.x -= m_TileSize * (m_RowCount - 1) / 2f;
        bottomRight.y -= m_TileSize * (m_ColCount - 1) / 2f;

        // Instantiate tiles
        for (int x = 0; x < m_RowCount; x += 1) {
            for (int y = 0; y < m_ColCount; y += 1) {
                Vector3 tilePosition = bottomRight;
                tilePosition.x += m_TileSize * x;
                tilePosition.y += m_TileSize * y;
                GameObject tile = Instantiate(m_TilePrefab, tilePosition, Quaternion.identity);
                m_TileMap[x, y] = tile.GetComponent<TileController>();
                m_TileMap[x, y].SetAttributes(x, y, 0);
            }
        }
    }
}
