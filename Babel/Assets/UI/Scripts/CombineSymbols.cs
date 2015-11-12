using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Core.Configuration;

public class CombineSymbols : MonoBehaviour
{

    //public GameObject Slot1;

    //public GameObject Slot2;

    //public GameObject Slot3;

    public Text text;

    //private GameObject SymbolPrefab;

    private GameObject databaseManager;

    private AudioManager audioManager;

    //NavMeshAgent navMesh;
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

        audioManager = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Combine()
        //If two different syllables are present in the two slots it should create a new prefab with the two syllables pictures and sounds. TYhen it should play the sounds in order.
    {
        //Destroy any preexisting symbols.
        //ClearCurrentSign();

        List<int> syllableIDs = new List<int>();
        //Debug.Log("CHILDCOUNT OF PARENT: " + transform.childCount);
        //Debug.Log("CHILDCOUNT OF CHILD1: " + transform.GetChild(0).childCount);
        //Debug.Log("CHILDCOUNT OF PARENT+CHILD1: " + (transform.childCount + transform.GetChild(0).childCount));
        //Debug.Log("CHILDCOUNT OF PARENT+CHILD1+CHILD2: " + (transform.childCount + transform.GetChild(0).childCount + transform.GetChild(0).transform.GetChild(0).childCount));
        if ((transform.childCount + transform.GetChild(0).childCount) >= 2)
        {
            if ((transform.childCount + transform.GetChild(0).childCount + transform.GetChild(0).transform.GetChild(0).childCount) == 3)
            {
                syllableIDs.Add(transform.GetChild(0).GetComponent<symbolPress>().ID);
                syllableIDs.Add(transform.GetChild(0).transform.GetChild(0).GetComponent<symbolPress>().ID);
                syllableIDs.Add(transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<symbolPress>().ID);

                transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<symbolPress>().MoveSignBackInBook();
                transform.GetChild(0).transform.GetChild(0).GetComponent<symbolPress>().MoveSignBackInBook();
                transform.GetChild(0).GetComponent<symbolPress>().MoveSignBackInBook();
                //Debug.Log("nej der er fukin 3");
            }
            else //if ((transform.childCount + transform.GetChild(0).childCount) == 2)
            {
                syllableIDs.Add(transform.GetChild(0).GetComponent<symbolPress>().ID);
                syllableIDs.Add(transform.GetChild(0).transform.GetChild(0).GetComponent<symbolPress>().ID);

                transform.GetChild(0).transform.GetChild(0).GetComponent<symbolPress>().MoveSignBackInBook();
                transform.GetChild(0).GetComponent<symbolPress>().MoveSignBackInBook();
                //Debug.Log("den tror der kun er 2");
            }
            databaseManager.GetComponent<DatabaseManager>().AddSign(CreateNewSymbol.SymbolID, syllableIDs);
            databaseManager.GetComponent<DatabaseManager>().SaveSignsDb();
            audioManager.StartPlayCoroutine(CreateNewSymbol.SymbolID);
        }
        else
        {
            text.text = "Invalid combination. A sign must be at least two syllables.";
          //  Debug.Log("BOGEN ÅBNER MEN VED IKKE HVORFOR");
            return;
        }



        //if (Slot1.GetComponent<SentenceSlotHandler>().symbol)
        //{
        //    syllableIDs.Add(Slot1.GetComponentInChildren<SyllableHandler>().ID);
        //    Destroy(Slot1.transform.GetChild(0).gameObject);
        //}

        //if (Slot2.GetComponent<SentenceSlotHandler>().symbol)
        //{
        //    syllableIDs.Add(Slot2.GetComponentInChildren<SyllableHandler>().ID);
        //    Destroy(Slot2.transform.GetChild(0).gameObject);
        //}

        //if (Slot3.GetComponent<SentenceSlotHandler>().symbol)
        //{
        //    syllableIDs.Add(Slot3.GetComponentInChildren<SyllableHandler>().ID);
        //    Destroy(Slot3.transform.GetChild(0).gameObject);
        //}



        //if (syllableIDs.Count < 2)
        //{
        //    text.text = "Invalid combination. A sign must be at least two syllables.";
        //    Debug.Log("IM HERE");
        //    return;
        //}

        //if (syllableIDs.Count == 2)
        //{
        //    if (syllableIDs[0] == syllableIDs[1])
        //    {
        //        text.text = "Invalid combination. The syllables must be different.";
        //        return;
        //    }
        //}

        //if (syllableIDs.Count == 3)
        //{
        //    if (syllableIDs[0] == syllableIDs[1] || syllableIDs[0] == syllableIDs[2] || syllableIDs[1] == syllableIDs[2])
        //    {
        //        text.text = "Invalid combination. The syllables must be different.";
        //        return;
        //    }
        //}

        //databaseManager.GetComponent<DatabaseManager>().AddSign(CreateNewSymbol.SymbolID, syllableIDs);
        //databaseManager.GetComponent<DatabaseManager>().SaveSignsDb();
        //audioManager.StartPlayCoroutine(CreateNewSymbol.SymbolID);
        //GameObject newSymbol = Instantiate(SymbolPrefab);


        //newSymbol.transform.SetParent(transform);

        //newSymbol.transform.localScale = Vector3.one;

        //newSymbol.GetComponent<SymbolHandler>().ID = CreateNewSymbol.SymbolID;

        //newSymbol.GetComponent<SymbolHandler>().UpdateSymbol();
    }

    public void ClearCurrentSign()
    {
        if (symbol)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        Time.timeScale = 1;
        //navMesh.Resume();
    }

}
