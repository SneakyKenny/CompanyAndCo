using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public int lvl, hpMax, currentHP, attack, attackMental, defense, defenseMental, speed, stun, range;
	public string c;
	
	public List <Ability> abilities;
	public Ability SpecialAbility;
	
	public Tile CurrentTile;

	private Vector3 destinationPosition;

	private void Awake ()
	{
		this.abilities = new List <Ability> ();
	}

	private void Start ()
	{
		MoveTo ( this.CurrentTile );

		this.destinationPosition = this.transform.position;
	}

	public void AddAbility (Ability ability)
	{
		this.abilities.Add ( ability );
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
	
	public bool MoveTo ( Tile destinationTile, bool isGameInit = false )
	{
		if ( destinationTile == null )
			return false;

		try
		{
			if ( this.CurrentTile != null )
				this.CurrentTile.Unit = null;

			if ( !isGameInit )
			{
				int dx = destinationTile.x - ( this.CurrentTile == null ? 0 : this.CurrentTile.x );
				int dy = destinationTile.y - ( this.CurrentTile == null ? 0 : this.CurrentTile.y );

				if ( Math.Abs(dx + dy) > this.range ) //the tile is too far away
					return false;
			}

			if ( isGameInit )
				transform.position = destinationTile.transform.position + new Vector3 ( 0, destinationTile.vertOffset / 2, 0 );
			else
				this.destinationPosition = destinationTile.transform.position + new Vector3 ( 0, destinationTile.vertOffset / 2, 0 );

			this.CurrentTile = destinationTile;

			this.CurrentTile.Unit = this;

			return true;
		} catch ( Exception )
		{
			return false;
		}
	}

	public void ShowTilesInMovementRange ()
	{
		for ( int i = -this.range / 2 - 1; i <= this.range / 2 + 1; i++ )
		{
			for ( int j = -this.range / 2 - 1; j <= this.range / 2 + 1; j++ )
			{
				if ( Mathf.Abs(i + j) <= this.range )
				{
					int tileIndex = BoardGenerator.CoordToIndex ( this.CurrentTile.x + i, this.CurrentTile.y + j );
					BoardGenerator.tiles [tileIndex].gameObject.GetComponent<Renderer> ().material.color = Color.cyan;
				}
			}
		}
	}

	void Update ()
	{
		if ( ( this.destinationPosition - transform.position ).magnitude > .05f )
			transform.position = Vector3.Lerp ( transform.position, this.destinationPosition, 2 * Time.deltaTime );
	}

	public override string ToString ()
	{
		return string.Format ( "{0} ({1})", this.name, this.c );
	}

}
