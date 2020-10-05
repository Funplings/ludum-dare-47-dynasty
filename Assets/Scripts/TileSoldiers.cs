using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSoldiers : MonoBehaviour
{

    int soldierCount = 0;
    private Grid grid;
    private Tilemap tilemap;
    public Tile baseTile;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        tilemap = GetComponentInChildren<Tilemap>();
        UpdateVisual();
    }

    public void AddCount(int delta){
        soldierCount = Mathf.Max(0, soldierCount + delta);
        // print("SOLDIERCOUNT:" + soldierCount);
        UpdateVisual();
    }

    public void SetCount(int count){
        soldierCount = count;
        UpdateVisual();
    }

    public int GetCount(){
        return soldierCount;
    }

    void UpdateVisual(){
        if(soldierCount == 0){
            gameObject.SetActive(false);
            return;
        }
        else{
            gameObject.SetActive(true);
        }

        tilemap.ClearAllTiles();
        
        int divisions = Mathf.Max(2, Mathf.CeilToInt(Mathf.Log(soldierCount, 2)));
        float gridSize = 1 / (float) divisions;
        grid.cellSize = new Vector3(gridSize, gridSize, 0);

        int count = 0;
        for(int i = 0; i < divisions; i++){
            for(int j = -1; j > -divisions -1; j--){
                if(count == soldierCount){
                    return;
                }

                Vector3Int vec = new Vector3Int(i, j, 0);
                tilemap.SetTile(vec, baseTile);
                count++;
            }
        }
        
    }
}
