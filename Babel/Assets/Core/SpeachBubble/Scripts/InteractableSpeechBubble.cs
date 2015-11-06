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

	public SpeechList speechList;  

	public List<SpeechHolder> narrative;
	private int _ConversationCounter = 0;

	private bool _hasSpeech = false;
	public Vector2 bubbleOffset;

    private int _wordCounter = 0;
	public int wordsForNewLine = 10;

    public RectTransform PlayerSignBubble;
    public float PlayerSignBubbleStayTime = 5f;
    public GameObject SignPrefab;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag (Constants.Tags.Player);
		sidekick = GameObject.FindGameObjectWithTag (Constants.Tags.SideKick);

		_PlayerText = PlayerSpeechBubble.transform.GetChild (0).GetComponent<Text>();
		_SideKickText = SideKickSpeechBubble.transform.GetChild (0).GetComponent<Text>();
		_NarrativeText = NarrativeSpeechBubble.transform.GetChild (0).GetComponent<Text>();

		_playerScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, player.transform.position);
		_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekick.transform.position);

		PlayerSpeechBubble.transform.position = _playerScreenPos;
		SideKickSpeechBubble.transform.position = _sidekickScreenPos;

		narrative = speechList.speechList;
		GetNextSpeech ();

	}
	
	// Update is called once per frame
	void Update () {

		if(!_hasSpeech)
		{
			return;
		}

		Vector3 playerOffset = player.transform.position + new Vector3(bubbleOffset.x, bubbleOffset.y, 0);
		_playerScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, playerOffset);
		PlayerSpeechBubble.transform.position = _playerScreenPos;
        PlayerSignBubble.transform.position = _playerScreenPos;

        Vector3 sidekickOffset = sidekick.transform.position + new Vector3(bubbleOffset.x, bubbleOffset.y, 0);
		_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekickOffset);
		SideKickSpeechBubble.transform.position = _sidekickScreenPos;
	}

	private void RandomBubblePos()
	{
		int rand = Random.Range (0, 1);
		switch(rand)
		{
		case 0:
			bubbleOffset.x = -bubbleOffset.x; 
			Debug.Log(bubbleOffset);
			break;
		case 1:
			bubbleOffset.x = -bubbleOffset.x;
			Debug.Log(bubbleOffset);
			break;
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
		if (_ConversationCounter >= 0 && _ConversationCounter < narrative.Count) {
			if (narrative [_ConversationCounter].isNarrativeSpeechActive) {

				NarrativeSpeechBubble.gameObject.SetActive (true);
				string tempText = narrative [_ConversationCounter].NarrativeSpeech;
				tempText = AddNewLineToNarrative(tempText);
				_NarrativeText.text = tempText;
			} else {
				NarrativeSpeechBubble.gameObject.SetActive (false);
			}

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
        PlayerSignBubble.gameObject.SetActive(true);

        foreach (int i in ids)
        {
            GameObject nSign = Instantiate(SignPrefab);
            nSign.transform.SetParent(PlayerSignBubble);
            nSign.GetComponent<SymbolHandler>().ID = i;
            nSign.GetComponent<SymbolHandler>().UpdateSymbol();
        }

        StartCoroutine(signBubbleTimer());

    }

    IEnumerator signBubbleTimer()
    {
        
        yield return new WaitForSeconds(PlayerSignBubbleStayTime);

        for (int i = PlayerSignBubble.childCount; i > 0; i--)
        {
            Destroy(PlayerSignBubble.GetChild(i-1).gameObject);
        }

        PlayerSignBubble.gameObject.SetActive(false);

    }

}
