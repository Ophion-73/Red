using System;
using System.Collections.Generic;

// cosas de out run
[Serializable]
public class MetaData
{
    public int permanentDamageLevel;
    public int highscore;
}

// cosas de in run
[Serializable]
public class RunData
{
    public float currentHealth;
    public int currentDamage;
    public int currentRoom;
    public List<string> inventoryItems = new List<string>();
}
