using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Core.Configuration;

public class InteractableSpeechBubble : MonoBehaviour {
	
	public Button PlayerSpeechBubble;
	public Button SideKickSpeechBubble;
	public Button NarrativeSpeechBubble;

	private Text _PlayerText;
	private Text _SideKickText;
	private Text _NarrativeText;
	
	private Vector2 _playerScreenPos;
	private Vector2 _sidekickScreenPos;

	private GameObject player;
	private	GameObject sidekick;

	public SpeechList speechList;  

	public List<SpeechHolder> narrative;
	public int ConversationCounter = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag (Constants.Tags.Player);
		sidekick = GameObject.FindGameObjectWithTag (Constants.Tags.SideKick);

		_PlayerText = PlayerSpeechBubble.transform.GetChild (0).GetComponent<Text>();
		_SideKickText = SideKickSpeechBubble.transform.GetChild (0).GetComponent<Text>();
		_NarrativeText = NarrativeSpeechBubble.transform.GetChild (0).GetComponent<Text>();

		_playerScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, player.transform.position);
		_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekick.transform.position);
		//Debug.Log (playerScreenPos);
		PlayerSpeechBubble.transform.position = _playerScreenPos;
		SideKickSpeechBubble.transform.position = _sidekickScreenPos;

		narrative = speechList.speechList;
		GetNextSpeech ();
	}
	
	// Update is called once per frame
	void Update () {
		_playerScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, player.transform.position);
		PlayerSpeechBubble.transform.position = _playerScreenPos;

		_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekick.transform.position);
		SideKickSpeechBubble.transform.position = _sidekickScreenPos;
	}

	public void GetNextSpeech()
	{
		if(ConversationCounter >= 0 && ConversationCounter < narrative.Count)
		{
			if (narrative [ConversationCounter].isNarrativeSpeechActive) {

				NarrativeSpeechBubble.gameObject.SetActive(true);
				_NarrativeText.text = narrative [ConversationCounter].NarrativeSpeech;
			} 
			else 
			{
				NarrativeSpeechBubble.gameObject.SetActive(false);
			}

			if(narrative[ConversationCounter].isPlayerSpeechActive)
			{
				PlayerSpeechBubble.gameObject.SetActive(true);
				_PlayerText.text = narrative[ConversationCounter].PlayerSpeech;
			}
			else 
			{
				PlayerSpeechBubble.gameObject.SetActive(false);
			}

			if(narrative[ConversationCounter].isSideKickSpeechActive)
			{
				SideKickSpeechBubble.gameObject.SetActive(true);
				_SideKickText.text = narrative[ConversationCounter].SideKickSpeech;
			}
			else 
			{
				SideKickSpeechBubble.gameObject.SetActive(false);
			}

			ConversationCounter++;
		}

	}
}
