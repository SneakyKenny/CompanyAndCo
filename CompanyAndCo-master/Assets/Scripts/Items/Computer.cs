using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Items
{

    int dmg;
    public Computer(string name, int lvl = 0, int dmg = 5): base (ItemType.WEAPON,ItemClass.COMPUTER, name, lvl)
    {
        this.dmg = dmg + 5*lvl;
    }

    public override void UpdateStats()
    {
        dmg += 5;
    }

    public override void UseItem(Character target)
    {
        throw new System.Exception("Special capacities not implemented yet");
    }
}
