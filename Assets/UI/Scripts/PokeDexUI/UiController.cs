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

    //NavMeshAgent navMeshP, navMeshS;
	private AudioManager _audioManager;
	private CameraManager _cameraManager;
	private PlayerMovement _playerMovement;

    private float playerSpeed;
    private float sideKickSpeed;
	//private GameObject _pokedexButton;
	private GameObject _pauseCanvas;
    private GameObject arrowBut;
    public static int hotbarOpen = 0;

    // Use this for initialization
    void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager> ().GetComponent<AudioManager> ();
		_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
		_playerMovement = GameObject.FindObjectOfType<PlayerMovement> ().GetComponent<PlayerMovement> ();
        //navMeshP = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        //navMeshS = GameObject.FindGameObjectWithTag("SideKick").GetComponent<NavMeshAgent>();
		//_pokedexButton = transform.FindChild ("Button").gameObject;
		_pauseCanvas = GameObject.FindObjectOfType<PauseScreen> ().gameObject;
        anim = GetComponent<Animator>();
        arrowBut = GameObject.FindGameObjectWithTag("PokedexButton");
        hotbarOpen = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (menuMask.GetComponent<UiSnapScroll>().Pips[0].GetComponent<Image>().color == Color.black)
        //{
        //    arrowBut.SetActive(false);
        //}
        //else
        //{
        //    arrowBut.SetActive(true);
        //}
        //if (scrollRect.horizontalNormalizedPosition == 1f)
        //{
        //    GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().CreateInfoDialog("Phrases/MockUpShop", "Phrases/ShopText", "Phrases/OKText", AccesShop);

        //}
        //Debug.Log("WTF");
    }

    public void NewSignCreation(int id)
    {
		//Debug.Log ("NewSignCreation");

        if(hotbarOpen == 1)
        {
            anim.SetTrigger("MenuToggle");
        }
        else
        {
            anim.SetTrigger("FullyEnter");
        }
        signText.enabled = true;
        switch (id)
        {
            case 0:
                signText.text = LanguageManager.Instance.Get("phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("phrases/CallOver");
                break;
            case 1:
                signText.text = LanguageManager.Instance.Get("phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("phrases/Lever");
                break;
            case 2:
                signText.text = LanguageManager.Instance.Get("phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("phrases/Stick");
                break;
            case 3:
                signText.text = LanguageManager.Instance.Get("phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("phrases/Firepit");
                break;
            case 5:
                signText.text = LanguageManager.Instance.Get("phrases/CreateASignFor") +
                                LanguageManager.Instance.Get("phrases/Trade");
                break;
        }
        _playerMovement.enabled = false;
        _cameraManager.enabled = false;
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        menuIndicator.SetActive(false);
        MinBut.SetActive(false);
        anim.SetBool("CreatingSign", true);
        creation.SetActive(true);
        slidePanel.SetActive(false);
        Time.timeScale = 0;
		_pokedexButton.SetActive (false);
		_pauseCanvas.SetActive (false);
    }

    public void SignCreationDone()
    {
		//Debug.Log ("SignCreationDone");
		//_cameraManager.enabled = true;
        anim.SetBool("CreatingSign", false);
        creation.SetActive(false);
        anim.SetTrigger("MenuExit");
        slidePanel.SetActive(true);
        Time.timeScale = 1;
		_pokedexButton.SetActive (true);
		_pauseCanvas.SetActive (true);
        signText.enabled = false;
    }

    //public void OpenShop()
    //{
    //    arrowBut.transform.rotation = new Quaternion(0, 0, 180, 0);
    //    anim.SetTrigger("MenuToggle");
    //    _cameraManager.enabled = false;
    //    _playerMovement.enabled = false;
    //    _audioManager.PokedexBtnOpenPlay();
    //    Time.timeScale = 0;
    //    _pauseCanvas.SetActive(false);
    //    closeUiBut.SetActive(true);
    //    menuMask.GetComponent<ScrollRect>().enabled = true;
    //    scrollRect.horizontalNormalizedPosition = 1f;
    //    menuIndicator.SetActive(true);
    //}

    //void AccesShop()
    //{
    //    shopCanvas.SetActive(true);
    //}

    public void PokedexOpen()
    {
        Debug.Log(hotbarOpen);
      
        if (hotbarOpen == 2)
        {
            if (arrowBut.transform.rotation.z == 1)
            {
                anim.SetTrigger("HalfExit");
                hotbarOpen = 1;
                //HotbarPokedexOpen();
                arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
                hintPanel.SetActive(false);
                _pauseCanvas.SetActive(true);
                closeUiBut.SetActive(false);
                _cameraManager.enabled = true;
                _playerMovement.enabled = true;
                scrollRect.horizontalNormalizedPosition = 0f;
                Time.timeScale = 1;
                return;
            }
            arrowBut.transform.rotation = new Quaternion(0, 0, 180, 0);
            anim.SetTrigger("MenuToggle");
            _audioManager.PokedexBtnOpenPlay();
            _cameraManager.enabled = false;
            _playerMovement.enabled = false;
            Debug.Log("LANG TEXT WTF" + arrowBut.transform.rotation.z);
            Time.timeScale = 0;
            _pauseCanvas.SetActive(false);
            closeUiBut.SetActive(true);
            hintPanel.SetActive(true);
            hintPanel.transform.GetChild(0).GetComponent<Text>().text = "";
            //Debug.Log("RESET TEKSTEN FOR HINTPANEL *************************************************");
            menuMask.GetComponent<ScrollRect>().enabled = true;
            menuIndicator.SetActive(true);
        }
    }

    public void HotbarPokedexOpen()
    {
        if(hotbarOpen == 1)
        {
            anim.SetTrigger("MenuToggle");
            _cameraManager.enabled = true;
            _playerMovement.enabled = true;
            _audioManager.PokedexBtnOpenPlay();
            _pauseCanvas.SetActive(true);
            closeUiBut.SetActive(false);
            Time.timeScale = 1;
            menuMask.GetComponent<ScrollRect>().enabled = false;
            menuMask.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
            MinBut.SetActive(true);
            hintPanel.SetActive(false);
        }
    }

    public void hotbarOpenMinusOne()
    {
        if(hotbarOpen > 0)
        {
            hotbarOpen -= 1;
        }
    }

    public void hotbarOpenPlusOne()
    {
        if (hotbarOpen >= 2)
        {
            hotbarOpen = 2;
        }
        else
        {
            hotbarOpen += 1;
        }
    }

    public void PokedexClose()
    {
        anim.SetTrigger("MenuExit");
        hintPanel.SetActive(false);
        _cameraManager.enabled = true;
		_playerMovement.enabled = true;
		_audioManager.PokedexBtnClosePlay ();
        arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
        Time.timeScale = 1;
        hotbarOpen = 0;
		_pauseCanvas.SetActive (true);
        MinBut.SetActive(false);
        menuIndicator.SetActive(true);
    }
}
