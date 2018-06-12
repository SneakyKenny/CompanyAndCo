using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public enum AbilityType
    {
        PHYS,
        MEN,
        SPE,
    };
	public string AbilityName;
	public int AbilityBaseDamage;
    public AbilityEffet effect;
    private int Area, range;
    private AbilityType type;

    public Ability(AbilityType t, string AbilityName, int Area, int range, int BaseDamage = 10)
    {
        if (t == AbilityType.MEN)
            range = 3;
        else
            range = 1;
        type = t;
        this.AbilityName = AbilityName;
        this.range = range;
        this.Area = Area;
        AbilityBaseDamage = BaseDamage;
        if (t == AbilityType.MEN)
            range += 3;
    }
}
