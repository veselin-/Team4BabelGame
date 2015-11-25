using UnityEngine;
using Assets.Core.Configuration;

public class NewSign : MonoBehaviour
{

    public int ID;
    public Camera cam;
    private DatabaseManager databaseManager;
    private UiController _uiControl;
    //private UiController _uiControl;

    void Start()
    {
        _uiControl = GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UiController>();
        databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
    }

    void Update()
    {
        if (databaseManager.GetSign(ID) != null)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != Constants.Tags.Player) return;

        _uiControl.NewSignCreation(ID);
        Destroy(gameObject);
    }
}


