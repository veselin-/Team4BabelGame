﻿using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioManager : MonoBehaviour {

	public AudioMixer MasterMixer;
	public AudioMixer MusicMixer;
	public AudioMixer SoundFXMixer;
	public AudioMixer VoiceMixer;

	public AudioMixerSnapshot[] AmbientSnapshots;

	public AudioClip[] MaleSyllabusList;
	public AudioClip[] FemaleSyllabusList;

	public AudioSource[] SignalSounds;

	public int MinSnapShotTransition = 5;
	public int MaxSnapShotTransition = 20;
	public int MinSnapShotPlayingTime = 7;
	public int MaxSnapShotPlayingTime = 15;
	public int MinSignalSoundChange = 7;
	public int MaxSignalSoundChange = 15;

	[SerializeField]
	private int _currentSnapshot = 0;
	[SerializeField]
	private int _currentSnapshotSeconds = 0;
	[SerializeField]
	private int _currentSignal = 0;
	[SerializeField]
	private int _currentSignalSeconds = 0;

	private AudioSource Player;
    private DatabaseManager databaseManager;

	// Use this for initialization
	void Start () {

		if(GameObject.FindGameObjectWithTag("Player") != null)
		{
			Player = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		}

	    databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();

		LoadSavedValues ();

		//Start random ambience sound
		StartCoroutine (RandomAmbience());
		StartCoroutine (RandomAmbienceSignals());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void LoadSavedValues()
	{
		// if it doesnt have "Sound" it doesnt have any of the sound keys set so set them all
		if(!PlayerPrefs.HasKey("Sound"))
		{
			PlayerPrefs.SetString("Sound", "On");
			PlayerPrefs.SetString("Music", "On");
			PlayerPrefs.SetString("Voices", "On");
			PlayerPrefs.SetString("SoundFX", "On");
		}
		
		// checking saved states 
		if (PlayerPrefs.GetString ("Sound").Equals ("On")) {
			AudioListener.pause = false;
		} else {
			AudioListener.pause = true;
		}
		
		if (PlayerPrefs.GetString ("Music").Equals ("On")) {
			MasterMixer.SetFloat ("AmbienceV", 0f);
		} else {
			MasterMixer.SetFloat ("AmbienceV", -80f);
		}
		
		if (PlayerPrefs.GetString ("Voices").Equals ("On")) {
			MasterMixer.SetFloat ("VoicesV", 0f);
		} else {
			MasterMixer.SetFloat ("VoicesV", -80f);
		}
		
		if (PlayerPrefs.GetString ("SoundFX").Equals ("On")) {
			MasterMixer.SetFloat ("SFXV", 0f);
		} else {
			MasterMixer.SetFloat ("SFXV", -80f);
		}
	}

	public void SetSoundOnOff()
	{
		if (PlayerPrefs.GetString ("Sound").Equals ("On")) {
			AudioListener.pause = true;
			PlayerPrefs.SetString("Sound", "Off");
		} else {
			AudioListener.pause = false;
			PlayerPrefs.SetString("Sound", "On");
		}
		//Debug.Log (PlayerPrefs.GetString ("Sound"));
	}

	public void SetMusicOnOff()
	{
		if (PlayerPrefs.GetString ("Music").Equals ("On")) {
			MasterMixer.SetFloat ("AmbienceV", -80f);
			PlayerPrefs.SetString ("Music", "Off");
		} else {
			MasterMixer.SetFloat ("AmbienceV", 0f);
			PlayerPrefs.SetString ("Music", "On");
		}
		//Debug.Log (PlayerPrefs.GetString ("Music"));
	}

	public void SetVoicesOnOff()
	{
		if (PlayerPrefs.GetString ("Voices").Equals ("On")) {
			MasterMixer.SetFloat ("VoicesV", -80f);
			PlayerPrefs.SetString ("Voices", "Off");
		} else {
			MasterMixer.SetFloat ("VoicesV", 0f);
			PlayerPrefs.SetString ("Voices", "On");
		}
		//Debug.Log (PlayerPrefs.GetString ("Voices"));
	}

	public void SetSoundFXOnOff()
	{
		if (PlayerPrefs.GetString ("SoundFX").Equals ("On")) {
			MasterMixer.SetFloat ("SFXV", -80f);
			PlayerPrefs.SetString ("SoundFX", "Off");
		} else {
			MasterMixer.SetFloat ("SFXV", 0f);
			PlayerPrefs.SetString ("SoundFX", "On");
		}
		//Debug.Log (PlayerPrefs.GetString ("SoundFX"));
	}

	IEnumerator RandomAmbienceSignals()
	{
		int nextTransition = Random.Range (MinSignalSoundChange, MaxSignalSoundChange);
		_currentSignalSeconds = nextTransition;
		yield return new WaitForSeconds (nextTransition);

		int randomAmbientSignal = Random.Range (0, SignalSounds.Length);
		while(_currentSignal == randomAmbientSignal)
		{
			randomAmbientSignal = Random.Range (0, SignalSounds.Length);
			yield return null;
		}
		_currentSignal = randomAmbientSignal;
		SignalSounds [_currentSignal].Play ();

		StartCoroutine (RandomAmbienceSignals ());
	}

	IEnumerator RandomAmbience()
	{
		int randomTransition = Random.Range (MinSnapShotTransition, MaxSnapShotTransition);
		int randomSnap = Random.Range (0, AmbientSnapshots.Length);
		//AmbientSnapshots[0].name;

		float[] weights = new float[AmbientSnapshots.Length];

		while (_currentSnapshot == randomSnap) 
		{
			randomSnap = Random.Range (0, AmbientSnapshots.Length);
			yield return null;
		}

		weights [randomSnap] = 1f;
		_currentSnapshot = randomSnap;
		for(int i = 0; i < weights.Length; i++)
		{
			if(weights[i] != 1f)
			{
				weights[i] = 0f;
			}
		}


		MusicMixer.TransitionToSnapshots (AmbientSnapshots, weights, randomTransition);

		int nextTransition = Random.Range (MinSnapShotPlayingTime, MaxSnapShotPlayingTime);
		_currentSnapshotSeconds = nextTransition;
		yield return new WaitForSeconds(nextTransition);

		//randomTransition = Random.Range (MinSnapTransition, MaxSnapTransition);
		StartCoroutine (RandomAmbience ());

	}

	// male voices ------------------------------
	public void MaleSyllabusSoundPlay(int index)
	{
		//MaleSyllabusList [index].Play ();
		//GetMaleSyllabusByName (name).Play ();
	}
	
	public void MaleSyllabusSoundStop(int index)
	{
		//MaleSyllabusList [index].Stop ();
		//GetMaleSyllabusByName (name).Stop ();
	}

	/*
	private AudioSource GetMaleSyllabusByName(string name)
	{
		foreach(AudioSource s in MaleSyllabusList)
		{
			if(s.name.Equals(name))
			{
				return s;
			}
		}
		return null;
	}
	*/
	//--------------------------------------------

	// female voices -----------------------------
	public void FemaleSyllabusSoundPlay(int index)
	{
		Player.clip = FemaleSyllabusList [index];
		Player.Play ();
		//FemaleSyllabusList[index].Play();
		//GetFemaleSyllabusByName (name).Play ();
	}
	
	public void FemaleSyllabusSoundStop(int index)
	{
		Player.clip = FemaleSyllabusList [index];
		Player.Stop ();
		//FemaleSyllabusList[index].Stop();
		//GetFemaleSyllabusByName (name).Stop ();
	}

    public void StartPlayCoroutine(int id)
    {
        StartCoroutine(FemaleSignPlay(id));
    }

    IEnumerator FemaleSignPlay(int id)
    {
        if (databaseManager.GetSign(id) != null)
        {
            Sign s = databaseManager.GetSign(id);

            foreach (int i in s.SyllableSequence)
            {
                FemaleSyllabusSoundPlay(i);
                yield return new WaitForSeconds(FemaleSyllabusList[i].length);
            }
        }
        yield return new WaitForSeconds(1);


    }

	/*
	private AudioSource GetFemaleSyllabusByName(string name)
	{
		foreach(AudioSource s in FemaleSyllabusList)
		{
			if(s.name.Equals(name))
			{
				return s;
			}
		}
		return null;
	}
	*/
	//--------------------------------------------



}
