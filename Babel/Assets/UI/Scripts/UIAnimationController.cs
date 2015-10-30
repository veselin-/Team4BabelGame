using UnityEngine;
using System.Collections;

public class UIAnimationController : MonoBehaviour
{

    public Animator Title;

    public Animator Menu;

    private float time;

	// Use this for initialization
	void Start () {

        TitleEnter();
	    MenuEnter();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    time += Time.deltaTime;

	    if (time > 5f)
	    {
	        TitleExit();
            MenuExit();
	    }

	}




  public  void TitleEnter()
    {
       // Title.clip.
        Title.SetTrigger("Enter");

    }

  public  void TitleExit()
    {

        Title.SetTrigger("Exit");

    }

    public void MenuEnter()
    {
        // Title.clip.
        Menu.SetTrigger("Enter");

    }

    public void MenuExit()
    {

        Menu.SetTrigger("Exit");

    }

}
