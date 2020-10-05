using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction
{
    private static Faction playerFaction = new Faction(Color.magenta);
    public static Faction None = new Faction(new Color(0.7094289f, 0.7830189f, 0.2548505f, 1f));

    public Color m_Color;
    private List<TileController> ownedTiles = new List<TileController>();

    public Faction(Color factionColor)
    {
        this.m_Color = factionColor;
    }

    public bool IsPlayer(){
        return this == playerFaction;
    }

    public void SetAsPlayer(){
        playerFaction = this;
    }

    public static Faction GetPlayer(){
        return playerFaction;
    }

    public static Faction GenerateFaction(){
        return new Faction(new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F)));
    }

    public void RemoveTile(TileController tile){
        ownedTiles.Remove(tile);
    }

    public void AddTile(TileController tile){
        ownedTiles.Add(tile);
    }

    public int LabCount(){
        int count = 0;
        foreach(TileController tile in ownedTiles){
            if(tile.GetTileType() == TileController.TileType.LAB){
                count++;
            }
        }
        return count;
    }

    public int FarmCount(){
        int count = 0;
        foreach(TileController tile in ownedTiles){
            if(tile.GetTileType() == TileController.TileType.FARM){
                count++;
            }
        }
        return count;
    }

    public int TerritoryCount(){
        return ownedTiles.Count;
    }

    public void AddSoldier(){
        TileController tile = RandomFromList(ownedTiles);
        tile.AddSoldier(1);
    }
    
    public T RandomFromList<T>(List<T> list){
        if(list.Count == 0){
            return default(T);
        }
        return list[Random.Range(0, list.Count)];
    } 

    public void MaybeSiege(){
        if(Random.value < Constants.CHANCE_TO_SIEGE){
            //siege
        }
    }

    public void RemoveAllSoldiers(){
        foreach(TileController tile in ownedTiles){
            tile.SetSoldier(0);
        }
    }

}
