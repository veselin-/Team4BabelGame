using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Core.Configuration;
using System.Text.RegularExpressions;

public class InteractableSpeechBubble : MonoBehaviour {
	
	public RectTransform PlayerSpeechBubble;
	public RectTransform SideKickSpeechBubble;
	public RectTransform NarrativeSpeechBubble;


    private Text _PlayerText;
	private Text _SideKickText;
	private Text _NarrativeText;
	
	private Vector2 _playerScreenPos;
	private Vector2 _sidekickScreenPos;

	private GameObject player;
	private	GameObject sidekick;

	public SpeechList speechListEnglish;
    public SpeechList speechListDanish;

    public List<SpeechHolder> narrativeEnglish;
    public List<SpeechHolder> narrativeDanish;
    private int _ConversationCounter = 0;

	private bool _hasSpeech = false;
	public Vector2 bubbleOffset;

    private int _wordCounter = 0;
	public int wordsForNewLine = 10;

    public RectTransform PlayerSignBubble;
    public RectTransform SidekickSignBubble;

    public float PlayerSignBubbleStayTime = 5f;
    public GameObject SignPrefab;

    // Use this for initialization
    void Start () {

		if (GameObject.FindGameObjectWithTag (Constants.Tags.Player)) {
			player = GameObject.FindGameObjectWithTag (Constants.Tags.Player);
			_playerScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, player.transform.position);
			//PlayerSpeechBubble.transform.position = _playerScreenPos;
		}

