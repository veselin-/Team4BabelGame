using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreen : MonoBehaviour {

	public GameObject PausePanel;
	public GameObject SettingsPanel;
	public GameObject SettingsButton;

	public Text SoundText, MusicText, VoicesText, SoundFXText;
	private Animator pauseAnim;
	
	AudioManager _audioManager;
	
	// Use this for initialization
	void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
		SettingsPanel.SetActive (false);
		pauseAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PausePanelBtn()
	{
		SettingsButton.SetActive (false);
		//PausePanel.SetActive (true);
		pauseAnim.SetBool ("ShowPause", true);
	}

	public void PausePanelBackBtn()
	{
		//PausePanel.SetActive (false);
		SettingsButton.SetActive (true);
		pauseAnim.SetBool ("ShowPause", false);
	}

	public void LevelSelectBtn()
	{
		Application.LoadLevel ("LevelSelect");
	}

	public void SettingsPanelBtn()
	{
		//SettingsPanel.SetActive (true);
		pauseAnim.SetBool ("ShowSettings", true);
		pauseAnim.SetBool ("ShowPause", false);
	}

	public void SettingsPanelBackBtn()
	{
		//SettingsPanel.SetActive (false);
		pauseAnim.SetBool ("ShowSettings", false);
		pauseAnim.SetBool ("ShowPause", true);
	}



}
