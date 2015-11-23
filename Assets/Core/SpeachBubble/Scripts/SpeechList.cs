using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpeechHolder
{
    [TextArea]
    public string PlayerSpeech = "";
	public bool isPlayerSpeechActive = false;
    [TextArea]
    public string SideKickSpeech = "";
	public bool isSideKickSpeechActive = false;
    [TextArea]
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
