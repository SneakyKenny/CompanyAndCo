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

	/*public void SendAction(string message)
	{
		if (currentUnit > 0)
		{
			bool wasturn = Team1.Contains(turnOrder[currentUnit % turnOrder.Count]);
			if (wasturn)
				Client.Instance.Send("Turn Finished");
			if(GameObject.Find("NetworkManager").GetComponent<NetworkManager>().isServer)
				Server.Instance.Transfer();
			if(!wasturn)
			{
				string newpos = Client.Instance.Receive();
				turnOrder[currentUnit % turnOrder.Count].transform.position = new Vector3(10 * newpos[0] + newpos[1], 1 , 10 * newpos[3] + newpos[4]);
			}
			
		}
	}*/
	
	public void NextTurn(string message = "")
	{
		/*if(message != "")
			SendAction(message);*/
		currentUnit++;
		if (Team1.Contains(turnOrder[currentUnit % turnOrder.Count]))
			GameplayManager.Instance.UnitSelected = turnOrder[currentUnit % turnOrder.Count];
		else NextTurn();
	}
}
