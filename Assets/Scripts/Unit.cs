<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> Victor
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
<<<<<<< HEAD
	public int hpMax, currentHP, attack, attackMental, defense, defenseMental, speed, stun;
	
	//public Character.CharacterClass c;
	//public List<Items> Inventory;
=======
	public int lvl, hpMax, currentHP, attack, attackMental, defense, defenseMental, speed, stun, range;
	public string c;
>>>>>>> Victor
	
	public List <Ability> abilities;
	public Ability SpecialAbility;
	
	public Tile CurrentTile;

<<<<<<< HEAD
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
=======
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

	public int Attack (Tile tile)
	{
		//Debug.Log ( "Attack." );
		int tileIndex = BoardGenerator.CoordToIndex ( tile.x, tile.y);

		if(tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count)
			if ( BoardGenerator.tiles [tileIndex].GetComponent <Renderer> ().material.color != Color.red )
				return 0;
		if (tile.Unit != null)
		{
			Debug.Log(tile.Unit.currentHP);
			tile.Unit.currentHP -= (GameplayManager.Instance.UnitSelected.attack - tile.Unit.defense) < 0? 1 : (GameplayManager.Instance.UnitSelected.attack - tile.Unit.defense);
			Debug.Log(tile.Unit.currentHP);
			if (tile.Unit.currentHP <= 0)
			{
				if (TurnManager.Instance.Team1.Contains(tile.Unit))
					TurnManager.Instance.Team1.Remove(tile.Unit);
				else TurnManager.Instance.Team2.Remove(tile.Unit);
				TurnManager.Instance.turnOrder.Remove(tile.Unit);
				tile.Unit.transform.localScale.Set(1, 0.25f, 1);
			}
			return 1;
		}
		return 1;
	}

	public int UseFirstAbility (Tile tile)
	{
		Debug.Log ( "UseFirstAbility." );
		int tileIndex = BoardGenerator.CoordToIndex ( tile.x, tile.y);

		if(tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count)
			if ( BoardGenerator.tiles [tileIndex].GetComponent <Renderer> ().material.color != Color.red )
				return 0;
		if (tile.Unit != null)
		{
			Debug.Log(tile.Unit.currentHP);
			tile.Unit.currentHP -= (GameplayManager.Instance.UnitSelected.attackMental - tile.Unit.defenseMental) < 0? 1 : (GameplayManager.Instance.UnitSelected.attackMental - tile.Unit.defenseMental);
			Debug.Log(tile.Unit.currentHP);
			if (tile.Unit.currentHP <= 0)
			{
				if (TurnManager.Instance.Team1.Contains(tile.Unit))
					TurnManager.Instance.Team1.Remove(tile.Unit);
				else TurnManager.Instance.Team2.Remove(tile.Unit);
				TurnManager.Instance.turnOrder.Remove(tile.Unit);
				tile.Unit.transform.localScale.Set(1, 0.25f, 1);
			}
			return 1;
		}
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

	public int UseSpecialAbility (Tile tile)
	{
		Debug.Log ( "UseSpecialAbility." );
		SpecialAbility.effect.ApplyEffect(tile);
		return 1;
	}
	
	public bool MoveTo ( Tile destinationTile, bool isGameInit = false )
	{
		if ( destinationTile == null )
			return false;

		if ( destinationTile == this.CurrentTile )
			return false;

		if ( destinationTile.Unit != null )
			return false;

		try
		{
			if ( this.CurrentTile != null )
				this.CurrentTile.Unit = null;

			if ( !isGameInit )
			{
				int dx = destinationTile.x - ( this.CurrentTile == null ? 0 : this.CurrentTile.x );
				int dy = destinationTile.y - ( this.CurrentTile == null ? 0 : this.CurrentTile.y );

				int tileIndex = BoardGenerator.CoordToIndex ( this.CurrentTile.x + dx, this.CurrentTile.y + dy );

				if ( tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count )
				{
					if ( Mathf.Abs ( dx ) + Mathf.Abs ( dy ) <= this.range )
						this.destinationPosition = destinationTile.transform.position + new Vector3 ( 0, destinationTile.vertOffset / 2, 0 );
					else
					if ( BoardGenerator.tiles [tileIndex].GetComponent <Renderer> ().material.color != Color.cyan )
						return false;
				}
			}
			else
				transform.position = destinationTile.transform.position + new Vector3 ( 0, destinationTile.vertOffset / 2, 0 );

			this.CurrentTile = destinationTile;

			this.CurrentTile.Unit = this;

			GameplayManager.ResetBoardTilesColor ();
			
			return true;
		} catch ( Exception )
		{
			return false;
		}
	}

	public void ShowTilesInMovementRange ()
	{
		for ( int i = -this.range; i <= this.range ; i++ )
		{
			for ( int j = -this.range; j <= this.range; j++ )
			{
				if ( Mathf.Abs ( i ) + Mathf.Abs ( j ) <= range )
				{
					int tileIndex = BoardGenerator.CoordToIndex ( this.CurrentTile.x + i, this.CurrentTile.y + j );
					if ( tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count)
						BoardGenerator.tiles [tileIndex].gameObject.GetComponent <Renderer> ().material.color = Color.cyan;
				}
			}
		}
	}

	public void ShowAttackRange(bool phys)
	{
		for ( int i = -this.abilities[phys?0:1].range ; i <= this.abilities[phys?0:1].range ; i++ )
		{
			for ( int j = -this.abilities[phys?0:1].range ; j <= this.abilities[phys?0:1].range; j++ )
			{
				if ( Mathf.Abs(i) + Mathf.Abs(j) <= this.abilities[phys?0:1].range )
				{
					int tileIndex = BoardGenerator.CoordToIndex ( this.CurrentTile.x + i, this.CurrentTile.y + j );
					if ( tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count )
						BoardGenerator.tiles [tileIndex].gameObject.GetComponent<Renderer> ().material.color = Color.red;
				}
			}
		}
	}

	public void ShowAbilityRange()
	{
		for ( int i = -SpecialAbility.range ; i <= SpecialAbility.range ; i++ )
		{
			for ( int j = -SpecialAbility.range ; j <= SpecialAbility.range; j++ )
			{
				if ( Mathf.Abs(i) + Mathf.Abs(j) <= SpecialAbility.range )
				{
					int tileIndex = BoardGenerator.CoordToIndex ( this.CurrentTile.x + i, this.CurrentTile.y + j );
					if ( tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count )
						BoardGenerator.tiles [tileIndex].gameObject.GetComponent<Renderer> ().material.color = Color.green;
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
>>>>>>> Victor
	}

}
