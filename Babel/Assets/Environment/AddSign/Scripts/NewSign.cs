using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class NewSign : MonoBehaviour
{

    public int ID;
    public Camera cam;
    private DatabaseManager databaseManager;
    private UiController _uiControl;

    void Start()
    {
        _uiControl = GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UiController>();
        databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
    }

    void Update()
    {
        if (databaseManager.GetSign(ID) != null)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            _uiControl.NewSignCreation(ID);
            Debug.Log("IM HERE");
        }
    }

    public void SetSignID()
    {
        CreateNewSymbol.SymbolID = ID;
    }
}


