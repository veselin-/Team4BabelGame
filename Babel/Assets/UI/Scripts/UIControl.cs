using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour
{

    public Animator SignBook;
    public Animator SignCreation;
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
    }
    public void SignCreationExit()
    {
        SignCreation.SetTrigger("SignCreationExit");
    }

}
