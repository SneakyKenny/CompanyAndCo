﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapler : Items
{

    int dmg;
    int range;
    public Stapler(string name, int lvl = 0, int dmg = 5, int range = 2): base (ItemType.WEAPON,ItemClass.STAPLER, name, lvl)
    {
        this.dmg = dmg + 5 * lvl;
        this.range = range + lvl - 1;
    }

    public override void UpdateStats()
    {
        dmg += 5;
        range++;
    }

    public override void UseItem(Character target)
    {
        throw new System.Exception("Special capacities not implemented yet");
    }
}
