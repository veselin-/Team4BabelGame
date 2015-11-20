using UnityEngine;
using System.Collections;
//using UnityEditor.AnimatedValues;
using UnityEngine.UI;
using Assets.Characters.Player.Scripts;

public class UiController : MonoBehaviour
{
    private Animator anim;
    public ScrollRect scrollRect;
    public Animator bookAnim;
    public GameObject slidePanel;
    public GameObject _pokedexButton;
    public GameObject closeUiBut;
    public GameObject menuMask;
    NavMeshAgent navMeshP, navMeshS;
	private AudioManager _audioManager;
	private CameraManager _cameraManager;
	private PlayerMovement _playerMovement;

    private float playerSpeed;
    private float sideKickSpeed;
	//private GameObject _pokedexButton;
	private GameObject _pauseCanvas;
    int hotbarOpen = 0;

    // Use this for initialization
    void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager> ().GetComponent<AudioManager> ();
		_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
		_playerMovement = GameObject.FindObjectOfType<PlayerMovement> ().GetComponent<PlayerMovement> ();
        navMeshP = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        navMeshS = GameObject.FindGameObjectWithTag("SideKick").GetComponent<NavMeshAgent>();
		//_pokedexButton = transform.FindChild ("Button").gameObject;
		_pauseCanvas = GameObject.FindObjectOfType<PauseScreen> ().gameObject;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewSignCreation(int id)
    {
		//Debug.Log ("NewSignCreation");
		_playerMovement.enabled = false;
		_cameraManager.enabled = false;
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        anim.SetTrigger("FullyEnter");
        anim.SetBool("CreatingSign", true);
        bookAnim.SetBool("CreatingSign", true);
        bookAnim.SetTrigger("CreationToggle");
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
        bookAnim.SetBool("CreatingSign", false);
        bookAnim.SetTrigger("CreationToggle");
        anim.SetTrigger("MenuExit");
        slidePanel.SetActive(true);
        Time.timeScale = 1;
		_pokedexButton.SetActive (true);
		_pauseCanvas.SetActive (true);
    }

    public void PokedexOpen()
    {
        if (hotbarOpen == 2)
        {
            anim.SetTrigger("MenuToggle");
            _cameraManager.enabled = false;
            _playerMovement.enabled = false;
            _audioManager.PokedexBtnOpenPlay();
            Time.timeScale = 0;
            _pauseCanvas.SetActive(false);
            closeUiBut.SetActive(true);
            menuMask.GetComponent<ScrollRect>().enabled = true;
        }
    }

    public void HotbarPokedexOpen()
    {
        if(hotbarOpen == 1)
        {
            anim.SetTrigger("MenuToggle");
            //_cameraManager.enabled = false;
            //_playerMovement.enabled = false;
            _audioManager.PokedexBtnOpenPlay();
            //_pauseCanvas.SetActive(false);
            menuMask.GetComponent<ScrollRect>().enabled = false;
            menuMask.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
        }
    }

    public void hotbarOpenPlusOne()
    {
        hotbarOpen += 1;
    }

    public void PokedexClose()
    {
        anim.SetTrigger("MenuExit");
        _cameraManager.enabled = true;
		_playerMovement.enabled = true;
		_audioManager.PokedexBtnClosePlay ();
         Time.timeScale = 1;
        hotbarOpen = 0;
		_pauseCanvas.SetActive (true);
    }
}
