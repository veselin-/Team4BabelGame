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
    public Text hintText, dicText;
    bool _pressed, _hold;
    float timeForHold = 0;
    private GameObject gameui;
    //DatabaseManager dm;
    //AudioManager am;

    // Use this for initialization
    void Start ()
	{
        var sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
	    if (sideKick == null) return;
	    _sidekick = sideKick.GetComponent<SidekickControls>();
	    isb = GameObject.FindGameObjectWithTag(Constants.Tags.SpeechCanvas).GetComponent<InteractableSpeechBubble>();
        gameui = GameObject.FindGameObjectWithTag("GameUI");
        //am = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();
        //dm = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_pressed)
        {
            timeForHold += Time.unscaledDeltaTime;
            if(timeForHold > 0.25 && UiController.hotbarOpen == 1)
            {
                ChangeHintText();
            }
        }
	}

    void ChangeHintText()
    {
        hintText.transform.position = transform.position + new Vector3(0, 50, 0);
        switch (gameObject.GetComponent<SymbolHandler>().ID)
        {
            // TODO 
            case 0:
                hintText.text = "A waving gesture for uniting people.";
                break;
            case 1:
                hintText.text = "If you tell a dog to sit it ***** put.";
                break;
            case 2:
                hintText.text = "Requires pulling.. activates mechanism..";
                break;
            case 3:
                hintText.text = "It’s brown and can be set on fire.";
                break;
            case 4:
                hintText.text = "It looks like a pan.. but I wouldn't eat anything from it!";
                break;
            case 5:
                hintText.text = "Give or take!"; 
                break;
            case 6:
                hintText.text = "It can contain several buckets of water, looks like a birdbath.";
                break;
            case 7:
                hintText.text = "An endless supply of water at your disposal!";
                break;
            case 8:
                hintText.text = "It’s tiny, it’s shiny.. but it’s the only means to get out!";
                break;
            case 9:
                hintText.text = "Can be opened with a certain shiny little object.";
                break;
            case 10:
                hintText.text = "Can be used to transport fluids.";
                break;
        }
        _hold = true;
    }

    void ChangeDicText()
    {
        switch (gameObject.GetComponent<SymbolHandler>().ID)
        {
            case 0:
                dicText.text = "A waving gesture for uniting people.";
                break;
            case 1:
                dicText.text = "If you tell a dog to sit it ***** put.";
                break;
            case 2:
                dicText.text = "Requires pulling.. activates mechanism..";
                break;
            case 3:
                dicText.text = "It’s brown and can be set on fire.";
                break;
            case 4:
                dicText.text = "It looks like a pan.. but I wouldn't eat anything from it!";
                break;
            case 5:
                dicText.text = "Can be used to transport fluids.";
                break;
            case 6:
                dicText.text = "It can contain several buckets of water, looks like a birdbath.";
                break;
            case 7:
                dicText.text = "An endless supply of water at your disposal!";
                break;
            case 8:
                dicText.text = "It’s tiny, it’s shiny.. but it’s the only means to get out!";
                break;
            case 9:
                dicText.text = "Can be opened with a certain shiny little object.";
                break;
            case 10:
                dicText.text = "Give or take!";
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("DOWN");
        _pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_hold && UiController.hotbarOpen == 1)
        {
            testFunc();
        }
        //Debug.Log("UP");
        if(UiController.hotbarOpen == 2)
        {
            ChangeDicText();
        }
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
        if (transform.GetChild(2).GetComponent<Image>().sprite == null && transform.GetChild(0).GetComponent<Image>().sprite == null && transform.GetChild(1).GetComponent<Image>().sprite == null)
        {
            return;
        }
        else
        {
            _sidekick.ExecuteAction(sentence.FirstOrDefault());
            isb.ActivatePlayerSignBubble(sentence);
            gameui.GetComponent<UiController>().PokedexClose();
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
