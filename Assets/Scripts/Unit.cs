using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public int hpMax, currentHP, attack, attackMental, defense, defenseMental, speed, stun;
	
	public Character.CharacterClass c;
	public List<Items> Inventory;
	
	public List <Ability> abilities;
	public Ability SpecialAbility;
	
	public Tile CurrentTile;

	private void Start ()
	{
		this.CurrentTile = BoardGenerator.tiles [0];

		MoveTo ( this.CurrentTile );
	}

	public bool Attack ()
	{
		Debug.Log ( "Attack." );

		return true;
	}

	public bool UseFirstAbility ()
	{
		Debug.Log ( "UseFirstAbility." );

		return true;
	}

	public bool UseSecondAbility ()
	{
		Debug.Log ( "UseSecondAbility." );

		return true;
	}

	public bool UseThirdAbility ()
	{
		Debug.Log ( "UseThirdAbility." );

		return true;
	}

	public bool UseSpecialAbility ()
	{
		Debug.Log ( "UseSpecialAbility." );

		return true;
	}
	
	public bool MoveTo ( Tile destinationTile )
	{
		try
		{
			this.CurrentTile.Unit = null;

			this.transform.position = destinationTile.transform.position + new Vector3 ( 0, destinationTile.vertOffset, 0 );

			this.CurrentTile = destinationTile;

			this.CurrentTile.Unit = this;

			return true;
		} catch ( Exception )
		{
			return false;
		}
	}

}
