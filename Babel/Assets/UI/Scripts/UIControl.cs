using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour
{

    public Animator SignBook;
    public Animator SignCreation;
    public Animator Shop;
    public Animator Customization;
    public Animator MainMenu;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void SignBookEnter()
    {
        SignBook.SetTrigger("SignBookEnter");
    }
   public void SignBookExit()
    {
        SignBook.SetTrigger("SignBookExit");
    }

    public void SignCreationEnter()
    {
        SignCreation.SetTrigger("SignCreationEnter");
        SignCreation.gameObject.GetComponentInChildren<CombineSymbols>().ClearCurrentSign();
    }
    public void SignCreationExit()
    {
        SignCreation.SetTrigger("SignCreationExit");
    }

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
