using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombineSymbols : MonoBehaviour
{

    public GameObject Slot1;

    public GameObject Slot2;

    public Text text;

    public GameObject SymbolPrefab;

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
            if (Slot1.GetComponentInChildren<IDHolder>().ID == Slot2.GetComponentInChildren<IDHolder>().ID)
            {
                text.text = "Invalid combination. The two syllables must be different.";
               
            }
            else
            {
                GameObject newSymbol = Instantiate(SymbolPrefab);
                newSymbol.transform.SetParent(transform);
                newSymbol.GetComponent<SymbolHandler>().SetSyllables(Slot1.transform.GetChild(0).gameObject, Slot2.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            text.text = "Invalid combination. There must be two syllables chosen.";
            
        }
    }

}
