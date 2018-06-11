using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items {

    public enum ItemType
    {
        WEAPON,
        SUPPORT
    };
    public enum ItemClass : int
    {
        MONEY,
        STICK,
        COMPUTER,
        CALCULATOR,
        BROOM,
        STAPLER,
        COFFEE,
        DONUT,
        TASER
    };


    public string iname;
    public int lvl;
    public ItemType type;
    public ItemClass c;
    public string desc;
  //  int[] stats = new int[1];
    public Items(ItemType t,ItemClass c, string name = "", int lvl = 0, string desc = "")
    {
        this.type = t;
        this.iname = name;
        this.lvl = 0;
        this.c = c;
        this.desc = desc;
    }

    public bool Upgrade()
    {
        if (this.lvl < 2)
        {
            this.lvl++;
            UpdateStats();
            return true;
        }
        else return false;
    }

    public virtual void UpdateStats()
    {  
              
    }

    public virtual void UseItem(Character c)
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
