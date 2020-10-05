using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState
{
    public enum Perk {
        INVEST,
        WEALTH,
        FOOD,
        SOLDIER
    }

    public string m_EmpireName;
    public int m_Happiness = 50;
    public int m_Money = Constants.STARTING_MONEY;
    public int m_Food = 0;
    public int m_Soldiers = 0;
    public int m_turn = 0;
    public Dynasty m_currentDynasty = null;
    public List<Faction> enemyFactions = new List<Faction>();
    public List<Dynasty> m_allDynasties = new List<Dynasty>(); 
    public HashSet<Perk> m_perks = new HashSet<Perk>();

    //Happiness per investment
    public int HappinessPerInvest(){
        return m_perks.Contains(Perk.INVEST) ? Constants.PERK_INVESTED_HAPPINESS : Constants.INVESTED_HAPPINESS;
    }

    //Revenue per territory
    public int RevenuePerTerritory(){
        return m_perks.Contains(Perk.INVEST) ? Constants.PERK_TERRITORY_REVENUE : Constants.TERRITORY_REVENUE;
    }

    //Cost of feeding
    public int GetFeedCost(){
        return m_perks.Contains(Perk.FOOD) ? Constants.PERK_FEED_COST : Constants.FEED_COST;
    }

    //Chance of winning a battle given a base chance
    public float GetSoliderChance(float chance){
        if(!m_perks.Contains(Perk.SOLDIER)) return chance;

        float remaining = 1 - chance;
        return chance + (remaining - remaining/Constants.PERK_SOLDIER_EFFICIENCY);
    }

    public bool CheckForReusedName(string name){
        return m_allDynasties.Exists(dynasty => dynasty.name == name);
    }


}
