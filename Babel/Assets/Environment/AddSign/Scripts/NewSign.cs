using UnityEngine;
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

    public void SetSignID()
    {
        CreateNewSymbol.SymbolID = ID;
    }
}


