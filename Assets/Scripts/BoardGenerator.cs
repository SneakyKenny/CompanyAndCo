using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BoardGenerator : MonoBehaviour
{
	public GameObject TilePrefab;

	public int BoardWidth, BoardHeight;

	public static List <Tile> tiles;

	private void Awake ()
	{
		tiles = new List <Tile> ();
		
		GenerateBoard ();
	}

	private void GenerateBoard ()
	{
		GameObject parent = Instantiate ( new GameObject () );
		parent.name = "BoardTiles";
		
		for ( int i = 0; i < this.BoardWidth; i++ )
		{
			for ( int j = 0; j < this.BoardHeight; j++ )
			{
				float height = Random.value;
				TilePrefab.transform.localScale = new Vector3 ( this.TilePrefab.transform.localScale.x, height, this.TilePrefab.transform.localScale.z );
				
				GameObject tile = Instantiate ( this.TilePrefab, new Vector3 ( i, height / 2, j ), Quaternion.identity );
				
				this.TilePrefab.transform.localScale = Vector3.one;

				tile.name = string.Format("Tile:({0}, {1})", i, j);
				
				Tile t = tile.AddComponent <Tile> ().GetComponent<Tile> ();

				t.x = i;
				t.y = j;
				t.vertOffset = height / 2;
				
				tile.transform.parent = parent.transform;

				tiles.Add ( t );
			}
		}

		parent.transform.parent = this.transform;
	}

}
