using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
	public Unit UnitSelected;

	private Tile SelectedTile;
	private bool isLookingForDestinationTile = false;

	private void Update ()
	{
		if ( this.isLookingForDestinationTile && Input.GetMouseButtonDown ( 0 ) )
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

			if ( Physics.Raycast ( ray, out hit, Mathf.Infinity, 8 ) )
			{
				this.SelectedTile = hit.transform.GetComponent <Tile> ();

				this.isLookingForDestinationTile = false;
			}
		}
	}

	public void PASSBUTTON ()
	{
		
	}

	public void MOVEBUTTON ()
	{
		this.isLookingForDestinationTile = true;

		while ( this.SelectedTile == null || !Input.GetKeyDown ( KeyCode.Escape ) )
		{
			continue;
		}
		
		this.UnitSelected.MoveTo ( SelectedTile );

		this.SelectedTile = null;
	}

	public void ATTACKBUTTON ()
	{
		
	}
}
