using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject SettingsPanel;
	public Text SoundText, MusicText, VoicesText, SoundFXText;


	AudioManager _audioManager;

	// Use this for initialization
	void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
		SettingsPanel.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SettingsBtnPress()
	{
		InitTextFields ();
		SettingsPanel.SetActive (true);
	}
	
	public void BackBtnPress()
	{
		SettingsPanel.SetActive (false);
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
