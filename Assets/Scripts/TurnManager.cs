using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

	public static TurnManager Instance;
	
	public List<Unit> Team1;
	public List<Unit> Team2;
	public List<Unit> turnOrder;
	private int currentUnit = -1;

	private void Awake ()
	{
		Instance = this;
	}


	void Start ()
	{
		StartGame ();
	}
	
	public void StartGame()
	{
		Team1 = Spawn_Control.Instance.Team1;
		Team2 = Spawn_Control.Instance.Team2;
		turnOrder = new List<Unit>();
		foreach (var c in Team1)
		{
			if (c != null)
			{
				int i = 0;
				int j = 0;
				while (i < j && c.speed < turnOrder[i].speed)
				{
					i++;
				}

				if (j == 0)
					turnOrder.Add(c);
				else
					turnOrder.Insert(i - 1,c);			
			}
		}		
		foreach (var c in Team2)
		{
			if (c != null)
			{
				int i = 0;
				int j = turnOrder.Count;
				while (i < j && c.speed < turnOrder[i].speed)
				{
					i++;
				}
				turnOrder.Insert(i, c);
				
			}
		}
		NextTurn();
	}

	
	public void NextTurn(string message = "")
	{
		if (Team1.Count > 0 && Team2.Count > 0)
		{
			foreach (var u in turnOrder)
			{
				u.stun--;
			}
			currentUnit++;
			if (Team1.Contains(turnOrder[currentUnit % turnOrder.Count]) && turnOrder[currentUnit % turnOrder.Count].stun <= 0)
				GameplayManager.Instance.UnitSelected = turnOrder[currentUnit % turnOrder.Count];
			else NextTurn();
		}
		//else finish game
	}
}
