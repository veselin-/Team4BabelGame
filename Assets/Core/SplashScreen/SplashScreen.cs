using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public enum SceneName
	{
		DadiuSplashScreen,
		UnitySplashScreen,
		TeamSplashScreen,
		MainMenu
	}

	public SceneName NextSplashScreen;

	public float SplashScreenStay = 2f;
	// Use this for initialization
	void Start () {
	
		if (NextSplashScreen == SceneName.DadiuSplashScreen) {
			StartCoroutine (LoadSplashScreen(NextSplashScreen.ToString ()));
		} 
		else if (NextSplashScreen == SceneName.UnitySplashScreen) 
		{
			StartCoroutine (LoadSplashScreen(NextSplashScreen.ToString ()));
		}
		else if (NextSplashScreen == SceneName.TeamSplashScreen) 
		{
			StartCoroutine (LoadSplashScreen(NextSplashScreen.ToString ()));
		}
		else if (NextSplashScreen == SceneName.MainMenu) 
		{
			StartCoroutine (LoadSplashScreen(NextSplashScreen.ToString ()));
		}
	}
	
	IEnumerator LoadSplashScreen(string sceneName)
	{
		yield return new WaitForSeconds (SplashScreenStay);
		Application.LoadLevel (sceneName);
	}

	public void SkipScreen()
	{
		Application.LoadLevel (NextSplashScreen.ToString());
	}
}

// Async load
/*
public string levelName;
AsyncOperation async;

public void StartLoading() {
	StartCoroutine("load");
}

IEnumerator load() {
	Debug.LogWarning("ASYNC LOAD STARTED - " +
	                 "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
	async = Application.LoadLevelAsync(levelName);
	async.allowSceneActivation = false;
	yield return async;
}

public void ActivateScene() {
	async.allowSceneActivation = true;
}
*/