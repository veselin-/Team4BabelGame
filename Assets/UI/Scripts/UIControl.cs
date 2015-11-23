using UnityEngine;
using System.Collections;
using Assets.Characters.AiScripts;

public class UIControl : MonoBehaviour
{

    public Animator SignBook;
    public Animator SignCreation;
    public Animator Shop;
    public Animator Customization;
    public Animator MainMenu;
	private CameraManager _cameraManager;
    //NavMeshAgent navMeshP, navMeshS;
    //float speed1, speed2;

    // Use this for initialization
    void Start () {
        //navMeshP = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        //navMeshS = GameObject.FindGameObjectWithTag("SideKick").GetComponent<NavMeshAgent>();
        //speed1 = navMeshP.gameObject.GetComponent<AiMovement>().MovementSpeed;
        //speed2 = navMeshS.gameObject.GetComponent<AiMovement>().MovementSpeed; 
		_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void SignBookEnter()
    {
		Debug.Log ("SignBookEnter");
		_cameraManager.enabled = false;
        SignBook.SetTrigger("SignBookEnter");
        //navMeshP.speed = 0;
        //navMeshS.speed = 0;
        Time.timeScale = 0;
    }
   public void SignBookExit()
    {
		_cameraManager.enabled = true;
        SignBook.SetTrigger("SignBookExit");
        //navMeshP.speed = speed1;
        //navMeshS.speed = speed2;
        Time.timeScale = 1;
    }

    public void SignCreationEnter()
    {
		Debug.Log ("SignCreationEnter");
		_cameraManager.enabled = false;
        SignCreation.SetTrigger("SignCreationEnter");
        //StartCoroutine(StopTimeScale());
        //if (wtfIsThis)
        //{
        //navMeshP.speed = 0;
        //navMeshS.speed = 0;
        Time.timeScale = 0;
        //}

        //SignCreation.gameObject.GetComponentInChildren<CombineSymbols>().ClearCurrentSign();
    }
    public void SignCreationExit()
    {
		Debug.Log ("SignCreationExit");
		_cameraManager.enabled = true;
        SignCreation.SetTrigger("SignCreationExit");
        //StartCoroutine(StartTimeScale());
        //if (!wtfIsThis)
        //{
        //navMeshP.speed = speed1;
        //navMeshS.speed = speed2;
        Time.timeScale = 1;
        //}
    }

    //IEnumerator StopTimeScale()
    //{
    //    new WaitForSeconds(2);
    //    Debug.Log("I RUN THIS");
    //    wtfIsThis = true;
    //    yield return wtfIsThis;
    //}

    //IEnumerator StartTimeScale()
    //{
    //    new WaitForSeconds(2);
    //    wtfIsThis = false;
    //    yield return wtfIsThis;
    //}

    public void ShopEnter()
    {
		Debug.Log ("ShopEnter");
		_cameraManager.enabled = false;
        Shop.SetTrigger("Enter");
    }
    public void ShopExit()
    {
		Debug.Log ("ShopExit");
		_cameraManager.enabled = true;
        Shop.SetTrigger("Exit");
    }

    public void CustomizationEnter()
    {
		Debug.Log ("CustomizationEnter");
		_cameraManager.enabled = false;
        Customization.SetTrigger("Enter");
    }
    public void CustomizationExit()
    {
		Debug.Log ("CustomizationExit");
		_cameraManager.enabled = true;
        Customization.SetTrigger("Exit");
    }

}
