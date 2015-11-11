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

    //NavMeshAgent navMeshP, navMeshS;
    //float speed1, speed2;

    // Use this for initialization
    void Start () {
        //navMeshP = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        //navMeshS = GameObject.FindGameObjectWithTag("SideKick").GetComponent<NavMeshAgent>();
        //speed1 = navMeshP.gameObject.GetComponent<AiMovement>().MovementSpeed;
        //speed2 = navMeshS.gameObject.GetComponent<AiMovement>().MovementSpeed; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void SignBookEnter()
    {
        SignBook.SetTrigger("SignBookEnter");
        //navMeshP.speed = 0;
        //navMeshS.speed = 0;
        Time.timeScale = 0;
    }
   public void SignBookExit()
    {
        SignBook.SetTrigger("SignBookExit");
        //navMeshP.speed = speed1;
        //navMeshS.speed = speed2;
        Time.timeScale = 1;
    }

    public void SignCreationEnter()
    {
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
        Shop.SetTrigger("Enter");
    }
    public void ShopExit()
    {
        Shop.SetTrigger("Exit");
    }

    public void CustomizationEnter()
    {
        Customization.SetTrigger("Enter");
    }
    public void CustomizationExit()
    {
        Customization.SetTrigger("Exit");
    }

}
