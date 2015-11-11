using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class SymbolLoader : MonoBehaviour
{

    private DatabaseManager databaseManager;

    [SerializeField]
    private GameObject[] Signs;

    

	// Use this for initialization
	void Start ()
	{


	    databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();



	    Signs = GameObject.FindGameObjectsWithTag(Constants.Tags.Sign);


        UpdateBook();
        //  Debug.Log("Updated book");


    }
	
	// Update is called once per frame
	void Update () {

        //This is baaaaaad!!!
     //   Signs = GameObject.FindGameObjectsWithTag(Constants.Tags.Sign);

    }


    public void UpdateBook()
    {
        foreach (GameObject s in Signs)
        {
           // Debug.Log("Updated book");
            s.GetComponent<SymbolHandler>().UpdateSymbol();
        }

    }

    public void AnimateSignBook(GameObject go)
    {
        go.GetComponent<Animator>().SetTrigger("SignBookEnter");
        foreach (GameObject s in Signs)
        {
            s.GetComponent<SymbolHandler>().ResetSigns();
        }
        Time.timeScale = 0;
    }
}
