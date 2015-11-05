using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelScreen : MonoBehaviour {

	public GameObject EndLevelCanvas;
	public Text orbsTxt; 
	public string NextLevel;

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

	public void NextLevelBtn()
	{
		Application.LoadLevel (NextLevel);
	}

	public void UpdateOrbsUI()
	{
		orbsTxt.text = "Orbs " + PlayerPrefs.GetInt ("CurrencyAmount").ToString();
	}

    void OnEnable()
    {
        UpdateOrbsUI();
    }
}
