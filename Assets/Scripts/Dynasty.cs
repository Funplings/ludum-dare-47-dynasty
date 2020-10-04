using System.Collections;
using System.Collections.Generic;

public class Dynasty {
    public string name;
    public int turnStarted; 
    public int turnEnded;

    public Dynasty(string name, int turnStarted, int turnEnded)
    {
        this.name = name;
        this.turnStarted = turnStarted;
        this.turnEnded = turnEnded;
    }
    public string GetName()
    {
        return this.name;
    }
    public int GetDuration()
    {
        return this.turnEnded - this.turnStarted;
    }
}