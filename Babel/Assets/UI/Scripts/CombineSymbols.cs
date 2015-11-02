using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Core.Configuration;

public class CombineSymbols : MonoBehaviour
{

    public GameObject Slot1;

    public GameObject Slot2;

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


        if (Slot1.GetComponent<SentenceSlotHandler>().symbol && Slot2.GetComponent<SentenceSlotHandler>().symbol)
        {
            if (Slot1.GetComponentInChildren<SyllableHandler>().ID == Slot2.GetComponentInChildren<SyllableHandler>().ID)
            {
                text.text = "Invalid combination. The two syllables must be different.";
               
            }
            else
            {
                GameObject newSymbol = Instantiate(SymbolPrefab);
                newSymbol.transform.SetParent(transform);
               // newSymbol.GetComponent<SymbolHandler>().SetSyllables(Slot1.transform.GetChild(0).gameObject, Slot2.transform.GetChild(0).gameObject);
                newSymbol.GetComponent<SymbolHandler>().PlaySound();
                List<int> SyllableSequence = new List<int> { Slot1.GetComponentInChildren<SyllableHandler>().ID , Slot2.GetComponentInChildren<SyllableHandler>().ID };
                databaseManager.GetComponent<DatabaseManager>().AddWord(CreateNewSymbol.SymbolID, SyllableSequence);
                databaseManager.GetComponent<DatabaseManager>().SaveWordsDB();

            }
        }
        else
        {
            text.text = "Invalid combination. There must be two syllables chosen.";
            
        }
    }

}
