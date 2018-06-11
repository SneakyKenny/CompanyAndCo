using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Items
{

    int dmg;
    int durability;
    public Broom(string name, int lvl = 0, int dmg = 5, int durability = 2): base (ItemType.WEAPON,ItemClass.BROOM, name, lvl)
    {
        this.dmg = dmg + 5 * lvl;
        this.durability = durability + lvl;
    }

    public override void UpdateStats()
    {
        dmg += 5;
        durability++;
    }

    public override void UseItem(Character target)
    {
        throw new System.Exception("Special capacities not implemented yet");
    }
}
