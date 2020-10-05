using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents
{
    public delegate void Func();

    public class RandomEvent {
        public string message;
        public Func eventFunc;

        public RandomEvent(string message, Func eventFunc)
        {
            this.message = message;
            this.eventFunc = eventFunc;
        }
    }

    public static List<RandomEvent> randomEvents = new List<RandomEvent>{
        new RandomEvent("A new enemy empire has risen!", NewEmpire),
        new RandomEvent("A grand festival brings great joy to your citizens!", PlusHappiness),
        new RandomEvent("The citizens grow displeased for some reason, I can't think of anything.", MinusHappiness),
        new RandomEvent("A bountiful harvest brings extra food!", PlusFood),
        new RandomEvent("A flood ruins your food reserves!", MinusFood),
        new RandomEvent("Some soldiers were feeling kinda patriotic, so your army grew!", PlusSoldier),
        new RandomEvent("Being in the army sucks, so you lost some soldiers.", MinusSoldier),
        new RandomEvent("C-c-c-c-c-cash baby!", PlusMoney),
        new RandomEvent("A maniac came and ate some of the money in the national bank.", MinusMoney),
        new RandomEvent("A new money source has been discovered!", AddMine),

    };

    private static void NewEmpire(){
        
        return;
    }

    private static void PlusHappiness(){

    }

    private static void MinusHappiness(){

    }

    private static void PlusFood(){

    }

    private static void MinusFood(){

    }

    private static void PlusSoldier(){

    }

    private static void MinusSoldier(){

    }

    private static void PlusMoney(){

    }

    private static void MinusMoney(){

    }

    private static void AddMine(){

    }
}
