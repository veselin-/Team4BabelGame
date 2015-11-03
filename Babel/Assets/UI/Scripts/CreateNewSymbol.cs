using UnityEngine;
using System.Collections;

public class CreateNewSymbol : MonoBehaviour
{

    public static int SymbolID = 0;

    private DatabaseManager db;

	// Use this for initialization
	void Awake () {

        db = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
        db.LoadData();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void NextSymbol()
    {
        SymbolID++;
    }
}
