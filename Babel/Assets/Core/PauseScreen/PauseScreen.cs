using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreen : MonoBehaviour {

	public GameObject PausePanel;
	public GameObject SettingsPanel;
	public GameObject SettingsButton;
	public GameObject GamePausedTxt;

	public Text SoundText, MusicText, VoicesText, SoundFXText;
	private Animator pauseAnim;
	
	AudioManager _audioManager;
	
	// Use this for initialization
	void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
		SettingsPanel.SetActive (false);
		pauseAnim = GetComponent<Animator> ();
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PausePanelBtn()
	{
		GamePausedTxt.SetActive (true);
		SettingsButton.SetActive (false);
		pauseAnim.SetBool ("ShowPause", true);

		Time.timeScale = 0f;
	}

	public void PausePanelBackBtn()
	{
		GamePausedTxt.SetActive (false);
		SettingsButton.SetActive (true);
		pauseAnim.SetBool ("ShowPause", false);

		Time.timeScale = 1f;
	}

	public void LevelSelectBtn()
	{
		Application.LoadLevel ("LevelSelect");
	}

	public void SettingsPanelBtn()
	{
		pauseAnim.SetBool ("ShowSettings", true);
		pauseAnim.SetBool ("ShowPause", false);
		InitTextFields ();
	}

	public void SettingsPanelBackBtn()
	{
		pauseAnim.SetBool ("ShowSettings", false);
		pauseAnim.SetBool ("ShowPause", true);
	}

	public void MainMenuScene()
	{
		Application.LoadLevel ("MainMenu");
	}

	private void InitTextFields()
	{
		GetSoundText ();
		GetMusicText ();
		GetVoicesText ();
		GetSoundFXText ();
	}

	public void SoundBtnPress()
	{
		_audioManager.SetSoundOnOff ();
		GetSoundText ();
	}
	
	public void MusicBtnPress()
	{
		_audioManager.SetMusicOnOff ();
		GetMusicText ();
	}
	
	public void VoicesBtnPress()
	{
		_audioManager.SetVoicesOnOff ();
		GetVoicesText ();
	}
	
	public void SoundFXBtnPress()
	{
		_audioManager.SetSoundFXOnOff ();
		GetSoundFXText ();
	}

	private void GetSoundText()
	{
		SoundText.text = "Sound " + PlayerPrefs.GetString ("Sound").ToString();
	}
	private void GetMusicText()
	{
		MusicText.text = "Music " + PlayerPrefs.GetString ("Music").ToString();
	}
	private void GetVoicesText()
	{
		VoicesText.text = "Voices " + PlayerPrefs.GetString ("Voices").ToString();
	}
	private void GetSoundFXText()
	{
		SoundFXText.text = "SoundFX " + PlayerPrefs.GetString ("SoundFX").ToString();
	}

}
