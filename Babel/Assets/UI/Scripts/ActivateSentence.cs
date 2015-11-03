using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;

public class ActivateSentence : MonoBehaviour
{

    public GameObject[] SentenceSlots;
    private SidekickControls _sidekick;


	// Use this for initialization
	void Start ()
	{
        var sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
	    if (sideKick == null) return;
	    _sidekick = sideKick.GetComponent<SidekickControls>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void testFunc()
    {
        var sentence = GetSentence();
        if(_sidekick == null) return;
        _sidekick.ReachToSentence(sentence);
    }

  public List<int> GetSentence()
   {

       List<int> sentence = new List<int>();

       foreach (GameObject slot in SentenceSlots)
       {
           if (slot.GetComponent<SentenceSlotHandler>().symbol)
           {
               sentence.Add(slot.transform.GetComponentInChildren<SymbolHandler>().ID);
           }
           else
           {
               sentence.Add(-1);
           }
       }
       Debug.Log(sentence[0]);
        Debug.Log(sentence[1]);
        Debug.Log(sentence[2]);
        Debug.Log(sentence[3]);
        return null;
   } 

}
