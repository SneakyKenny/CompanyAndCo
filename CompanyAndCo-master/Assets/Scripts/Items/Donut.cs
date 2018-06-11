using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : Items {

    int boost;
    int dam;
    public Donut(string name, int lvl = 0, int boost = 10, int dam = 5): base (ItemType.SUPPORT,ItemClass.DONUT, name, lvl)
    {
        this.boost = boost + 5 * lvl;
        this.dam = dam - lvl;
    }

    public override void UpdateStats()
    {
        boost += 5;
        dam--;
    }

    public override void UseItem(Character target)
    {
        int[] statboost = { 0, this.boost, this.boost, this.boost, this.boost, 0 };
        target.BoostStats(statboost);
        if (target.Class != Character.Characterclass.GUARD)
            target.takeDmg(dam);
    }
}
