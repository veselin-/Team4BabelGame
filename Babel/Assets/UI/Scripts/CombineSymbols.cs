using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombineSymbols : MonoBehaviour
{

    public GameObject Slot1;

    public GameObject Slot2;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Combine()
    {
        if (Slot1.GetComponent<SentenceSlotHandler>().symbol && Slot2.GetComponent<SentenceSlotHandler>().symbol)
        {
            if (Slot1.GetComponentInChildren<IDHolder>().ID == Slot2.GetComponentInChildren<IDHolder>().ID)
            {
                GetComponentInChildren<Text>().text = "Invalid";
            }
            else
            {
                GetComponentInChildren<Text>().text = (Slot1.GetComponentInChildren<IDHolder>().ID + Slot2.GetComponentInChildren<IDHolder>().ID).ToString();
            }


        }
        else
        {
            GetComponentInChildren<Text>().text = "Invalid";
        }



    }

}
