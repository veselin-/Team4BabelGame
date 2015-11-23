using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelScreen : MonoBehaviour {


	public Text orbsTxt; 
	public string NextLevel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowEndLevelScreen()
	{
		gameObject.SetActive (true);
	}

	public void MainMenuBtn()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void NextLevelBtn()
	{
		Application.LoadLevel (NextLevel);
	}

	public void UpdateOrbsUI()
	{
		orbsTxt.text = "" + PlayerPrefs.GetInt ("CurrencyAmount").ToString();
	}

    void OnEnable()
    {
        UpdateOrbsUI();
    }
}
