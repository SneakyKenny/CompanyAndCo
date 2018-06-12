using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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
=======

public class BoardGenerator : MonoBehaviour
{
	public static BoardGenerator Instance;
	
	public GameObject TilePrefab;

	public static int StartWidth, StartHeight, BoardWidth, BoardHeight;

	public static List <Tile> tiles;

	void Awake ()
	{
		Instance = this;
		
		tiles = new List <Tile> ();
>>>>>>> Victor
	}

	public static void SetSize (int _startWidth, int _startHeight, int _boardWidth, int _boardHeight)
	{
		StartWidth = _startWidth;
		StartHeight = _startHeight;
		BoardWidth = _boardWidth;
		BoardHeight = _boardHeight;
	}

	public void GenerateBoard ()
	{
		GameObject parent = new GameObject ();
		parent.name = "BoardTiles";

		for ( int i = StartWidth; i < StartWidth + BoardWidth; i++ )
		{
			for ( int j = StartHeight; j < StartHeight + BoardHeight; j++ )
			{
				float height = Random.value;
				TilePrefab.transform.localScale = new Vector3 ( this.TilePrefab.transform.localScale.x, height, this.TilePrefab.transform.localScale.z );
				
				GameObject tile = Instantiate ( this.TilePrefab, new Vector3 ( i, height / 2, j ), Quaternion.identity );
				
				this.TilePrefab.transform.localScale = Vector3.one;

<<<<<<< HEAD
				tile.name = string.Format("Tile:({0}, {1})", i, j);
=======
				this.TilePrefab.transform.localScale = Vector3.one;

				tile.name = string.Format ( "Tile({0},{1})", i - StartWidth, j - StartHeight );
>>>>>>> Victor
				
				Tile t = tile.AddComponent <Tile> ().GetComponent<Tile> ();

				t.x = i;
				t.y = j;
<<<<<<< HEAD
				t.vertOffset = height / 2;
=======
				t.vertOffset = height;

				tiles.Add ( t );
>>>>>>> Victor
				
				tile.transform.parent = parent.transform;

				tiles.Add ( t );
			}
		}

		parent.transform.parent = this.transform;
	}

	public static int CoordToIndex (int x, int y)
	{
		return y + x * BoardHeight;
	}

	public static int[] IndexToCoord ( int index )
	{
		return new[] {index % BoardWidth, index / BoardWidth};
	}

	public static Unit GetUnitOnTile (Tile t)
	{
		return t.Unit;
	}

	public static Unit GetUnitAtCoord ( int x, int y )
	{
		return GetUnitAtIndex ( CoordToIndex ( x, y ) );
	}

	public static Unit GetUnitAtIndex ( int index )
	{
		return GetUnitOnTile ( tiles [index] );
	}
	
}
