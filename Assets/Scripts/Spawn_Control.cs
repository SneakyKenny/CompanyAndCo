using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JSONSTUFF;

public class Spawn_Control : MonoBehaviour
{

    public static Spawn_Control Instance;
    
    public List<GameObject> Team1 = new List<GameObject>();
    public List<GameObject> Team2 = new List<GameObject>();
    public GameObject Player;

    void ParseTeam(List<GameObject> Team, bool hero)
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
            chara.hpMax = JSON.SearchJSON(T.data[i], "life").int_value;
            chara.currentHP = chara.hpMax;
            chara.attack = JSON.SearchJSON(T.data[i], "atk").int_value;
            chara.defense = JSON.SearchJSON(T.data[i], "def").int_value;
            chara.attackMental = JSON.SearchJSON(T.data[i], "matk").int_value;
            chara.defenseMental = JSON.SearchJSON(T.data[i], "mdef").int_value;
            chara.speed = JSON.SearchJSON(T.data[i], "spd").int_value;
            chara.stun = 0;
            chara.range = 0;
            JSONElement Inv = JSON.SearchJSON(T.data[i], "Inventory");
            if(Inv != null)
            {
                int j = 0;
                foreach(string iname in Inv.key)
                {
                    string[] items = {"STICK", "COMPUTER", "CALC", "MONEY", "BROOM"};
                    if(items.Contains(iname))
                    {
                        chara.attack += 5 * JSON.SearchJSON(Inv.data[j], "lvl").int_value; 
                        chara.attackMental += 5 * JSON.SearchJSON(Inv.data[j], "lvl").int_value; 
                        j++;
                    }
                    if(iname == "STAPLER")
                    {
                        chara.attack += 5 * JSON.SearchJSON(Inv.data[j], "lvl").int_value; 
                        chara.range += JSON.SearchJSON(Inv.data[j], "lvl").int_value;
                    }
                }
            }
            Team.Add(c);
            i++;
        }          
    }

    void Start()
    {
        Instance = this;
        
        BoardGenerator.SetSize ( 0, 0, 50, 50 );
        
        BoardGenerator.Instance.GenerateBoard ();
        
        ParseTeam(Team1, true);
        ParseTeam(Team2, false);
        
        foreach (GameObject c in Team1)
        {
            if ( c != null )
            {
                c.GetComponent <Unit> ().MoveTo ( BoardGenerator.tiles [Random.Range ( 0, BoardGenerator.tiles.Count )] );
            }
            //c.transform.position = new Vector3 ( Random.Range ( 0, 50 ), 1.5f, 1 );
        }
        
        foreach (GameObject c in Team2)
        {
            if ( c != null )
            {
                c.GetComponent <Unit> ().MoveTo ( BoardGenerator.tiles [Random.Range ( 0, BoardGenerator.tiles.Count )] );
            }
            //c.transform.position = new Vector3(Random.Range ( 0, 50), 1.5f, 50);
        }
    }
}
