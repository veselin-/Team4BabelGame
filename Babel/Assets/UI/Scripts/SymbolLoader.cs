using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class SymbolLoader : MonoBehaviour
{

    private DatabaseManager databaseManager;

    private GameObject[] Signs;

	// Use this for initialization
	void OnEnable ()
	{

	    databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();

	    Signs = GameObject.FindGameObjectsWithTag(Constants.Tags.Sign);

	    foreach (GameObject s in Signs)
	    {
	        if (databaseManager.GetSign(s.GetComponent<SymbolHandler>().ID) != null)
	        {
	            s.SetActive(true);
	        }
	        else
	        {
                s.SetActive(false);
            }
	    }


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
