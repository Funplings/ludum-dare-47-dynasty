using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController: MonoBehaviour
{
    int m_XIndex;
    int m_YIndex;
    int m_TileType; // 0 = None, 1 = Farm, 2 = Lab, 3 = Mine
    GameObject m_CurrSoldier;

    // Initalize tile attributes
    public void SetAttributes(int xIndex, int yIndex, int tileType) {
        m_XIndex = xIndex;
        m_YIndex = yIndex;
        m_TileType = tileType;
    }

    void OnMouseDown() {
        print(string.Format("Clicked tile [{0}, {1}]", m_XIndex, m_YIndex));
    }
}
