using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class SymbolLoader : MonoBehaviour
{

    private DatabaseManager databaseManager;

    private GameObject[] Signs;

	// Use this for initialization
	void Start ()
	{


	    databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();

        databaseManager.LoadData();

	    Signs = GameObject.FindGameObjectsWithTag(Constants.Tags.Sign);

	    foreach (GameObject s in Signs)
	    {

            s.GetComponent<SymbolHandler>().UpdateSymbol();

	    }


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
