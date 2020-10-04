using System.Collections;
using System.Collections.Generic;

public static class Constants {
    
    public enum ON_SALE {
        FOOD,
        SOLDIER,
        FARM,
        LAB,
        PLACE_SOLDIER
    }

    public const int STARTING_MONEY = 100;
    public const int GRID_SIZE = 4;
    public const int PLAYER_FACTION_INDEX = -1;
    public const int NO_FACTION_INDEX = -2;
    public const int NUM_ROWS = 10;
    public const int NUM_COLS = 10;

    #region Perks

    public const int FEED_COST = 2;
    public const int PERK_FEED_COST = 1;

    public const int HAPPY_PER_INVEST = 2;
    public const int PERK_HAPPY_PER_INVEST = 4;

    public const int TERRITORY_REVENUE = 2;
    public const int PERK_TERRITORY_REVENUE = 3;

    public const float PERK_SOLDIER_EFFICIENCY = 1.5f; // base should be 1, so no variable

    #endregion Perks
}