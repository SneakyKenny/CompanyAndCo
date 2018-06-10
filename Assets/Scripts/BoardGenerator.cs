using UnityEngine;
using UnityEngine.WSA;

public class BoardGenerator : MonoBehaviour
{

	public GameObject TilePrefab;

	public int BoardWidth, BoardHeight;
	
	void Start ()
	{
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

				tile.transform.parent = parent.transform;
			}
		}

		parent.transform.parent = this.transform;
	}

}
