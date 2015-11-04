using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelScreen : MonoBehaviour {

	public GameObject EndLevelCanvas;
	public Text orbsTxt; 

	// Use this for initialization
	void Start () {
		EndLevelCanvas = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowEndLevelScreen()
	{
		EndLevelCanvas.SetActive (true);
	}

	public void MainMenuBtn()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void NextLevelBtn(string NextLevel)
	{
		Application.LoadLevel (NextLevel);
	}

	public void UpdateOrbsUI()
	{
		orbsTxt.text = "Orbs " + PlayerPrefs.GetInt ("CurrencyAmount").ToString();
	}
}
