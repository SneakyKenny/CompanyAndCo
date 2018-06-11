using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

	public static TurnManager Instance;
	
	List<GameObject> Team1;
	List<GameObject> Team2;
	List<GameObject> turnOrder = new List<GameObject>();
	private int currentUnit = -1;

	public static Unit CurrentlyusedUnit;

	private void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		Debug.Log ( "(✧෴✧)" );
		
		Team1 = Spawn_Control.Instance.Team1;
		Team2 = Spawn_Control.Instance.Team2;

		foreach (var c in Team1)
		{
			int i = 0;
			int j = 0;
			while (i < j && c.GetComponent<Unit>().speed < turnOrder[i].GetComponent<Unit>().speed)
			{
				i++;
			}
			if (j == 0)
				turnOrder.Add(c);
			else
				turnOrder.Insert(i - 1,c);
		}
		
		foreach (var c in Team2)
		{
			int i = 0;
			int j = turnOrder.Count;
			while (i < j && c.GetComponent<Unit>().speed < turnOrder[i].GetComponent<Unit>().speed)
			{
				i++;
			}
			turnOrder.Insert(i, c);
		}

		CurrentlyusedUnit = this.turnOrder[0].GetComponent<Unit> ();
		
		NextTurn ();
	}

	public void NextTurn()
	{
		currentUnit++;
		
		GameplayManager.Instance.UnitSelected = turnOrder[currentUnit % turnOrder.Count].GetComponent<Unit> ();
	}
}
