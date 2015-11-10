using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour
{

    public Animator SignBook;
    public Animator SignCreation;
    public Animator Shop;
    public Animator Customization;
    public Animator MainMenu;
    bool wtfIsThis = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void SignBookEnter()
    {
        SignBook.SetTrigger("SignBookEnter");
        Time.timeScale = 0;
    }
   public void SignBookExit()
    {
        SignBook.SetTrigger("SignBookExit");
        Time.timeScale = 1;
    }

    public void SignCreationEnter()
    {
        SignCreation.SetTrigger("SignCreationEnter");
        //StartCoroutine(StopTimeScale());
        //if (wtfIsThis)
        //{
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
