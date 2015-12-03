using UnityEngine;
using System.Collections;
//using UnityEditor.AnimatedValues;
using UnityEngine.UI;
using Assets.Characters.Player.Scripts;
using Assets.Core.Configuration;

public class UiController : MonoBehaviour
{
    private Animator anim;
    public ScrollRect scrollRect;
    public GameObject creation;
    public GameObject slidePanel;
    public GameObject _pokedexButton;
    public GameObject closeUiBut;
    public GameObject menuMask;
    public GameObject MinBut;
    public GameObject hintPanel;
    public GameObject menuIndicator;
    public GameObject shopCanvas;
    public Text signText;
    public Image GlowPanel;

	private AudioManager _audioManager;
	private CameraManager _cameraManager;
	private PlayerMovement _playerMovement;

    private float playerSpeed;
    private float sideKickSpeed;
	private GameObject _pauseCanvas;
    private GameObject arrowBut;
    public static int hotbarOpen = 0;
    bool firstTime = true;

    bool isStuffRunning;

    // Use this for initialization
    void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager> ().GetComponent<AudioManager> ();
		_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
		_playerMovement = GameObject.FindObjectOfType<PlayerMovement> ().GetComponent<PlayerMovement> ();
		_pauseCanvas = GameObject.FindObjectOfType<PauseScreen> ().gameObject;
        anim = GetComponent<Animator>();
        arrowBut = GameObject.FindGameObjectWithTag("PokedexButton");
        hotbarOpen = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

    }

    public void NewSignCreation(int id)
    {
        if(hotbarOpen >= 1)
        {
            anim.SetTrigger("MenuToggle");
        }
        else
        {
            anim.SetTrigger("FullyEnter");
        }
        anim.SetBool("CreatingSign", true);
        creation.SetActive(true);
        signText.enabled = true;
        switch (id)
        {
            case 0:
                signText.text = LanguageManager.Instance.Get("Phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("Phrases/CallOver");
                break;
            case 2:
                signText.text = LanguageManager.Instance.Get("Phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("Phrases/Lever");
                break;
            case 3:
                signText.text = LanguageManager.Instance.Get("Phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("Phrases/Stick");
                break;
            case 4:
                signText.text = LanguageManager.Instance.Get("Phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("Phrases/Firepit");
                break;
            case 5:
                signText.text = LanguageManager.Instance.Get("Phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("Phrases/Trade");
                break;
        }
        _playerMovement.enabled = false;
        _cameraManager.enabled = false;
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        menuIndicator.SetActive(false);
        MinBut.SetActive(false);
        slidePanel.SetActive(false);
        Time.timeScale = 0;
		_pokedexButton.SetActive (false);
		_pauseCanvas.SetActive (false);
        if (firstTime && Application.loadedLevelName == "Tutorial2Beta")
        {
            StartCoroutine(FeedbackCoRoutineAlpha());
        }
    }

    IEnumerator FeedbackCoRoutineAlpha()
    {
        while (firstTime)
        {
            GlowPanel.color = Color.Lerp(Color.white - new Color(0, 0, 0, 0.3f), Color.white - new Color(0, 0, 0, 1f), Mathf.PingPong(Time.unscaledTime, 1f));
            yield return new WaitForEndOfFrame();
        }
        GlowPanel.color = Color.clear;
    }

    public void SignCreationDone()
    {
        firstTime = false;
        anim.SetBool("CreatingSign", false);
        creation.SetActive(false);
        anim.SetTrigger("MenuExit");
        slidePanel.SetActive(true);
        Time.timeScale = 1;
		_pokedexButton.SetActive (true);
		_pauseCanvas.SetActive (true);
        signText.enabled = false;
        _playerMovement.enabled = true;
        _cameraManager.enabled = true;
        hotbarOpen = 0;
    }

    public void OpenShop()
    {
        anim.SetTrigger("FullyEnter");
        StartCoroutine(PokedexFullyEnterShop());
    }

    public void OpenShop2()
    {
        _cameraManager.enabled = false;
        _playerMovement.enabled = false;
        Time.timeScale = 0;
        MinBut.SetActive(false);
        arrowBut.SetActive(false);
        _pauseCanvas.SetActive(false);
        scrollRect.horizontalNormalizedPosition = 1f;
        menuIndicator.SetActive(false);
    }

    public void CloseShop()
    {
        anim.SetTrigger("MenuExit");
        StartCoroutine(FullyExitShop());
    }

    public void PokedexOpen()
    {
        if (hotbarOpen == 2 && isStuffRunning == false)
        {
            //if (arrowBut.transform.rotation.z == 1)
            //{
                anim.SetTrigger("HalfExit");
                StartCoroutine(PokedexHalfExit());
            //}
        }
        else if (hotbarOpen == 1 && isStuffRunning == false)
        {
            anim.SetTrigger("MenuToggle");
            StartCoroutine(PokedexFullyEnter());
        }
        else if (hotbarOpen == 0 && isStuffRunning == false)
        {
            anim.SetTrigger("MenuToggle");
            StartCoroutine(PokedexEnter());
        }
    }

    public void PokedexClose()
    {
        if(hotbarOpen == 2 && isStuffRunning == false)
        {
            anim.SetTrigger("MenuExit");
            StartCoroutine(FullyExit());
        }
        if(hotbarOpen == 1 && isStuffRunning == false)
        {
            anim.SetTrigger("MenuExit");
            StartCoroutine(PokedexEnterZero());
        }
    }

    IEnumerator FullyExit()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexFullyExit"))
        {
            yield return new WaitForEndOfFrame();
        }
        hintPanel.SetActive(false);
        _cameraManager.enabled = true;
        _playerMovement.enabled = true;
        _audioManager.PokedexBtnOpenPlay();
        arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
        Time.timeScale = 1;
        hotbarOpen = 0;
        _pauseCanvas.SetActive(true);
        MinBut.SetActive(false);
        menuIndicator.SetActive(true);
        //Debug.Log(hotbarOpen + "CLOSED");
        isStuffRunning = false;
    }

    IEnumerator PokedexEnter()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexEnter"))
        {
            yield return new WaitForEndOfFrame();
        }
        hotbarOpen = 1;
        _cameraManager.enabled = true;
        _playerMovement.enabled = true;
        _audioManager.PokedexBtnMiddlePlay();
        _pauseCanvas.SetActive(true);
        closeUiBut.SetActive(false);
        Time.timeScale = 1;
        menuMask.GetComponent<ScrollRect>().enabled = false;
        menuMask.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
        MinBut.SetActive(true);
        hintPanel.SetActive(false);
        //Debug.Log(hotbarOpen + "HOTBAR OPEN");
        isStuffRunning = false;
    }

    IEnumerator FullyExitShop()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexFullyExit"))
        {
            yield return new WaitForEndOfFrame();
        }
        isStuffRunning = false;
    }

        IEnumerator PokedexFullyEnterShop()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexFullyEnter"))
        {
            yield return new WaitForEndOfFrame();
        }
        _audioManager.PokedexBtnOpenPlay();
        _cameraManager.enabled = false;
        _playerMovement.enabled = false;
        Time.timeScale = 0;
        MinBut.SetActive(false);
        arrowBut.SetActive(false);
        _pauseCanvas.SetActive(false);
        scrollRect.horizontalNormalizedPosition = 1f;
        menuIndicator.SetActive(false);
        //closeUiBut.SetActive(true);
        isStuffRunning = false;
    }

        IEnumerator PokedexFullyEnter()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexFullyEnter"))
        {
            yield return new WaitForEndOfFrame();
        }
        arrowBut.transform.rotation = new Quaternion(0, 0, 180, 0);
        _audioManager.PokedexBtnClosePlay();
        _cameraManager.enabled = false;
        _playerMovement.enabled = false;
        Time.timeScale = 0;
        hotbarOpen = 2;
        _pauseCanvas.SetActive(false);
        closeUiBut.SetActive(true);
        hintPanel.SetActive(true);
        hintPanel.transform.GetChild(0).GetComponent<Text>().text = "";
        menuMask.GetComponent<ScrollRect>().enabled = true;
        menuIndicator.SetActive(true);
        //Debug.Log(hotbarOpen + "FULLY OPEN");
        isStuffRunning = false;
    }

    IEnumerator PokedexHalfExit()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexHalfExit"))
        {
            yield return new WaitForEndOfFrame();
        }
        arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
        hintPanel.SetActive(false);
        _pauseCanvas.SetActive(true);
        closeUiBut.SetActive(false);
        _cameraManager.enabled = true;
        _playerMovement.enabled = true;
        menuMask.GetComponent<ScrollRect>().enabled = false;
        scrollRect.horizontalNormalizedPosition = 0f;
        Time.timeScale = 1;
        hotbarOpen = 1;
        _audioManager.PokedexBtnMiddlePlay();
        //Debug.Log(hotbarOpen + "FULLY TO HOTBAR");
        isStuffRunning = false;
    }

    IEnumerator PokedexEnterZero()
    {
        isStuffRunning = true;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("PokeDexEnter 0"))
        {
            yield return new WaitForEndOfFrame();
        }
        hintPanel.SetActive(false);
        _cameraManager.enabled = true;
        _playerMovement.enabled = true;
        _audioManager.PokedexBtnOpenPlay();
        arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
        Time.timeScale = 1;
        hotbarOpen = 0;
        _pauseCanvas.SetActive(true);
        MinBut.SetActive(false);
        menuIndicator.SetActive(true);
        //Debug.Log(hotbarOpen + "CLOSED");
        isStuffRunning = false;
    }
        //PokeDexEnter -> hotbar
        //PokeDexEnter 0 -> lukker hotbar
        //PokeDexFullyEnter -> hotbar til helt op
        //PokeDexFullyExit -> helt op til helt ned
        //PokeDexHalfExit -> helt op til hotbar
        //Hidden -> helt nede
}
