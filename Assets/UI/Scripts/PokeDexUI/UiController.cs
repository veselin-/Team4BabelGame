using UnityEngine;
using System.Collections;
//using UnityEditor.AnimatedValues;
using UnityEngine.UI;
using Assets.Characters.Player.Scripts;

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
    }
	
	// Update is called once per frame
	void Update () {
        //if (menuMask.GetComponent<UiSnapScroll>().Pips[0].GetComponent<Image>().color == Color.black)
        //{
        //    arrowBut.SetActive(false);
        //}
        //else
        //{
        //    arrowBut.SetActive(true);
        //}
    }

    public void NewSignCreation(int id)
    {
		//Debug.Log ("NewSignCreation");
		_playerMovement.enabled = false;
		_cameraManager.enabled = false;
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        if(hotbarOpen == 1)
        {
            anim.SetTrigger("MenuToggle");
        }
        else
        {
            anim.SetTrigger("FullyEnter");
        }
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
    }

    public void OpenShop()
    {
        arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
        anim.SetTrigger("MenuToggle");
        _cameraManager.enabled = false;
        _playerMovement.enabled = false;
        _audioManager.PokedexBtnOpenPlay();
        Time.timeScale = 0;
        _pauseCanvas.SetActive(false);
        closeUiBut.SetActive(true);
        menuMask.GetComponent<ScrollRect>().enabled = true;
        scrollRect.horizontalNormalizedPosition = 1f;
        menuIndicator.SetActive(true);
    }

    public void PokedexOpen()
    {
        if (hotbarOpen == 2)
        {
            if (arrowBut.transform.rotation.z == 0)
            {
                anim.SetTrigger("HalfExit");
                hotbarOpen = 1;
                //HotbarPokedexOpen();
                arrowBut.transform.rotation = new Quaternion(0, 0, 180, 0);
                hintPanel.SetActive(false);
                _pauseCanvas.SetActive(true);
                closeUiBut.SetActive(false);
                _cameraManager.enabled = true;
                _playerMovement.enabled = true;
                scrollRect.horizontalNormalizedPosition = 0f;
                Time.timeScale = 1;
                return;
            }
            arrowBut.transform.rotation = new Quaternion(0, 0, 0, 0);
            anim.SetTrigger("MenuToggle");
            _cameraManager.enabled = false;
            _playerMovement.enabled = false;
            _audioManager.PokedexBtnOpenPlay();
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
        arrowBut.transform.rotation = new Quaternion(0, 0, 180, 0);
        Time.timeScale = 1;
        hotbarOpen = 0;
		_pauseCanvas.SetActive (true);
        MinBut.SetActive(false);
        menuIndicator.SetActive(true);
    }
}
