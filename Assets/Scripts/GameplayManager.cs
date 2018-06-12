using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
	public static GameplayManager Instance;
	
	public Unit UnitSelected;

	private Tile SelectedTile;
	private bool isLookingForDestinationTile, isLookingForAttackTile;

	private bool UnitCanMove = true, UnitCanAttack = true;

	public GameObject WhatToDoCanvas, ActionCanvas, SpecialAttackCanvas;
	public Button MoveButton, AttackButton;

	private int attackType = -1;
	int attackId = -1;

	private Tile AttackedTile;
	
	string CACTION = String.Empty;

	private Client client;

	void Awake ()
	{
		Instance = this;


		client = FindObjectOfType <Client> ();
		
		
		StartTurn ();
	}

	private void Update ()
	{
		if( Input.GetMouseButtonDown ( 0 ) )
		{
			if ( this.isLookingForDestinationTile )
			{
				this.SelectedTile = SelectTile ();

				if ( this.SelectedTile != null && this.UnitSelected.MoveTo ( this.SelectedTile ) )
				{
					this.isLookingForDestinationTile = false;

					UnitCanMove = false;
					
					OnUnitDoneMoving ();
				}
			} else if ( this.isLookingForAttackTile )
			{
				this.SelectedTile = SelectTile ();

				if ( this.SelectedTile != null )
				{
					switch ( this.attackType )
					{
						// reminder : attackId takes values 0 >= 2: 0: attack failed, 1: attack succeeded, 2: attack was not on a valid target (empty tile for example)
						case -1:
							return;
						case 0:
							attackId = this.UnitSelected.Attack (SelectedTile);
							break;
						case 1:
							attackId = this.UnitSelected.UseFirstAbility (SelectedTile);
							break;
						case 2:
							attackId = this.UnitSelected.UseFirstAbility (SelectedTile);
							break;
						case 3:
							attackId = this.UnitSelected.UseFirstAbility (SelectedTile);
							break;
						case 4:
							attackId = this.UnitSelected.UseFirstAbility (SelectedTile);
							break;
						case 5:
							// TODO: Objects
							this.attackId = 0;
							break;
					}

					switch ( attackId )
					{
						case 0:
							Debug.Log ( "Unit failed to attack." );
							UnitCanAttack = false;
							AttackedTile = this.SelectedTile;
							OnUnitDoneAttacking ();
							break;
						case 1:
							Debug.Log ( "Unit succesfully attacked." );
							UnitCanAttack = false;
							AttackedTile = this.SelectedTile;
							OnUnitDoneAttacking ();
							break;
						case 2:
							Debug.Log ( "Selected Tile didn't contain an ennemy, therefore we couldn't attack it." );
							ShowActionMenu ();
							break;
					}
				}
			}
		}
		
		this.MoveButton.interactable = this.UnitCanMove;
		this.AttackButton.interactable = this.UnitCanAttack;

		if ( this.UnitSelected != null && Camera.main != null )
			Camera.main.transform.position = Vector3.Lerp ( Camera.main.transform.position, this.UnitSelected.transform.position + new Vector3 ( 5, 7.5f, -5 ) * 1.3f, 3 * Time.deltaTime );
	}

	public void EndTurn ()
	{
		Debug.Log ( "Turn ended." );

		CACTION += this.UnitSelected.CurrentTile.x + "|" + this.UnitSelected.CurrentTile.y + "|";

		if ( this.UnitCanAttack )
			CACTION += "NOTHING|0|0|0|0";
		else
		{
			switch ( this.attackType )
			{
				case 0:
					CACTION += "atk|" + this.AttackedTile.x + "|" + this.AttackedTile.y + "|" + this.attackId;
					break;
				case 1:
					CACTION += "atkspeone|" + this.AttackedTile.x + "|" + this.AttackedTile.y + "|" + this.attackId;
					break;
				case 2:
					CACTION += "atkspetwo|" + this.AttackedTile.x + "|" + this.AttackedTile.y + "|" + this.attackId;
					break;
				case 3:
					CACTION += "atkspethree|" + this.AttackedTile.x + "|" + this.AttackedTile.y + "|" + this.attackId;
					break;
				case 4:
					CACTION += "compspe|" + this.AttackedTile.x + "|" + this.AttackedTile.y + "|" + this.attackId;
					break;
				case 5:
					CACTION += "obj|" + this.AttackedTile.x + "|" + this.AttackedTile.y + "|" + this.attackId;
					break;
			}
		}
		
		client.Send ( this.CACTION );
		
		TurnManager.Instance.NextTurn ();
		
		StartTurn ();
	}

	private IEnumerator WaitforThingNotToBeNull ()
	{
		yield return new WaitForSeconds ( 1 );

		CACTION = "CACTION|" + this.UnitSelected.CurrentTile.x + "|" + this.UnitSelected.CurrentTile.y + "|";
	}
	
	public void StartTurn ()
	{
		if ( UnitSelected == null )
			StartCoroutine ( WaitforThingNotToBeNull () );
		else
			CACTION = "CACTION|" + this.UnitSelected.CurrentTile.x + "|" + this.UnitSelected.CurrentTile.y + "|";
		
		ShowWhatToDoMenu ();

		AttackedTile = null;
		
		ResetBoardTilesColor ();

		this.UnitCanMove = true;
		this.UnitCanAttack = true;

		isLookingForDestinationTile = false;
		isLookingForAttackTile = false;
	}

	public static void ResetBoardTilesColor ()
	{
		foreach ( Tile t in BoardGenerator.tiles )
		{
			t.GetComponent <Renderer> ().material.color = Color.white;
		}
	}
	
	Tile SelectTile ()
	{
		Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

		RaycastHit[] hits = Physics.RaycastAll ( ray );

		foreach ( var hit in hits )
		{
			if ( hit.collider.gameObject.GetComponent <Tile> () != null )
				return hit.collider.gameObject.GetComponent <Tile> ();
		}

		return null;
	}

	public void OnUnitDoneMoving ()
	{
		if ( this.UnitCanAttack )
			ShowWhatToDoMenu ();
		else
			EndTurn ();
	}

	public void OnUnitDoneAttacking ()
	{
		if ( this.UnitCanMove )
			ShowWhatToDoMenu ();
		else
			EndTurn ();
	}

	#region BUTTONS

	public void PASSBUTTON ()
	{
		EndTurn ();
	}

	public void MOVEBUTTON ()
	{
		if ( this.UnitCanMove )
		{
			this.isLookingForDestinationTile = true;
			UnitSelected.ShowTilesInMovementRange ();
		}
	}

	public void ACTIONBUTTON ()
	{
		if ( this.UnitCanAttack )
			ShowActionMenu ();
	}

	public void ATTACKBUTTON ()
	{
		if ( this.UnitCanAttack )
		{
			this.isLookingForAttackTile = true;

			this.attackType = 0;
		}
	}

	public void SPECIALATTACKBUTTON ()
	{
		ShowSpecialAttackMenu ();
	}

	public void SPECIALATTACKONEBUTTON ()
	{
		if ( this.UnitCanAttack )
		{
			this.isLookingForAttackTile = true;

			this.attackType = 1;
		}
	}

	public void SPECIALATTACKTWOBUTTON ()
	{
		if ( this.UnitCanAttack )
		{
			this.isLookingForAttackTile = true;

			this.attackType = 2;
		}
	}

	public void SPECIALATTACKTHREEBUTTON ()
	{
		if ( this.UnitCanAttack )
		{
			this.isLookingForAttackTile = true;

			this.attackType = 3;
		}
	}

	public void SPECIALABILITYBUTTON ()
	{
		if ( this.UnitCanAttack )
		{
			this.isLookingForAttackTile = true;

			this.attackType = 4;
		}
	}

	public void OBJECTBUTTON ()
	{
		Debug.Log ( "Trying to use an object." );
	}

	#endregion
	
	#region Menus

	private void ShowActionMenu ()
	{
		this.ActionCanvas.SetActive ( true );

		this.WhatToDoCanvas.SetActive ( false );
		this.SpecialAttackCanvas.SetActive ( false );
	}
	
	private void ShowWhatToDoMenu ()
	{
		this.WhatToDoCanvas.SetActive ( true );

		this.ActionCanvas.SetActive ( false );
		this.SpecialAttackCanvas.SetActive ( false );
	}
	
	private void ShowSpecialAttackMenu ()
	{
		this.SpecialAttackCanvas.SetActive ( true );

		this.WhatToDoCanvas.SetActive ( false );
		this.ActionCanvas.SetActive ( false );
	}

	#endregion
	
}
