using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;

public class ActivateSentence : MonoBehaviour
{

    public GameObject[] SentenceSlots;
    private SidekickControls _sidekick;

    private InteractableSpeechBubble isb;


	// Use this for initialization
	void Start ()
	{
        var sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
	    if (sideKick == null) return;
	    _sidekick = sideKick.GetComponent<SidekickControls>();

	    isb = GameObject.FindGameObjectWithTag(Constants.Tags.SpeechCanvas).GetComponent<InteractableSpeechBubble>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void testFunc()
    {
        var sentence = GetSentence();
        if(_sidekick == null) return;
        if (sentence.Count == 0)
        {
            return;
        }
        else
        {
            _sidekick.ExecuteAction(sentence.FirstOrDefault());
        }
        
    }

  public List<int> GetSentence()
   {

       List<int> sentence = new List<int>();

       //foreach (GameObject slot in SentenceSlots)
       //{
       //    if (slot.GetComponent<SentenceSlotHandler>().symbol)
       //    {
       //        sentence.Add(slot.transform.GetComponentInChildren<SymbolHandler>().ID);
       //         Destroy(slot.transform.GetChild(0).gameObject, 2f);
       //    }
       //    else
       //    {
       //       // sentence.Add(-1);
       //    }
       //}
        if (SentenceSlots[0].transform.childCount == 1)
        {
            sentence.Add(SentenceSlots[0].transform.GetComponentInChildren<SymbolHandler>().ID);
            SentenceSlots[0].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        }
        if (SentenceSlots[1].transform.childCount == 1)
        {
            sentence.Add(SentenceSlots[1].transform.GetComponentInChildren<SymbolHandler>().ID);
            SentenceSlots[1].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        }
        if (SentenceSlots[2].transform.childCount == 1)
        {
            sentence.Add(SentenceSlots[2].transform.GetComponentInChildren<SymbolHandler>().ID);
            SentenceSlots[2].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        }
        if (SentenceSlots[3].transform.childCount == 1)
        {
            sentence.Add(SentenceSlots[3].transform.GetComponentInChildren<SymbolHandler>().ID);
            SentenceSlots[3].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        }
     //   Debug.Log("SLOT CHILD COUNT: " + SentenceSlots[0].transform.childCount);
    //    Debug.Log("sentence[0]: " + sentence[0]);
        //if
        //Debug.Log(sentence[0]);
        // Debug.Log(sentence[1]);
        // Debug.Log(sentence[2]);
        // Debug.Log(sentence[3]);



        isb.ActivatePlayerSignBubble(sentence);
        return sentence;
   } 

}
