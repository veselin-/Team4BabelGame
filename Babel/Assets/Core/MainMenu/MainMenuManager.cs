using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject SettingsPanel, CreditsPanel, AchievementsPanel;
	public Text SoundText, MusicText, VoicesText, SoundFXText;

	private Animator menuAnim;

	AudioManager _audioManager;

	// Use this for initialization
	void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
		SettingsPanel.SetActive (false);
		menuAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartBtnPress()
	{
		Application.LoadLevel ("LevelSelect");
	}

	public void SettingsBtnPress()
	{
		InitTextFields ();
		SettingsPanel.SetActive (true);
	}

	public void SettingsBackBtnPress()
	{
		SettingsPanel.SetActive (false);
	}

	public void CreditsBtnPress()
	{
		menuAnim.SetBool ("Credits", true);
		CreditsPanel.SetActive (true);
	}

	public void CreditsBackBtnPress()
	{
		menuAnim.SetBool ("Credits", false);
		CreditsPanel.SetActive (false);
	}

	public void AchievementsBtnPress()
	{
		AchievementsPanel.SetActive (true);
	}

	public void AchievementsBackBtnPress()
	{
		AchievementsPanel.SetActive (false);
	}
	
	private void InitTextFields()
	{
		GetSoundText ();
		GetMusicText ();
		GetVoicesText ();
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

}
