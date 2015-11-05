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

	private bool _hasSpeech = false;
	public Vector2 bubbleOffset;
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


		//bubble resize depending on the text length
		Debug.Log ("Image  " + PlayerSpeechBubble.image.rectTransform.rect.width);
		Debug.Log ("Text  " +_PlayerText.rectTransform.rect.width);
		//_PlayerText.h
		float textWidth = _PlayerText.rectTransform.rect.width + 10;
		Rect imageWidth = PlayerSpeechBubble.image.rectTransform.rect;
		imageWidth.width = textWidth;
		//PlayerSpeechBubble.image.rectTransform.rect.width = textWidth;

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

		Vector3 sidekickOffset = sidekick.transform.position + new Vector3(bubbleOffset.x, bubbleOffset.y, 0);
		_sidekickScreenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, sidekickOffset);
		SideKickSpeechBubble.transform.position = _sidekickScreenPos;

		//Debug.Log (_PlayerText.);

		/*
		Debug.Log ("Text  " +_PlayerText.rectTransform.rect.width);
		float textWidth = _PlayerText.rectTransform.rect.width + 10;
		Rect imageWidth = PlayerSpeechBubble.image.rectTransform.rect;
		imageWidth.width = textWidth;
		*/
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

	public void GetNextSpeech()
	{
		if (ConversationCounter >= 0 && ConversationCounter < narrative.Count) {
			if (narrative [ConversationCounter].isNarrativeSpeechActive) {

				NarrativeSpeechBubble.gameObject.SetActive (true);
				_NarrativeText.text = narrative [ConversationCounter].NarrativeSpeech;
			} else {
				NarrativeSpeechBubble.gameObject.SetActive (false);
			}

			if (narrative [ConversationCounter].isPlayerSpeechActive) {
				PlayerSpeechBubble.gameObject.SetActive (true);
				_PlayerText.text = narrative [ConversationCounter].PlayerSpeech;
			} else {
				PlayerSpeechBubble.gameObject.SetActive (false);
			}

			if (narrative [ConversationCounter].isSideKickSpeechActive) {
				SideKickSpeechBubble.gameObject.SetActive (true);
				_SideKickText.text = narrative [ConversationCounter].SideKickSpeech;
			} else {
				SideKickSpeechBubble.gameObject.SetActive (false);
			}
			_hasSpeech = true;
			ConversationCounter++;
			//RandomBubblePos();
		} else {
			NarrativeSpeechBubble.gameObject.SetActive (false);
			PlayerSpeechBubble.gameObject.SetActive (false);
			SideKickSpeechBubble.gameObject.SetActive (false);
			_hasSpeech = false;
		}

	}
}
