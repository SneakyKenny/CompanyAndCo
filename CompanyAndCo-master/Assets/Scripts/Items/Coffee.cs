﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Items {

    protected int healing;
    protected int nbUse;
    public Coffee(string name,int lvl = 0,int basePow = 10) : base(ItemType.SUPPORT,ItemClass.COFFEE, name, lvl)
    {
        healing = basePow;
        nbUse = lvl + 2;
    }

    public override void UpdateStats()
    {
        healing += 5 * lvl;
        nbUse += 1;
    }

   public override void UseItem(Character target)
    {
        target.Heal(healing);
        nbUse--;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
