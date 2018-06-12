using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using JSONSTUFF;

public class Spawn_Control : MonoBehaviour
{

    public List<Unit> Team1;
    public List<Unit> Team2;
    public GameObject Player;
    public static Spawn_Control Instance;
    private bool start = false;

    private void Awake()
    {
        Instance = this;
    }


    void ParseTeam(List<Unit> Team, bool hero)
    {
        JSONElement T = JSON.ParseJSONFile((".\\" + (hero ? "Heroes" : "Foes")));
        int i = 0;
        if (T.key == null)
            throw new System.Exception("HELL NAH BRO");

        GameObject unitsContainer = new GameObject();
        unitsContainer.name = hero ? "Heroes" : "Foes";

        foreach (string name in T.key)
        {
            GameObject c = Instantiate(Player);
            c.name = name;
            c.transform.parent = unitsContainer.transform;
            Unit chara = c.GetComponent<Unit>();
            chara.c = JSON.SearchJSON(T.data[i], "class").string_value;
            chara.lvl = JSON.SearchJSON(T.data[i], "lvl").int_value;
            chara.hpMax = JSON.SearchJSON(T.data[i], "life").int_value;
            chara.currentHP = chara.hpMax;
            chara.attack = JSON.SearchJSON(T.data[i], "atk").int_value;
            chara.defense = JSON.SearchJSON(T.data[i], "def").int_value;
            chara.attackMental = JSON.SearchJSON(T.data[i], "matk").int_value;
            chara.defenseMental = JSON.SearchJSON(T.data[i], "mdef").int_value;
            chara.speed = JSON.SearchJSON(T.data[i], "spd").int_value;
            chara.stun = 0;
            chara.range = 1;           
            JSONElement Inv = JSON.SearchJSON(T.data[i], "Inventory");
            if (Inv != null)
            {
                int j = 0;
                foreach (string iname in Inv.key)
                {
                    string[] items = {"STICK", "COMPUTER", "CALC", "MONEY", "BROOM"};
                    if (items.Contains(iname))
                    {
                        chara.attack += 5 * JSON.SearchJSON(Inv.data[j], "lvl").int_value;
                        chara.attackMental += 5 * JSON.SearchJSON(Inv.data[j], "lvl").int_value;
                        j++;
                    }
                    if (iname == "STAPLER")
                    {
                        chara.attack += 5 * JSON.SearchJSON(Inv.data[j], "lvl").int_value;
                        chara.range += JSON.SearchJSON(Inv.data[j], "lvl").int_value;
                    }
                }
            }

            chara.AddAbility ( new Ability ( Ability.AbilityType.PHYS, "Regular strike", 1, chara.range, chara.attack ) );
            //chara.abilities.Add(new Ability((Ability.AbilityType.PHYS, "Poweful strike", 3, chara.attack / 3)));
            chara.AddAbility ( new Ability ( Ability.AbilityType.MEN, "Regular insult", 1, 1, chara.attackMental ) );
            //chara.abilities.Add(new Ability((Ability.AbilityType.PHYS, "Poweful strike", 3, chara.attack / 3)));
            switch (chara.c)
            {
                    case "STAG":
                        chara.range = 2;
                        break;
                    case "SEC":
                        chara.range = 3;
                        break;
                    case "TECH":
                        chara.range = 5;
                        break;
                    case "GUARD":
                        chara.range = 2;
                        break;
                    case "ING":
                        chara.range = 4;
                        break;
                    case "COUNT":
                        chara.range = 2;
                        break;
                    case "MAN":
                        chara.range = 3;
                        break;
                    default :
                        chara.range = 2;
                        break;
            }
            Team.Add(chara);
            i++;
        }
    }


    public void SpawnPlayers()
    {

        ParseTeam(Team1,true);
        ParseTeam(Team2,false);

        foreach (Unit c in Team1)
        {
            if (c != null)
            {
<<<<<<< HEAD
                c.MoveTo(BoardGenerator.tiles[Random.Range(0, BoardGenerator.tiles.Count)]);
=======
                c.GetComponent<Unit>().MoveTo(BoardGenerator.tiles[Random.Range(0, BoardGenerator.tiles.Count)], true);
>>>>>>> 9d5ae4b0d25f2f707db171e729c692b92aae122f
            }
        }

        foreach (Unit c in Team2)
        {
            if (c != null)
            {
<<<<<<< HEAD
                c.MoveTo(BoardGenerator.tiles[Random.Range(0, BoardGenerator.tiles.Count)]);
=======
                c.GetComponent<Unit>().MoveTo(BoardGenerator.tiles[Random.Range(0, BoardGenerator.tiles.Count)], true);
>>>>>>> 9d5ae4b0d25f2f707db171e729c692b92aae122f
            }
        }
    }

    void Start()
    {
        BoardGenerator.SetSize(0, 0, 50, 50);
        BoardGenerator.Instance.GenerateBoard();
        SpawnPlayers();
        
    }
}
        

