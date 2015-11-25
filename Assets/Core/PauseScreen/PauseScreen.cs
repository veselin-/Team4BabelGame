using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Core.Configuration;
using Assets.Characters.Player.Scripts;

public class PauseScreen : MonoBehaviour {

	public GameObject PausePanel;
	public GameObject SettingsPanel;
	public GameObject SettingsButton;
	public GameObject GamePausedTxt;

    public Text SoundTextOn, SoundTextOff, MusicTextOn, MusicTextOff, VoicesTextOn, VoicesTextOff, SoundFxTextOn, SoundFxTextOff;
	private Animator pauseAnim;

	private AudioManager _audioManager;
	private CameraManager _cameraManager;
	private GameObject _pokedexButton;
	private PlayerMovement _player;

    public Slider MusicSlider, VoicesSlider, SoundFxSlider;
    // Use this for initialization
    void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
		_pokedexButton = GameObject.FindGameObjectWithTag("PokedexButton");
		_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
		_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
		SettingsPanel.SetActive (false);
		pauseAnim = GetComponent<Animator> ();
		Time.timeScale = 1f;
        SetupButtons();
    }

    private void SetupButtons()
    {
        GetSoundText();
        GetMusicText();
        GetVoicesText();
        GetSoundFXText();
    }

    // Update is called once per frame
    void Update () {
	
	}

	public void PausePanelBtn()
	{
		_player.enabled = false;
		_pokedexButton.SetActive (false);
		_cameraManager.enabled = false;
		_audioManager.ClickBtnPlay ();
		GamePausedTxt.SetActive (true);
		SettingsButton.SetActive (false);
		pauseAnim.SetBool ("ShowPause", true);

		Time.timeScale = 0f;
	}

	public void PausePanelBackBtn()
	{
		_player.enabled = true;
		_pokedexButton.SetActive (true);
		_cameraManager.enabled = true;
		_audioManager.ClickBtnPlay ();
		GamePausedTxt.SetActive (false);
		SettingsButton.SetActive (true);
		pauseAnim.SetBool ("ShowPause", false);

		Time.timeScale = 1f;
	}

	public void LevelSelectBtn()
	{
		_audioManager.ClickBtnPlay ();
		Application.LoadLevel ("LevelSelect");
	}

    public void LevelRestart()
    {
        _audioManager.ClickBtnPlay();
        Application.LoadLevel(Application.loadedLevel);
    }

	public void SettingsPanelBtn()
	{
		_audioManager.ClickBtnPlay ();
		pauseAnim.SetBool ("ShowSettings", true);
		pauseAnim.SetBool ("ShowPause", false);
		InitTextFields ();
	}

	public void SettingsPanelBackBtn()
	{
		_audioManager.ClickBtnPlay ();
		pauseAnim.SetBool ("ShowSettings", false);
		pauseAnim.SetBool ("ShowPause", true);
	}

	public void MainMenuScene()
	{
		_audioManager.ClickBtnPlay ();
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

    private void GetSoundText()
    {
        SoundTextOn.enabled = PlayerPrefs.GetString("Sound").Equals(Constants.PlayerPrefs.On);
        SoundTextOff.enabled = !PlayerPrefs.GetString("Sound").Equals(Constants.PlayerPrefs.On);
        //SoundSlider.normalizedValue = PlayerPrefs.GetString("Sound"
    }
    private void GetMusicText()
    {
        MusicTextOn.enabled = PlayerPrefs.GetString("Music").Equals(Constants.PlayerPrefs.On);
        MusicTextOff.enabled = !PlayerPrefs.GetString("Music").Equals(Constants.PlayerPrefs.On);
        MusicSlider.normalizedValue = NormalizeMixerValue(PlayerPrefs.GetFloat("AmbienceV"));
        //Debug.Log(NormalizeMixerValue( PlayerPrefs.GetFloat ("AmbienceV")));
    }
    private void GetVoicesText()
    {
        VoicesTextOn.enabled = PlayerPrefs.GetString("Voices").Equals(Constants.PlayerPrefs.On);
        VoicesTextOff.enabled = !PlayerPrefs.GetString("Voices").Equals(Constants.PlayerPrefs.On);
        VoicesSlider.normalizedValue = NormalizeMixerValue(PlayerPrefs.GetFloat("VoicesV"));
    }
    private void GetSoundFXText()
    {
        SoundFxTextOn.enabled = PlayerPrefs.GetString("SoundFX").Equals(Constants.PlayerPrefs.On);
        SoundFxTextOff.enabled = !PlayerPrefs.GetString("SoundFX").Equals(Constants.PlayerPrefs.On);
        SoundFxSlider.normalizedValue = NormalizeMixerValue(PlayerPrefs.GetFloat("SFXV"));
    }

    float NormalizeMixerValue(float value)
    {
        float scaleVal = (-40 - value) / (-40 - 0);
        return scaleVal;
    }

    float DenormalizeSliderValue(float value)
    {
        float scaleVal = -40 - (value * -40f);
        return scaleVal;
    }
    public void MusicSlide()
    {
        float denormalizedValue = DenormalizeSliderValue(MusicSlider.normalizedValue);
        PlayerPrefs.SetFloat("AmbienceV", denormalizedValue);
        _audioManager.MasterMixer.SetFloat("AmbienceV", denormalizedValue);
    }
    public void VoiceSlide()
    {
        float denormalizedValue = DenormalizeSliderValue(VoicesSlider.normalizedValue);
        PlayerPrefs.SetFloat("VoicesV", denormalizedValue);
        _audioManager.MasterMixer.SetFloat("VoicesV", denormalizedValue);
    }
    public void SfxSlide()
    {
        float denormalizedValue = DenormalizeSliderValue(SoundFxSlider.normalizedValue);
        PlayerPrefs.SetFloat("SFXV", denormalizedValue);
        _audioManager.MasterMixer.SetFloat("SFXV", denormalizedValue);
    }

}
