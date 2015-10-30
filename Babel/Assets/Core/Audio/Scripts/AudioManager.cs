using UnityEngine;
using System.Collections;
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

	public AudioSource[] MaleSyllabusList;
	public AudioSource[] FemaleSyllabusList;

	public AudioSource[] AmbienceSignals;

	public int MinSnapTransition = 1;
	public int MaxSnapTransition = 3;
	public int MinSnapPlayingTime = 10;
	public int MaxSnapPlayingTime = 20;
	public int MinSignalChange = 10;
	public int MaxSignalChange = 20;

	[SerializeField]
	private int _currentSnapshot = 0;
	private int _currentSignal = 0;
	// Use this for initialization
	void Start () {

		//int randomTransition = Random.Range (MinSnapTransition, MaxSnapTransition);
		StartCoroutine (RandomAmbience());
		StartCoroutine (RandomAmbienceSignals());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator RandomAmbienceSignals()
	{
		int nextTransition = Random.Range (MinSignalChange, MaxSignalChange);
		yield return new WaitForSeconds (nextTransition);

		int randomAmbientSignal = Random.Range (0, AmbienceSignals.Length);
		while(_currentSignal == randomAmbientSignal)
		{
			randomAmbientSignal = Random.Range (0, AmbienceSignals.Length);
			yield return null;
		}
		_currentSignal = randomAmbientSignal;
		AmbienceSignals [_currentSignal].Play ();

		StartCoroutine (RandomAmbienceSignals ());
	}

	IEnumerator RandomAmbience()
	{
		int randomTransition = Random.Range (MinSnapTransition, MaxSnapTransition);
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

		int nextTransition = Random.Range (MinSnapPlayingTime, MaxSnapPlayingTime);
		yield return new WaitForSeconds(nextTransition);

		//randomTransition = Random.Range (MinSnapTransition, MaxSnapTransition);
		StartCoroutine (RandomAmbience ());

	}

	// male voices ------------------------------
	public void MaleSyllabusSoundPlay(string name)
	{
		GetMaleSyllabusByName (name).Play ();
	}
	
	public void MaleSyllabusSoundStop(string name)
	{
		GetMaleSyllabusByName (name).Stop ();
	}

	public AudioSource GetMaleSyllabusByName(string name)
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
	//--------------------------------------------

	// female voices -----------------------------
	public void FemaleSyllabusSoundPlay(string name)
	{
		GetFemaleSyllabusByName (name).Play ();
	}
	
	public void FemaleSyllabusSoundStop(string name)
	{
		GetFemaleSyllabusByName (name).Stop ();
	}
	
	public AudioSource GetFemaleSyllabusByName(string name)
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
	//--------------------------------------------



}
