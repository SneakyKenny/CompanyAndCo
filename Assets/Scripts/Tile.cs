using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	public int x, y;

	public float vertOffset;

	public Unit Unit;

	public override string ToString ()
	{
		return string.Format ( "Tile({0},{1})", this.x, this.y );
	}

}
