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
        if(!ownedTiles.Contains(tile)) return;
        ownedTiles.Remove(tile);
    }

    public void AddTile(TileController tile){
        if(ownedTiles.Contains(tile)) return;
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

    public int MineCount(){
        int count = 0;
        foreach(TileController tile in ownedTiles){
            if(tile.GetTileType() == TileController.TileType.MINE){
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
    
    private T RandomFromList<T>(List<T> list){
        if(list.Count == 0){
            return default(T);
        }
        return list[Random.Range(0, list.Count)];
    } 

    // -1 = unable to siege, 0 = basic expansion, 1 = successful siege to player, 2 = successful siege to nonplayer, 3 = failed siege
    public int MaybeSiege(){
        List<TileController> canExpandTiles = new List<TileController>();
        foreach(TileController tile in ownedTiles){
            if(tile.canExpand(this)){
                canExpandTiles.Add(tile);
            }
        }
        if(canExpandTiles.Count == 0) return -1;

        TileController tileToExpand = RandomFromList(canExpandTiles);
        TileController expandToTile = RandomFromList(tileToExpand.ExpandOptions());

        if(expandToTile.GetTileType() != TileController.TileType.NONE){
            //Siege
            bool success = Random.value < TileController.InvasionChance(tileToExpand.GetSoldier(), expandToTile.m_ExpandTarget.GetSoldier());
            if(success){
                expandToTile.SetFaction(tileToExpand.GetFaction());
                expandToTile.SetSoldier(tileToExpand.GetSoldier() - 1);
                tileToExpand.SetSoldier(0);
                return 1;
            }
            else{
                tileToExpand.SetSoldier(0);
                return 2;
            }

        }
        else{
            expandToTile.SetFaction(tileToExpand.GetFaction());
            expandToTile.SetSoldier(tileToExpand.GetSoldier() - 1);
            tileToExpand.SetSoldier(0);
            return 0;
        }

    }

    public void RemoveAllSoldiers(){
        foreach(TileController tile in ownedTiles){
            tile.SetSoldier(0);
        }
    }

}
