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



	    Signs = GameObject.FindGameObjectsWithTag(Constants.Tags.Sign);


        UpdateBook();


	}
	
	// Update is called once per frame
	void Update () {

        //This is baaaaaad!!!
        Signs = GameObject.FindGameObjectsWithTag(Constants.Tags.Sign);

    }


    public void UpdateBook()
    {
        foreach (GameObject s in Signs)
        {

            s.GetComponent<SymbolHandler>().UpdateSymbol();

        }
    }
}
