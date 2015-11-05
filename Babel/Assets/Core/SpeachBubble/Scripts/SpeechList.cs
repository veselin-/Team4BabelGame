using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpeechHolder
{
	public enum BubbleSize
	{
		Small,
		Medium,
		Large
	}

	public BubbleSize bubbleSize;
	public string PlayerSpeech = "";
	public bool isPlayerSpeechActive = false;

	public string SideKickSpeech = "";
	public bool isSideKickSpeechActive = false;

	public string NarrativeSpeech = "";
	public bool isNarrativeSpeechActive = false;

	/*
	public string name = "gosho";
	public string testString;
	public float testFloat;
	public GameObject testGM;
	public Sprite testSprite;
	*/

}

public class SpeechList : ScriptableObject {


	public List<SpeechHolder> speechList; 

}
