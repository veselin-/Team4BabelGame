using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Core.Configuration;

public class CombineSymbols : MonoBehaviour
{

    public GameObject Slot1;

    public GameObject Slot2;

    public GameObject Slot3;

    public Text text;

    public GameObject SymbolPrefab;

    private GameObject databaseManager;

    // Use this for initialization

    public GameObject symbol
    {
        get
        {
            if (transform.childCount > 0)
            {

                return transform.GetChild(0).gameObject;

            }
            return null;
        }
    }



    void Start () {

        databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Combine()
        //If two different syllables are present in the two slots it should create a new prefab with the two syllables pictures and sounds. TYhen it should play the sounds in order.
    {
        //Destroy any preexisting symbols.
        if (symbol)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        List<int> syllableIDs = new List<int>();

        if(Slot1.GetComponent<SentenceSlotHandler>().symbol)
            syllableIDs.Add(Slot1.GetComponentInChildren<SyllableHandler>().ID);

        if (Slot2.GetComponent<SentenceSlotHandler>().symbol)
            syllableIDs.Add(Slot2.GetComponentInChildren<SyllableHandler>().ID);

        if (Slot3.GetComponent<SentenceSlotHandler>().symbol)
            syllableIDs.Add(Slot3.GetComponentInChildren<SyllableHandler>().ID);


        if (syllableIDs.Count < 2)
        {
            text.text = "Invalid combination. A sign must be at least two syllables.";
            return;
        }

        if (syllableIDs.Count == 2)
        {
            if (syllableIDs[0] == syllableIDs[1])
            {
                text.text = "Invalid combination. The syllables must be different.";
                return;
            }
        }

        if (syllableIDs.Count == 3)
        {
            if (syllableIDs[0] == syllableIDs[1] || syllableIDs[0] == syllableIDs[2] || syllableIDs[1] == syllableIDs[2])
            {
                text.text = "Invalid combination. The syllables must be different.";
                return;
            }
        }

        databaseManager.GetComponent<DatabaseManager>().AddSign(CreateNewSymbol.SymbolID, syllableIDs);
        databaseManager.GetComponent<DatabaseManager>().SaveSignsDB();

        GameObject newSymbol = Instantiate(SymbolPrefab);

        newSymbol.transform.SetParent(transform);

        newSymbol.GetComponent<SymbolHandler>().ID = CreateNewSymbol.SymbolID;

        newSymbol.GetComponent<SymbolHandler>().UpdateSymbol();


    }

}
