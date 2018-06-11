using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public int hpMax, currentHP, attack, attackMental, defense, defenseMental, speed, stun, range;
	public string c;
	
	public List <Ability> abilities;
	public Ability SpecialAbility;
	
	public Tile CurrentTile;

	private void Start ()
	{
		MoveTo ( this.CurrentTile );
	}

	public int Attack ()
	{
		Debug.Log ( "Attack." );

		return 1;
	}

	public int UseFirstAbility ()
	{
		Debug.Log ( "UseFirstAbility." );

		return 1;
	}

	public int UseSecondAbility ()
	{
		Debug.Log ( "UseSecondAbility." );

		return 1;
	}

	public int UseThirdAbility ()
	{
		Debug.Log ( "UseThirdAbility." );

		return 1;
	}

	public int UseSpecialAbility ()
	{
		Debug.Log ( "UseSpecialAbility." );

		return 1;
	}
	
	public bool MoveTo ( Tile destinationTile )
	{
		if ( destinationTile == null )
			return false;
		
		Debug.Log ( "Trying to move " + this.gameObject.name );
		
		try
		{
			if ( this.CurrentTile != null )
				this.CurrentTile.Unit = null;

			this.transform.position = destinationTile.transform.position + new Vector3 ( 0, destinationTile.vertOffset / 2, 0 );

			this.CurrentTile = destinationTile;

			this.CurrentTile.Unit = this;

			Debug.Log ( "Done moving " + this.gameObject.name );

			return true;
		} catch ( Exception )
		{
			return false;
		}
	}

}
