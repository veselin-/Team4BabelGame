using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Core.Configuration;

public class MainMenuManager : MonoBehaviour {

	public GameObject SettingsPanel, CreditsPanel, AchievementsPanel;
	public Text SoundTextOn, SoundTextOff, MusicTextOn, MusicTextOff, VoicesTextOn, VoicesTextOff, SoundFxTextOn, SoundFxTextOff;

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
	    SetupButtons();
	   
	}

    private void SetupButtons()
    {
        SetupLanguage();
        GetSoundText();
        GetMusicText();
        GetVoicesText();
        GetSoundFXText();
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
	    SoundTextOn.enabled = PlayerPrefs.GetString("Sound").Equals(Constants.PlayerPrefs.On);
	    SoundTextOff.enabled = !PlayerPrefs.GetString("Sound").Equals(Constants.PlayerPrefs.On);
	}
	private void GetMusicText()
	{
        MusicTextOn.enabled = PlayerPrefs.GetString("Music").Equals(Constants.PlayerPrefs.On);
        MusicTextOff.enabled = !PlayerPrefs.GetString("Music").Equals(Constants.PlayerPrefs.On);
    }
	private void GetVoicesText()
	{
        VoicesTextOn.enabled = PlayerPrefs.GetString("Voices").Equals(Constants.PlayerPrefs.On);
        VoicesTextOff.enabled = !PlayerPrefs.GetString("Voices").Equals(Constants.PlayerPrefs.On);
    }
	private void GetSoundFXText()
	{
        SoundFxTextOn.enabled = PlayerPrefs.GetString("SoundFX").Equals(Constants.PlayerPrefs.On);
        SoundFxTextOff.enabled = !PlayerPrefs.GetString("SoundFX").Equals(Constants.PlayerPrefs.On);
    }

    public void SetupLanguage()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs.SetString("Language", Constants.Languages.Danish);
            SettingsLanguage.sprite = DanishFlag;
            LanguageManager.Instance.LoadLanguage(PlayerPrefs.GetString("Language"));
        }
        else
        {
            SettingsLanguage.sprite = PlayerPrefs.GetString("Language").Equals(Constants.Languages.Danish) ? DanishFlag : EnglishFlag;
            LanguageManager.Instance.LoadLanguage(PlayerPrefs.GetString("Language"));
        }
    }

    public void LanguageChange(string language)
    {
        LanguageManager.Instance.LoadLanguage(language);
        PlayerPrefs.SetString("Language", language);
        LocalizedText[] texts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText text in texts)
        {
            text.LocalizeText();
        }
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

    public void SoundSlider(float i)
    {
        
    }
    public void MusicSlider(float i)
    {

    }
    public void VoiceSlider(float i)
    {

    }
    public void SfxSlider(float i)
    {

    }

    public void LanguageBtnPress()
    {
		_audioManager.ClickBtnPlay ();
         if (PlayerPrefs.GetString("Language").Equals(Constants.Languages.Danish))
        {
            LanguageChange(Constants.Languages.English);
            SettingsLanguage.sprite = EnglishFlag;
        }
        else
        {
            LanguageChange(Constants.Languages.Danish);
            SettingsLanguage.sprite = DanishFlag;
        }
    }


    

}
