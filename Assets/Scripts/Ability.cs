using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    public AbilityEffect effect;
    public int Area, range;
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

    public Ability(AbilityEffect effect)
    {
        type = AbilityType.SPE;
        this.range = 5;
        this.AbilityName = "Special ability";
        this.effect = effect;
        if (effect is PDG_Effect)
            range = 0;
    }
}
