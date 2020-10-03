using System.Collections;
using System.Collections.Generic;

public class Dynasty {
    string name;
    int turnStarted; 
    int turnEnded;

    public Dynasty(string name, int turnStarted, int turnEnded)
    {
        this.name = name;
        this.turnStarted = turnStarted;
        this.turnEnded = turnEnded;
    }
}