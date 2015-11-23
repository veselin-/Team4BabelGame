using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine.EventSystems;

public class ActivateSentence : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //public GameObject[] SentenceSlots;
    private SidekickControls _sidekick;
    //public int ID;
    private InteractableSpeechBubble isb;
    public Text hintText;
    bool _pressed, _hold;
    float timeForHold = 0;
    //DatabaseManager dm;
    //AudioManager am;

    // Use this for initialization
    void Start ()
	{
        var sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
	    if (sideKick == null) return;
	    _sidekick = sideKick.GetComponent<SidekickControls>();
	    isb = GameObject.FindGameObjectWithTag(Constants.Tags.SpeechCanvas).GetComponent<InteractableSpeechBubble>();
        //am = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();
        //dm = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_pressed)
        {
            timeForHold += Time.unscaledDeltaTime;
            if(timeForHold > 0.25)
            {
                ChangeHintText();
            }
        }
	}

    void ChangeHintText()
    {
        hintText.transform.position = transform.position + new Vector3(0, 5, 0);
        hintText.text = "WTF HER ER DIT MATAFAKAN HINT";
        Debug.Log("sdgh klsdghkl sdgkl ksdhgkl s");
        _hold = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("DOWN");
        _pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_hold)
        {
            testFunc();
        }
        Debug.Log("UP");
        hintText.text = "";
        hintText.transform.position = new Vector3(100, 100, 100);
        timeForHold = 0;
        _pressed = false;
        _hold = false;
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
            //am.StartPlayCoroutine(ID);
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
        //if (SentenceSlots[0].transform.childCount == 1)
        //{
        //    sentence.Add(SentenceSlots[0].transform.GetComponentInChildren<SymbolHandler>().ID);
        //    SentenceSlots[0].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        //}
        //if (SentenceSlots[1].transform.childCount == 1)
        //{
        //    sentence.Add(SentenceSlots[1].transform.GetComponentInChildren<SymbolHandler>().ID);
        //    SentenceSlots[1].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        //}
        //if (SentenceSlots[2].transform.childCount == 1)
        //{
        //    sentence.Add(SentenceSlots[2].transform.GetComponentInChildren<SymbolHandler>().ID);
        //    SentenceSlots[2].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        //}
        //if (SentenceSlots[3].transform.childCount == 1)
        //{
        //    sentence.Add(SentenceSlots[3].transform.GetComponentInChildren<SymbolHandler>().ID);
        //    SentenceSlots[3].transform.GetComponentInChildren<SymbolHandler>().ResetSigns();
        //}
        //   Debug.Log("SLOT CHILD COUNT: " + SentenceSlots[0].transform.childCount);
        //    Debug.Log("sentence[0]: " + sentence[0]);
        //if
        //Debug.Log(sentence[0]);
        // Debug.Log(sentence[1]);
        // Debug.Log(sentence[2]);
        // Debug.Log(sentence[3]);

        sentence.Add(gameObject.GetComponent<SymbolHandler>().ID);

        isb.ActivatePlayerSignBubble(sentence);
        return sentence;
   }

    //public void SetSyllables(string syl1, string syl2)
    //{
    //    Image1.sprite = dm.GetImage(syl1);
    //    Image1.color = Color.white;
    //    Image2.sprite = dm.GetImage(syl2);
    //    Image2.color = Color.white;
    //    Image3.color = Color.clear;
    //}

    //public void SetSyllables(string syl1, string syl2, string syl3)
    //{
    //    Image1.sprite = dm.GetImage(syl1);
    //    Image1.color = Color.white;
    //    Image2.sprite = dm.GetImage(syl2);
    //    Image2.color = Color.white;
    //    Image3.sprite = dm.GetImage(syl3);
    //    Image3.color = Color.white;
    //}

    //public void UpdateSymbol()
    //{
    //    Sign s = dm.GetSign(ID);
    //    if (s == null)
    //    {
    //        SetSyllables(null, null, null);
    //        Image1.color = Color.clear;
    //        Image2.color = Color.clear;
    //        Image3.color = Color.clear;
    //        return;
    //    }
    //    if (s.SyllableSequence.Count == 2)
    //    {
    //        Syllable s1 = dm.GetSyllable(s.SyllableSequence[0]);
    //        Syllable s2 = dm.GetSyllable(s.SyllableSequence[1]);
    //        SetSyllables(s1.ImageName, s2.ImageName);
    //    }
    //    else if (s.SyllableSequence.Count == 3)
    //    {
    //        Syllable s1 = dm.GetSyllable(s.SyllableSequence[0]);
    //        Syllable s2 = dm.GetSyllable(s.SyllableSequence[1]);
    //        Syllable s3 = dm.GetSyllable(s.SyllableSequence[2]);
    //        SetSyllables(s1.ImageName, s2.ImageName, s3.ImageName);
    //    }
    //}
}
