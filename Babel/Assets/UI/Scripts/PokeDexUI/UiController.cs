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
    NavMeshAgent navMeshP, navMeshS;
	private AudioManager _audioManager;
	private CameraManager _cameraManager;
	private PlayerMovement _playerMovement;

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
        navMeshP.Stop();
        navMeshS.Stop();
    }

    public void SignCreationDone()
    {
		//Debug.Log ("SignCreationDone");
		//_cameraManager.enabled = true;
        anim.SetBool("CreatingSign", false);
        bookAnim.SetBool("CreatingSign", false);
        bookAnim.SetTrigger("CreationToggle");
        navMeshP.ResetPath();
        navMeshS.ResetPath();
    }

    public void PokedexOpen()
    {
		Debug.Log ("PokedexOpen");
		_cameraManager.enabled = false;
		_playerMovement.enabled = false;
		_audioManager.PokedexBtnPlay ();
        navMeshP.Stop();
        navMeshS.Stop();
    }

    public void PokedexClose()
    {
		Debug.Log ("PokedexClose");
		_cameraManager.enabled = true;
		_playerMovement.enabled = true;
		_audioManager.PokedexBtnPlay ();
        navMeshP.ResetPath();
        navMeshS.ResetPath();
    }
}