		if (GameObject.FindGameObjectWithTag (Constants.Tags.SideKick)) {
			sidekick = GameObject.FindGameObjectWithTag (Constants.Tags.SideKick);
			_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekick.transform.position);
			SideKickSpeechBubble.transform.position = _sidekickScreenPos;
		}

		_PlayerText = PlayerSpeechBubble.transform.GetChild (0).GetComponent<Text>();
		_SideKickText = SideKickSpeechBubble.transform.GetChild (0).GetComponent<Text>();
		_NarrativeText = NarrativeSpeechBubble.transform.GetChild (0).GetComponent<Text>();

        narrativeEnglish = speechListEnglish.speechList;
        narrativeDanish = speechListDanish.speechList;
		//GetNextSpeech ();

	}
	
	// Update is called once per frame
	void Update () {

		if(!_hasSpeech)
		{
			return;
		}

		if (player != null) {
			Vector3 playerOffset = player.transform.position + new Vector3 (bubbleOffset.x, bubbleOffset.y, 0);
			_playerScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, playerOffset);
			//PlayerSpeechBubble.transform.position = _playerScreenPos;
			PlayerSignBubble.transform.position = _playerScreenPos;
		}

		if(sidekick != null){
	        Vector3 sidekickOffset = sidekick.transform.position + new Vector3(bubbleOffset.x, bubbleOffset.y, 0);
			_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekickOffset);
			//SideKickSpeechBubble.transform.position = _sidekickScreenPos;
		    SidekickSignBubble.transform.position = _sidekickScreenPos;
		}
	}
	

	private string AddNewLines(string text)
	{
		_wordCounter = 0;
		for(int i = 0; i < text.Length; i++)
		{
			if(char.IsWhiteSpace(text[i]))
			{
				_wordCounter++;
			}
			if(_wordCounter == wordsForNewLine)
			{
				text = text.Remove(i, 1);
				text = text.Insert(i, "\n");
				_wordCounter = 0;
			}
		}
		return text;
	}

	private string AddNewLineToNarrative(string text)
	{
		_wordCounter = 0;
		for(int i = 0; i < text.Length; i++)
		{
			if(char.IsWhiteSpace(text[i]))
			{
				_wordCounter++;
			}
			if(_wordCounter == 15)
			{
				text = text.Remove(i, 1);
				text = text.Insert(i, "\n");
				_wordCounter = 0;
			}
		}
		return text;
	}
	public void GetNextSpeech()
	{
        List<SpeechHolder> narrative = PlayerPrefs.GetString("Language").Equals(Constants.Languages.Danish) ? narrativeDanish : narrativeEnglish;

        if (_ConversationCounter >= 0 && _ConversationCounter < narrative.Count) {

			// Narrative speech bubble
			if (narrative [_ConversationCounter].isNarrativeSpeechActive) {

				NarrativeSpeechBubble.gameObject.SetActive (true);
				string tempText = narrative [_ConversationCounter].NarrativeSpeech;
				tempText = AddNewLineToNarrative(tempText);
				_NarrativeText.text = tempText;
			} else {
				NarrativeSpeechBubble.gameObject.SetActive (false);
			}

			// Bubble speech bubble
			if (narrative [_ConversationCounter].isPlayerSpeechActive) {
				
				PlayerSpeechBubble.gameObject.SetActive (true);
				string tempText =  narrative [_ConversationCounter].PlayerSpeech;
				tempText = AddNewLines(tempText);
				_PlayerText.text = tempText;
			} else {
				PlayerSpeechBubble.gameObject.SetActive (false);
			}

			/*
			if (narrative [_ConversationCounter].isPlayerSpeechActive) {

				PlayerSpeechBubble.gameObject.SetActive (true);
				string tempText =  narrative [_ConversationCounter].PlayerSpeech;
				tempText = AddNewLines(tempText);
				_PlayerText.text = tempText;
			} else {
				PlayerSpeechBubble.gameObject.SetActive (false);
			}

			if (narrative [_ConversationCounter].isSideKickSpeechActive) {

				SideKickSpeechBubble.gameObject.SetActive (true);
				string tempText =  narrative [_ConversationCounter].SideKickSpeech;
				tempText = AddNewLines(tempText);
				_SideKickText.text = tempText;
			} else {
				SideKickSpeechBubble.gameObject.SetActive (false);
			}
			*/
			_hasSpeech = true;
			_ConversationCounter++;
			//RandomBubblePos();


		} else {
			NarrativeSpeechBubble.gameObject.SetActive (false);
			PlayerSpeechBubble.gameObject.SetActive (false);
			SideKickSpeechBubble.gameObject.SetActive (false);
			_hasSpeech = false;
		}

	}

    public void ActivatePlayerSignBubble(List<int> ids)
    {
        

        for (int i = PlayerSignBubble.childCount; i > 0; i--)
        {
            Destroy(PlayerSignBubble.GetChild(i - 1).gameObject);
        }

        PlayerSignBubble.gameObject.SetActive(true);

        foreach (int i in ids)
        {
            GameObject nSign = Instantiate(SignPrefab);
            nSign.transform.SetParent(PlayerSignBubble);
            nSign.GetComponent<SymbolHandler>().ID = i;
            nSign.GetComponent<SymbolHandler>().UpdateSymbol();
        }

        StartCoroutine(SignBubbleTimer(PlayerSignBubble));
    }

    public void ActivateSidekickSignBubble(List<int> ids)
    {

        for (int i = SidekickSignBubble.childCount; i > 0; i--)
        {
            Destroy(SidekickSignBubble.GetChild(i - 1).gameObject);
        }

        SidekickSignBubble.gameObject.SetActive(true);

        foreach (int i in ids)
        {
            GameObject nSign = Instantiate(SignPrefab);
            nSign.transform.SetParent(SidekickSignBubble);
            nSign.GetComponent<SymbolHandler>().ID = i;
            nSign.GetComponent<SymbolHandler>().UpdateSymbol();
        }

        StartCoroutine(SignBubbleTimer(SidekickSignBubble));
    }

    IEnumerator SignBubbleTimer(RectTransform PlayerSignBubble)
    {
        
        yield return new WaitForSeconds(PlayerSignBubbleStayTime);

        for (int i = PlayerSignBubble.childCount; i > 0; i--)
        {
            Destroy(PlayerSignBubble.GetChild(i-1).gameObject);
        }

        PlayerSignBubble.gameObject.SetActive(false);

    }

}
