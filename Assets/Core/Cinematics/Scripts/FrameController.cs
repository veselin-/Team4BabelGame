using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Core.Configuration;

[RequireComponent(typeof(AudioSource))]
public class FrameController : MonoBehaviour
{

    public GameObject NextFrame;

    public bool AnimateFrame = false;

    public string Animation;

    [Tooltip("This only applies if there is no animations attached to the frame.")]
    public float FrameLength = 3f;

    [Tooltip("Must only be true for the first frame of the cinematic or it will break horribly.")]
    public bool IsStartingFrame = false;

    [Tooltip("Must only be true for the last frame of the cinematic or it will break horribly.")]
    public bool IsEndFrame = false;

    public AudioClip EnglishAudio;
    public AudioClip DanishAudio;
    
    public string LevelToLoad;
    
    private Animator animator;
    private Text textBox;
    private AudioSource audio;

    [TextArea, Tooltip("Write Phrases/... to get the correct version of the text.")]
    public string Text;

	// Use this for initialization
	void Start ()
	{
	    textBox = GameObject.FindGameObjectWithTag("CinematicText").GetComponent<Text>();
	    animator = GetComponent<Animator>();
	    audio = GetComponent<AudioSource>();

        // Localized audio
	    var isDanish = PlayerPrefs.GetString("Language") == Constants.Languages.Danish;
        audio.clip = isDanish ? DanishAudio : EnglishAudio;

	    if (IsStartingFrame)
	    {
	        StartFrame();
	    }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartFrame()
    {
        GetComponent<Image>().enabled = true;


            if (AnimateFrame)
            {
                StartCoroutine(RunFrameWithAnimation());
            }
            else
            {
                StartCoroutine(RunFrame());
            }

        if (Text != String.Empty)
        {
            textBox.text = LanguageManager.Instance.Get(Text);
        }

        if (audio.clip != null)
        {
            audio.Play();
        }

    }

    IEnumerator RunFrame()
    {
        yield return new WaitForSeconds(FrameLength);



    //    Debug.Log(IsEndFrame);

        if (IsEndFrame)
        {
         //   Debug.Log("Loading Level");
           // Application.LoadLevel(LevelToLoad);
            GameObject.FindGameObjectWithTag("SkipButton").GetComponent<SkipButton>().ActivateScene();

        }
        else
        {
            if (NextFrame != null)
            {
				NextFrame.GetComponent<FrameController>().StartFrame();
				yield return new WaitForEndOfFrame();
                GetComponent<Image>().enabled = false;
            
            }
        }
        
    }

    IEnumerator RunFrameWithAnimation()
    {
       

     //   yield return new WaitForEndOfFrame();

        animator.SetTrigger(Animation);

        yield return new WaitForFixedUpdate();

		//Debug.Log (animator.GetCurrentAnimatorStateInfo (0).normalizedTime);

		while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99)
        {
            yield return new WaitForEndOfFrame();
        }


        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
       // yield return new WaitForSeconds(FrameLength);


        if (IsEndFrame)
        {
            GameObject.FindGameObjectWithTag("SkipButton").GetComponent<SkipButton>().ActivateScene();
           // Application.LoadLevel(LevelToLoad);
           // Debug.Log("Loading Level");
        }
        else
        {
            if (NextFrame != null)
            {

                NextFrame.GetComponent<FrameController>().StartFrame();
				yield return new WaitForEndOfFrame();
                //yield return new WaitForEndOfFrame();
				//yield return new WaitForEndOfFrame();
				//yield return new WaitForEndOfFrame();
				//yield return new WaitForEndOfFrame();
				//yield return new WaitForEndOfFrame();
				//yield return new WaitForEndOfFrame();
				GetComponent<Image>().enabled = false;


            }
        }

    }
}
