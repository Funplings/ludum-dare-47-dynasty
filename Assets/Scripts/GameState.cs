using System.Collections;
using System.Collections.Generic;

public class GameState
{
    public int m_Happiness = 50;
    public int m_Money = Constants.STARTING_MONEY;
    public int m_Food = 0;
    public int m_Soldiers = 0;
    public int turn = 0;
    public Dynasty currentDynasty = null;
    public List<Dynasty> allDynasties = new List<Dynasty>(); 
    //perks

}
