using UnityEngine;
using System.Collections;
using System.Linq;
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

	public AudioSource ClickBtn, SwipeBtn, PokedexOpenBtn, PokedexMiddleBtn, PokedexCloseBtn;

	public bool PlayAmbience;

	public AudioMixerSnapshot[] NewThemeAmbienceSnapshots;

	public AudioClip FemaleNoSound;
	public AudioClip[] GibberishList;
	public AudioClip[] MaleSyllabusList;
	public AudioClip[] FemaleSyllabusList;
	
	private AudioSource[] _ambienceSounds;

	[Header("Left Center Right Signal Sounds")]
	private AudioSource[] _L;    	// left side signal sounds
	private AudioSource[] _C;		// center side signal sounds
	private AudioSource[] _R;		// right side signal sounds
	[Header("")]

	public int MinSnapShotTransition = 5;
	public int MaxSnapShotTransition = 20;
	public int MinSnapShotPlayingTime = 7;
	public int MaxSnapShotPlayingTime = 15;
	public int MinSignalSoundChange = 7;
	public int MaxSignalSoundChange = 15;

	[Header("Left Center Right Signal Sounds Min - Max")]
	public int LMinSec = 7;
	public int LMaxSec = 15;
	public int CMinSec = 7;
	public int CMaxSec = 15;
	public int RMinSec = 7;
	public int RMaxSec = 15;
	//[Header("")]


	[Header("Current playing signal sounds")]
	[SerializeField]
	private int _currentSnapshot = 0;
	//[SerializeField]
	//private int _currentSnapshotSeconds = 0;
	//[SerializeField]
	//private int _currentSignal = 0;
	[SerializeField]
	private int _currentLSignal = 0;
	[SerializeField]
	private int _currentCSignal = 0;
	[SerializeField]
	private int _currentRSignal = 0;
	//[SerializeField]
	//private int _currentSignalSeconds = 0;

	private AudioSource Player;
	private AudioSource SideKick;
    private DatabaseManager databaseManager;

	// Use this for initialization
	void Start () {

		if(GameObject.FindGameObjectWithTag("Player") != null)
		{
			Player = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
		}
		if(GameObject.FindGameObjectWithTag("SideKick") != null)
		{
			SideKick = GameObject.FindGameObjectWithTag("SideKick").GetComponent<AudioSource>();
		}
		if (GameObject.FindGameObjectWithTag (Constants.Tags.DatabaseManager) != null) {
			databaseManager = GameObject.FindGameObjectWithTag (Constants.Tags.DatabaseManager).GetComponent<DatabaseManager> ();
		}

		LoadSavedValues ();

		if(PlayAmbience)
		{
			PlayAmbienceMix();
		}
		else
		{
			StopAmbienceMix();
		}

//		Debug.Log (Application.loa);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAmbienceMix()
	{
		Transform Ambient = transform.FindChild ("Ambient");
		Transform SignalSounds = Ambient.FindChild("SignalSounds");
		_ambienceSounds = Ambient.FindChild("KeynoteSounds").GetComponentsInChildren<AudioSource>();
		_L = SignalSounds.FindChild("L").GetComponentsInChildren<AudioSource>();
		_C = SignalSounds.FindChild("C").GetComponentsInChildren<AudioSource>();
		_R = SignalSounds.FindChild("R").GetComponentsInChildren<AudioSource>();

		//Start random ambience sound
		foreach(AudioSource a in _ambienceSounds)
		{
			a.Play();
		}
		
		StartCoroutine (ThemeTransition());
		StartCoroutine (RandomLeftSignals());
		StartCoroutine (RandomRightSignals());
		StartCoroutine (RandomCenterSignals());
		PlayAmbience = true;
	}

	public void StopAmbienceMix()
	{
		PlayAmbience = false;
		StopAllCoroutines ();
	}

	private void LoadSavedValues()
	{
		// if it doesnt have "Sound" it doesnt have any of the sound keys set so set them all
		if(!PlayerPrefs.HasKey("Sound"))
		{
			PlayerPrefs.SetString("Sound", "On");
			PlayerPrefs.SetString("Music", "On");
			PlayerPrefs.SetFloat("AmbienceV", 0f);
			PlayerPrefs.SetString("Voices", "On");
			PlayerPrefs.SetFloat("VoicesV", 0f);
			PlayerPrefs.SetString("SoundFX", "On");
			PlayerPrefs.SetFloat("SFXV", 0f);
		}
		
		// checking saved states 
		if (PlayerPrefs.GetString ("Sound").Equals ("On")) {
			AudioListener.pause = false;
		} else {
			AudioListener.pause = true;
		}
		
		if (PlayerPrefs.GetString ("Music").Equals ("On")) {
			MasterMixer.SetFloat ("AmbienceV", PlayerPrefs.GetFloat("AmbienceV"));
		} else {
			//PlayerPrefs.SetFloat("AmbienceV", -80f);
			MasterMixer.SetFloat ("AmbienceV", -80f);
		}
		
		if (PlayerPrefs.GetString ("Voices").Equals ("On")) {
			MasterMixer.SetFloat ("VoicesV", PlayerPrefs.GetFloat("VoicesV"));
		} else {
			//PlayerPrefs.SetFloat("VoicesV", -80f);
			MasterMixer.SetFloat ("VoicesV", -80f);
		}
		
		if (PlayerPrefs.GetString ("SoundFX").Equals ("On")) {
			MasterMixer.SetFloat ("SFXV", PlayerPrefs.GetFloat("SFXV"));
		} else {
			//PlayerPrefs.SetFloat("SFXV", -80f);
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
			MasterMixer.SetFloat ("AmbienceV", 	PlayerPrefs.GetFloat("AmbienceV"));
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
			MasterMixer.SetFloat ("VoicesV", PlayerPrefs.GetFloat("VoicesV"));
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
			MasterMixer.SetFloat ("SFXV", PlayerPrefs.GetFloat("SFXV"));
			PlayerPrefs.SetString ("SoundFX", "On");
		}
		//Debug.Log (PlayerPrefs.GetString ("SoundFX"));
	}
	
	// Randomize left sided signal sounds 
	IEnumerator RandomLeftSignals()
	{
		int nextTransition = Random.Range (MinSignalSoundChange, MaxSignalSoundChange);
		//_currentSignalSeconds = nextTransition;
		yield return new WaitForSeconds (nextTransition);
		
		int randomAmbientSignal = Random.Range (0, _L.Length);
		while(_currentLSignal == randomAmbientSignal)
		{
			randomAmbientSignal = Random.Range (0, _L.Length);
			yield return null;
		}
		_currentLSignal = randomAmbientSignal;
		_L [randomAmbientSignal].Play ();
		
		StartCoroutine (RandomLeftSignals ());
	}

	// Randomize center sided signal sounds 
	IEnumerator RandomCenterSignals()
	{
		int nextTransition = Random.Range (MinSignalSoundChange, MaxSignalSoundChange);
		//_currentSignalSeconds = nextTransition;
		yield return new WaitForSeconds (nextTransition);
		
		int randomAmbientSignal = Random.Range (0, _C.Length);
		while(_currentCSignal == randomAmbientSignal)
		{
			randomAmbientSignal = Random.Range (0, _C.Length);
			yield return null;
		}
		_currentCSignal = randomAmbientSignal;
		_C [randomAmbientSignal].Play ();

		StartCoroutine (RandomCenterSignals ());
	}

	// Randomize right sided signal sounds 
	IEnumerator RandomRightSignals()
	{
		int nextTransition = Random.Range (MinSignalSoundChange, MaxSignalSoundChange);
		//_currentSignalSeconds = nextTransition;
		yield return new WaitForSeconds (nextTransition);
		
		int randomAmbientSignal = Random.Range (0, _R.Length);
		while(_currentRSignal == randomAmbientSignal)
		{
			randomAmbientSignal = Random.Range (0, _R.Length);
			yield return null;
		}
		_currentRSignal = randomAmbientSignal;
		_R [randomAmbientSignal].Play ();

		StartCoroutine (RandomRightSignals ());
	}

	IEnumerator ThemeTransition()
	{
		int randomTransition = Random.Range (5, 10);     // MIXING theme 01 to theme 04
		int randomSnap = Random.Range (0, NewThemeAmbienceSnapshots.Length);

		float[] weights = new float[NewThemeAmbienceSnapshots.Length];


		while (_currentSnapshot == randomSnap) 
		{
			randomSnap = Random.Range (0, NewThemeAmbienceSnapshots.Length);
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

		MusicMixer.TransitionToSnapshots (NewThemeAmbienceSnapshots, weights, randomTransition);

		int nextTransition = Random.Range (15, 25);		// transit from theme01 to theme04
		//_currentSnapshotSeconds = nextTransition;
		yield return new WaitForSeconds(nextTransition);

		StartCoroutine (ThemeTransition());

	}

	// male voices ------------------------------
	public void MaleSyllabusSoundPlay(int index)
	{
		Player.clip = MaleSyllabusList.Length > index ? MaleSyllabusList[index] : MaleSyllabusList.FirstOrDefault();
		Player.Play();
	}
	
	public void MaleSyllabusSoundStop(int index)
	{
		Player.clip = MaleSyllabusList [index];
		Player.Stop ();
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
	    SideKick.clip = FemaleSyllabusList.Length > index ? FemaleSyllabusList[index] : FemaleSyllabusList.FirstOrDefault();
		SideKick.Play();
	    
	    //FemaleSyllabusList[index].Play();
		//GetFemaleSyllabusByName (name).Play ();
	}
	
	public void FemaleSyllabusSoundStop(int index)
	{
		SideKick.clip = FemaleSyllabusList [index];
		SideKick.Stop ();
		//FemaleSyllabusList[index].Stop();
		//GetFemaleSyllabusByName (name).Stop ();
	}

    public void StartPlayMaleCoroutine(int id)
    {
		StartCoroutine(MaleSignPlay(id));
    }

	public void StartPlayFemaleCoroutine(int id)
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

	IEnumerator MaleSignPlay(int id)
	{
		if (databaseManager.GetSign(id) != null)
		{
			Sign s = databaseManager.GetSign(id);
			
			foreach (int i in s.SyllableSequence)
			{
				MaleSyllabusSoundPlay(i);
				yield return new WaitForSeconds(MaleSyllabusList[i].length);
			}
		}
		yield return new WaitForSeconds(1);
	}

	public void ClickBtnPlay()
	{
		ClickBtn.Play ();
	}

	public void ClickBtnStop()
	{
		ClickBtn.Stop ();
	}

	public void SwipeBtnPlay()
	{
		SwipeBtn.Play ();
	}

	public void SwipeBtnStop()
	{
		SwipeBtn.Stop ();
	}

	public void PokedexBtnOpenPlay()
	{
		PokedexOpenBtn.Play ();
	}

	public void PokedexBtnMiddlePlay()
	{
		PokedexMiddleBtn.Play ();
	}

	public void PokedexBtnClosePlay()
	{
		PokedexCloseBtn.Play ();
	}

	public void FemaleNoSoundPlay()
	{
		SideKick.clip = FemaleNoSound;
		SideKick.Play ();
	}

	public void SidekickRandomGibberishPlay()
	{
		SideKick.clip = GibberishList[Random.Range(0, GibberishList.Length)];
		SideKick.Play();
	}

	public void PlayerRandomGibberishPlay()
	{
		Player.clip = GibberishList[Random.Range(0, GibberishList.Length)];
		Player.Play();
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

	/*
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
	*/

	/*
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
	*/
}
