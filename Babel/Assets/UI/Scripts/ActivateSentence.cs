using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateSentence : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void testFunc()
    {
        GetSentence();
    }

  public List<int> GetSentence()
   {

       GameObject[] symbols = GameObject.FindGameObjectsWithTag("SentenceSlot");

       List<int> sentence = new List<int>();

       foreach (GameObject slot in symbols)
       {
           if (slot.GetComponent<SentenceSlotHandler>().symbol)
           {
               sentence.Add(slot.transform.GetComponentInChildren<Transform>().GetSiblingIndex());
           }
           else
           {
               sentence.Add(-1);
           }
       }
       Debug.Log(sentence[0]);
        Debug.Log(sentence[1]);
        Debug.Log(sentence[2]);
        return null;
   } 

}
