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
    NavMeshAgent navMeshP, navMeshS;
	private AudioManager _audioManager;
	private CameraManager _cameraManager;
	private PlayerMovement _playerMovement;

    private float playerSpeed;
    private float sideKickSpeed;

    // Use this for initialization
    void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager> ().GetComponent<AudioManager> ();
		_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
		_playerMovement = GameObject.FindObjectOfType<PlayerMovement> ().GetComponent<PlayerMovement> ();
        navMeshP = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        navMeshS = GameObject.FindGameObjectWithTag("SideKick").GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewSignCreation(int id)
    {
		Debug.Log ("NewSignCreation");
		_playerMovement.enabled = false;
		_cameraManager.enabled = false;
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        anim.SetTrigger("MenuToggle");
        anim.SetBool("CreatingSign", true);
        bookAnim.SetBool("CreatingSign", true);
        bookAnim.SetTrigger("CreationToggle");
        slidePanel.SetActive(false);
       playerSpeed = navMeshP.speed;
       sideKickSpeed = navMeshS.speed;

        navMeshP.speed = 0f;
        navMeshS.speed = 0f;

         Time.timeScale = 0;
    }

    public void SignCreationDone()
    {
		//Debug.Log ("SignCreationDone");
		//_cameraManager.enabled = true;
        anim.SetBool("CreatingSign", false);
        bookAnim.SetBool("CreatingSign", false);
        bookAnim.SetTrigger("CreationToggle");
        anim.SetTrigger("MenuToggle");
        slidePanel.SetActive(true);
        //  navMeshP.ResetPath();
        //  navMeshS.ResetPath();
        navMeshP.speed = playerSpeed;
        navMeshS.speed = sideKickSpeed;
        Time.timeScale = 1;
    }

    public void PokedexOpen()
    {
		Debug.Log ("PokedexOpen");
		_cameraManager.enabled = false;
		_playerMovement.enabled = false;
		_audioManager.PokedexBtnOpenPlay ();
        //   navMeshP.Stop();
        //   navMeshS.Stop();
        playerSpeed = navMeshP.speed;
        sideKickSpeed = navMeshS.speed;

        navMeshP.speed = 0f;
        navMeshS.speed = 0f;
        Time.timeScale = 0;
    }

    public void PokedexClose()
    {
		Debug.Log ("PokedexClose");
		_cameraManager.enabled = true;
		_playerMovement.enabled = true;
		_audioManager.PokedexBtnClosePlay ();
        //   navMeshP.ResetPath();
        //   navMeshS.ResetPath();
        navMeshP.speed = playerSpeed;
        navMeshS.speed = sideKickSpeed;
         Time.timeScale = 1;
    }
}
