using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    // Serializable fields
    [SerializeField] GameObject m_TilePrefab;
    [SerializeField] int m_TileSize;
    [SerializeField] Vector3 m_MapCenter;
    [SerializeField] Canvas m_PopupCanvas;
    [SerializeField] UIManager uiManager;
    [SerializeField] DecisionManager decisionManager;

    public static Canvas m_StaticPopupCanvas = null;

    // Tile map
    public static TileController[,] m_TileMap;
    Dictionary<Faction, List<(int, int)>> m_FactionTiles; // Maps faction indexes to a list of (int, int) tuples indicating the tiles that belong to that faction (-2 is player)
    System.Random m_Random;

    bool m_Rebelling = false;

    #region Rebellion

    private static int abandonCount;
    public static int GetAbandonCount() { return abandonCount; }

    #endregion

    void Awake() {
        m_StaticPopupCanvas = m_PopupCanvas;
        m_Random = new System.Random();
        InitalizeMap();
    }

    #region Tiles

    // Initalizes the Constants.NUM_ROWS x Constants.NUM_COLS tile map in the game.
    void InitalizeMap() {
        // Instantiate tile map
        m_TileMap = new TileController[Constants.NUM_ROWS, Constants.NUM_COLS];

        // Create starting enemies
        
        List<Faction> startingFactions = GameManager.instance.state.enemyFactions;

        // Instantiate faction hash table
        m_FactionTiles = new Dictionary<Faction, List<(int, int)>>();
        m_FactionTiles.Add(Faction.GetPlayer(), new List<(int, int)>());
        for (int i = 0; i < Constants.NUM_STARTING_ENEMIES; i++) {
            Faction newFaction = Faction.GenerateFaction();
            startingFactions.Add(newFaction);
            m_FactionTiles.Add(newFaction, new List<(int, int)>());
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
                GameObject tile = Instantiate(m_TilePrefab, tilePosition, Quaternion.identity, transform);
                m_TileMap[x, y] = tile.GetComponent<TileController>();
                m_TileMap[x, y].SetAttributes(x, y, TileController.TileType.NONE);
            }
        }

        // Instantiate player faction
        (int, int) playerFactionPosition = (m_Random.Next(Constants.NUM_ROWS), m_Random.Next(Constants.NUM_COLS));
        SetTileFaction(playerFactionPosition, Faction.GetPlayer());

        // Instantiate enemy factions
        for (int i = 0; i < Constants.NUM_STARTING_ENEMIES; i++) {
            RandomSpawnFaction(startingFactions[i]);
        }

        // Make the first mines
        for (int i = 0; i < Constants.NUM_STARTING_MINES; i++){
            RandomSpawnMine();
        }
    }

    void RandomSpawnFaction(Faction faction){
        bool instantiated = false;
        while (!instantiated) {
            (int, int) pos = (m_Random.Next(Constants.NUM_ROWS), m_Random.Next(Constants.NUM_COLS));
            if (m_TileMap[pos.Item1, pos.Item2].GetFaction() == Faction.None) {
                SetTileFaction(pos, faction);
                instantiated = true;
            }
        }
    }

    //Return success or failure
    bool RandomSpawnMine(bool repeatUntilSuccess = false){
        do {
            (int, int) pos = (m_Random.Next(Constants.NUM_ROWS), m_Random.Next(Constants.NUM_COLS));
            if (m_TileMap[pos.Item1, pos.Item2].NewBuildingValid()) {
                m_TileMap[pos.Item1, pos.Item2].SetTileType(TileController.TileType.MINE);
                return true;
            }
        } while(repeatUntilSuccess);
        return false;
    }

    void SetTileFaction((int, int) position, Faction faction) {
        m_TileMap[position.Item1, position.Item2].SetFaction(faction);
        m_FactionTiles[faction].Add(position);
    }

    

    #endregion

    public void StartDynasty(){
        uiManager.UpdateDynasty();
        uiManager.ShowMapUI(true);
    }

    public void EndDynasty(){
        GameManager.instance.state.m_currentDynasty.turnEnded = GameManager.instance.state.m_turn;
        GameManager.instance.state.m_Happiness = 50;
        uiManager.UpdateHappinessCount();
        decisionManager.SetupDecision();
    }

    #region Getters and Setters

    public bool GetRebelling() {
        return m_Rebelling;
    }

    public void SetRebelling(bool rebelling) {
        m_Rebelling = rebelling;

        if (rebelling == true) {
            TileController.SetMode(Constants.ABANDONING_MODE);
        }

    }

    #endregion

    #region Rebellion

    public void StartRebellion(){
        GameState state = GameManager.instance.state;
        abandonCount = Mathf.Max(1, Mathf.FloorToInt(Constants.PERCENT_TERRITORY_LOST * Faction.GetPlayer().TerritoryCount()) );

        int foodLost = Mathf.FloorToInt(Constants.PERCENT_FOOD_LOST * state.m_Food);
        state.m_Food -= foodLost;
        uiManager.UpdateFoodCount();

        int soldiersLost = Mathf.FloorToInt(Constants.PERCENT_SOLDIER_LOST * state.m_Soldiers);
        state.m_Soldiers -= soldiersLost;
        uiManager.UpdateSoldiersCount();

        Faction.GetPlayer().RemoveAllSoldiers();
        uiManager.UpdateRebellion(foodLost, soldiersLost, abandonCount);
    }

    #endregion


    #region Buying

    public void Buy(Constants.ON_SALE item, int cost){
        GameState state = GameManager.instance.state;

        if(item == Constants.ON_SALE.PLACE_SOLDIER){
            if( cost > state.m_Soldiers) return;

            state.m_Soldiers -= cost;
            TileController.GetCurrentTile().AddSoldier(1);
            uiManager.UpdateSoldiersCount();
            return;
        }

        if( cost > state.m_Money ) return;

        

        switch(item){
            case Constants.ON_SALE.FOOD:
                state.m_Food++;
                uiManager.UpdateFoodCount();
                break;
            case Constants.ON_SALE.SOLDIER:
                state.m_Soldiers++;
                uiManager.UpdateSoldiersCount();
                break;
            case Constants.ON_SALE.FARM:
                if( !TileController.GetCurrentTile().NewBuildingValid() ) return;
                TileController.GetCurrentTile().SetTileType(TileController.TileType.FARM);
                TileController.ClearTilePopup();
                TileController.ClearSelectedTile();
                break;
            case Constants.ON_SALE.LAB:
                if( !TileController.GetCurrentTile().NewBuildingValid() ) return;
                TileController.GetCurrentTile().SetTileType(TileController.TileType.LAB);
                TileController.ClearTilePopup();
                TileController.ClearSelectedTile();
                break;
        }

        state.m_Money -= cost;
        uiManager.UpdateMoneyCount();
    }

    #endregion
}
