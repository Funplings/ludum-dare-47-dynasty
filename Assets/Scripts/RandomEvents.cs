using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    public delegate void Func();

    public class RandomEvent {
        public string message;
        public string alert;
        public Func eventFunc;

        public RandomEvent(string message, string alert, Func eventFunc)
        {
            this.message = message;
            this.alert = alert;
            this.eventFunc = eventFunc;
        }
    }

    public static List<RandomEvent> randomEvents = new List<RandomEvent>{
        new RandomEvent("A new enemy empire has risen!", "New empire appeared!", NewEmpire),
        new RandomEvent("A grand festival brings great joy to your citizens!", "Happiness increased!", PlusHappiness),
        new RandomEvent("The citizens grow displeased for some reason, I can't think of anything.", "Happiness decreased...",MinusHappiness),
        new RandomEvent("A bountiful harvest brings extra food!", "Food increased!",PlusFood),
        new RandomEvent("A flood ruins your food reserves!", "Food decreased...",MinusFood),
        new RandomEvent("Some soldiers were feeling kinda patriotic, so your army grew!", "Army increased!",PlusSoldier),
        new RandomEvent("Being in the army sucks, so you lost some soldiers.", "Army decreased...",MinusSoldier),
        new RandomEvent("C-c-c-c-c-cash baby!", "Money increased!",PlusMoney),
        new RandomEvent("A maniac came and ate some of the money in the national bank.","Money decreased...", MinusMoney),
        new RandomEvent("A new money source has been discovered!", "New jade reserve appeared!",AddMine),

    };

    private static T RandomFromList<T>(List<T> list){
        if(list.Count == 0){
            return default(T);
        }
        return list[Random.Range(0, list.Count)];
    } 

    public static RandomEvent GetEvent(){
        return RandomFromList(randomEvents);
    }

    private static void NewEmpire(){
        Faction newFaction = Faction.GenerateFaction();
        MapManager mapManager = FindObjectOfType<MapManager>();
        bool success = mapManager.TryRandomSpawnFaction(newFaction);
        if(success){
            GameManager.instance.state.enemyFactions.Add(newFaction);
        }
    }

    private static void PlusHappiness(){
        GameManager.instance.state.m_Happiness += 10;
        GameManager.instance.state.m_Happiness = Mathf.Clamp(GameManager.instance.state.m_Happiness, 0, 100);
    }

    private static void MinusHappiness(){
        GameManager.instance.state.m_Happiness -= 10;
        GameManager.instance.state.m_Happiness = Mathf.Clamp(GameManager.instance.state.m_Happiness, 0, 100);
    }

    private static void PlusFood(){
        GameManager.instance.state.m_Food += 10;
        GameManager.instance.state.m_Food = Mathf.Max(GameManager.instance.state.m_Food, 0);
    }

    private static void MinusFood(){
        GameManager.instance.state.m_Food -= 10;
        GameManager.instance.state.m_Food = Mathf.Max(GameManager.instance.state.m_Food, 0);
    }

    private static void PlusSoldier(){
        GameManager.instance.state.m_Soldiers += 10;
        GameManager.instance.state.m_Soldiers = Mathf.Max(GameManager.instance.state.m_Soldiers, 0);
    }

    private static void MinusSoldier(){
        GameManager.instance.state.m_Soldiers -= 10;
        GameManager.instance.state.m_Soldiers = Mathf.Max(GameManager.instance.state.m_Soldiers, 0);
    }   

    private static void PlusMoney(){
        GameManager.instance.state.m_Money += 10;
        GameManager.instance.state.m_Money = Mathf.Max(GameManager.instance.state.m_Money, 0);
    }

    private static void MinusMoney(){
        GameManager.instance.state.m_Money -= 10;
        GameManager.instance.state.m_Money = Mathf.Max(GameManager.instance.state.m_Money, 0);
    }

    private static void AddMine(){
        MapManager mapManager = FindObjectOfType<MapManager>();
        mapManager.RandomSpawnMine(false);
    }
}
