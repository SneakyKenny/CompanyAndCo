using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

	private void Start ()
	{
		Instance = this;
		
		this.UnitSelected = FindObjectOfType <Unit> ();
				
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
						case -1:
							return;
						case 0:
							this.UnitSelected.Attack ();
							break;
						case 1:
							this.UnitSelected.UseFirstAbility ();
							break;
						case 2:
							this.UnitSelected.UseSecondAbility ();
							break;
						case 3:
							this.UnitSelected.UseThirdAbility ();
							break;
						case 4:
							this.UnitSelected.UseSpecialAbility ();
							break;
						case 5:
							// TODO: Objects
							break;
					}

					UnitCanAttack = false;
					
					OnUnitDoneAttacking ();
				}
			}
		}
		
		this.MoveButton.interactable = this.UnitCanMove;
		this.AttackButton.interactable = this.UnitCanAttack;
	}

	public void EndTurn ()
	{
		Debug.Log ( "Turn ended." );
		//TODO: Go to the next Unit.
		//By the way, do we do it here or do we do it in the StartTurn method ?
		
		StartTurn ();
	}

	public void StartTurn ()
	{
		ShowWhatToDoMenu ();

		this.UnitCanMove = true;
		this.UnitCanAttack = true;

		isLookingForDestinationTile = false;
		isLookingForAttackTile = false;
	}

	Tile SelectTile ()
	{
		Tile t = null;

		Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

		RaycastHit[] hits = Physics.RaycastAll ( ray );

		foreach ( RaycastHit hit in hits )
		{
			GameObject objHit = hit.collider.transform.gameObject;

			if ( objHit.layer == 8 )
				t = objHit.GetComponent <Tile> ();
		}

		return t;
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
			this.isLookingForDestinationTile = true;
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
