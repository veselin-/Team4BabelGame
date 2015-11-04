using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class NewSign : MonoBehaviour
{

    public int ID;

    private DatabaseManager databaseManager;

    void Start()
    {
        databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
    }

    void Update()
    {
        if (databaseManager.GetSign(ID) != null)
        {
            this.gameObject.SetActive(false);
        }
    }


    public void SetSignID()
    {

        CreateNewSymbol.SymbolID = ID;

    }
}


