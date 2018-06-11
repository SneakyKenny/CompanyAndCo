using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	
	List<GameObject> Team1;
	List<GameObject> Team2;
	List<GameObject> turnOrder = new List<GameObject>();
	private GameplayManager manager = GameplayManager.Instance;
	private int currentUnit = -1;
	
	// Use this for initialization
	
	void Start ()
	{
		Team1 = GameObject.Find("GameObject").GetComponent<Spawn_Control>().Team1;
		Team2 = GameObject.Find("GameObject").GetComponent<Spawn_Control>().Team2;		
		foreach (var c in Team1)
		{
			int i = 0;
			while (i < turnOrder.Count)
			{
				if (c.GetComponent<Unit>().speed < turnOrder[i].GetComponent<Unit>().speed)
					turnOrder.Insert(i, c);
				i++;
			}
		}
		foreach (var c in Team2)
		{
			int i = 0;
			while (i < turnOrder.Count)
			{
				if (c.GetComponent<Unit>().speed < turnOrder[i].GetComponent<Unit>().speed)
					turnOrder.Insert(i, c);
				i++;
			}
		}
	}
	
	void NextTurn()
	{
		currentUnit++;
		manager.UnitSelected = turnOrder[currentUnit % turnOrder.Count].GetComponent<Unit>();
		//Faire le truc avec le gameplayManager
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
