using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public int hpMax, currentHP, attack, attackMental,defenseMental, defense, speed, stun;
	public string class;
	public int range;
	
	public List <Ability> abilities;
	public Ability SpecialAbility;

	public Tile CurrentTile;

	public void MoveTo ( Tile destinationTile )
	{
		this.transform.position = destinationTile.transform.position; //TODO: ajouter l'offset pour que l'unit ne soit pas dans le sol.
		
		this.CurrentTile = destinationTile;
	}

}
