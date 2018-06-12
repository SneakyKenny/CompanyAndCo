using System.IO;
using UnityEngine;

public class PDG_Effect : AbilityEffect {

	public PDG_Effect()
	{
		
	}
	
	public override void ApplyEffect(Tile tile)
	{
		Unit user = GameplayManager.Instance.UnitSelected;
		for (int i = 0; i < 1 + user.lvl; i++)
		{
			Unit inter = Component.Instantiate(Spawn_Control.Instance.Player).GetComponent<Unit>();
			inter.c = "INTER";
			inter.name = File.ReadAllLines(".\\name_list.txt")[Random.Range(0, 100)];
			inter.lvl = user.lvl;
			int stat = 20 * inter.lvl;
			inter.hpMax = stat;
			inter.currentHP = inter.hpMax;
			inter.attack = stat;
			inter.defense = stat;
			inter.attackMental = stat;
			inter.defenseMental = stat;
			inter.speed = 70 * user.lvl;
			inter.range = 3;
			GameObject o = new GameObject("Inter " + i);
			if (TurnManager.Instance.Team1.Contains(user))
				TurnManager.Instance.Team1.Add(inter);
			else TurnManager.Instance.Team2.Add(inter);
			TurnManager.Instance.turnOrder.Add(inter);			
			int temp = i;
			int newx = user.CurrentTile.x + (i >= 2 ? 1 : -1);
			int newy = user.CurrentTile.y + (i % 2 == 0 ? -1 : 1);
			inter.MoveTo(BoardGenerator.tiles[BoardGenerator.CoordToIndex(newx, newy)], true);
		}
	}
}
