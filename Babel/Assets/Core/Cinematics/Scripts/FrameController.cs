using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{

    public GameObject NextFrame;

    public bool AnimateFrame = false;

    public string Animation;

    [Tooltip("This only applies if there is no animations attached to the frame.")]
    public float FrameLength = 3f;

    public GameObject TextField;


    private Animator animator;



	// Use this for initialization
	void Start ()
	{

	    animator = GetComponent<Animator>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartFrame()
    {
        GetComponent<Image>().enabled = true;
        TextField.SetActive(true);

        if (AnimateFrame)
        {
            StartCoroutine(RunFrameWithAnimation());
        }
        else
        {
            StartCoroutine(RunFrame());
        }

    }

    IEnumerator RunFrame()
    {
        yield return new WaitForSeconds(FrameLength);

        GetComponent<Image>().enabled = false;
        TextField.SetActive(false);

        NextFrame.GetComponent<FrameController>().StartFrame();
    }

    IEnumerator RunFrameWithAnimation()
    {
        animator.SetTrigger(Animation);

        yield return AnimationDone;

    }
}
