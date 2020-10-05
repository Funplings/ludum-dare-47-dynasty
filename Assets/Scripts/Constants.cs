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
    public const int NUM_ROWS = 10;
    public const int NUM_COLS = 10;
    public const int NUM_STARTING_ENEMIES = 2;
    public const int NUM_STARTING_MINES = 3;

    public const int YEARS_PER_TURN = 25;

    #region Rebellion
    public const float PERCENT_FOOD_LOST = .25f;
    public const float PERCENT_SOLDIER_LOST = .25f;
    public const float PERCENT_TERRITORY_LOST = .3f;
    #endregion

    #region Faction variables
    public const float CHANCE_TO_EXPAND = .5f;
    #endregion

    #region Turn Variables
    public const int STARVING_HAPPINESS = -10;
    public const int FED_HAPPINESS = 3;
    public const int UNINVESTED_HAPPINESS = -1; //use gamesate HappinessPerInvest() because of perk
    public const int FARM_FOOD = 2;
    public const int MINE_MONEY = 5;
    public const int SUCCEED_INVADE_HAPPINESS = 10;
    public const int FAILED_INVADE_HAPPINESS = -10;
    #endregion

    public const int INVEST_COST = 3; //Empire Control - Payment

    #region Perks

    //Empire Control - Payment
    public const int FEED_COST = 2;
    public const int PERK_FEED_COST = 1;

    //Empire Control - Reward
    public const int INVESTED_HAPPINESS = 2;
    public const int PERK_INVESTED_HAPPINESS = 4;

    public const int TERRITORY_REVENUE = 2;
    public const int PERK_TERRITORY_REVENUE = 3;

    public const float PERK_SOLDIER_EFFICIENCY = 1.5f; // base should be 1, so no variable

    #endregion Perks

    #region TileController Modes
    public const int DEFAULT_MODE = 0;
    public const int SELECTING_MODE = 1;
    public const int EXPANDING_MODE = 2;
    public const int ABANDONING_MODE = 3;
    #endregion
}