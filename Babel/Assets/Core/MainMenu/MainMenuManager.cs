using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject SettingsPanel, CreditsPanel, AchievementsPanel;
	public Text SoundText, MusicText, VoicesText, SoundFXText;

    public Image SettingsLanguage;
    public Sprite DanishFlag;
    public Sprite EnglishFlag;


    private Animator menuAnim;

	AudioManager _audioManager;

	// Use this for initialization
	void Awake () {
		Time.timeScale = 1f;
		_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
		SettingsPanel.SetActive (false);
		menuAnim = GetComponent<Animator> ();
        LanguageManager.Instance.LoadLanguage("Danish");
        PlayerPrefs.SetString("Language", "Danish");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartBtnPress()
	{
		_audioManager.ClickBtnPlay ();
		Application.LoadLevel ("LevelSelect");
	}

	public void SettingsBtnPress()
	{
		_audioManager.ClickBtnPlay ();
		InitTextFields ();
		SettingsPanel.SetActive (true);
	}

	public void SettingsBackBtnPress()
	{
		_audioManager.ClickBtnPlay ();
		SettingsPanel.SetActive (false);
	}

	public void CreditsBtnPress()
	{
		_audioManager.ClickBtnPlay ();
		menuAnim.SetBool ("Credits", true);
		CreditsPanel.SetActive (true);
	}

	public void CreditsBackBtnPress()
	{
		_audioManager.ClickBtnPlay ();
		menuAnim.SetBool ("Credits", false);
		CreditsPanel.SetActive (false);
	}

	public void AchievementsBtnPress()
	{
		_audioManager.ClickBtnPlay ();
		AchievementsPanel.SetActive (true);
	}

	public void AchievementsBackBtnPress()
	{
		_audioManager.ClickBtnPlay ();
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
		_audioManager.ClickBtnPlay ();
		GetSoundText ();
	}
	
	public void MusicBtnPress()
	{
		_audioManager.SetMusicOnOff ();
		_audioManager.ClickBtnPlay ();
		GetMusicText ();
	}

	public void VoicesBtnPress()
	{
		_audioManager.SetVoicesOnOff ();
		_audioManager.ClickBtnPlay ();
		GetVoicesText ();
	}

	public void SoundFXBtnPress()
	{
		_audioManager.SetSoundFXOnOff ();
		_audioManager.ClickBtnPlay ();
		GetSoundFXText ();
	}

    public void LanguageBtnPress()
    {
		_audioManager.ClickBtnPlay ();
        if (PlayerPrefs.GetString("Language").Equals("Danish"))
        {
            LanguageManager.Instance.LoadLanguage("English");
            PlayerPrefs.SetString("Language", "English");
            SettingsLanguage.sprite = EnglishFlag;
        }
        else
        {
            LanguageManager.Instance.LoadLanguage("Danish");
            PlayerPrefs.SetString("Language", "Danish");
            SettingsLanguage.sprite = DanishFlag;
        }
    }


    

}
