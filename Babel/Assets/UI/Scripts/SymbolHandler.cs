using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SymbolHandler : MonoBehaviour
{

    public Image Image1;
    public Image Image2;
    public AudioSource Audio1;
    public AudioSource Audio2;

    public float SyllableAudioSpacing = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void PlaySound()
   {

        Audio1.Play();
        Audio2.PlayDelayed(Audio1.clip.length + SyllableAudioSpacing);

   }

    public void SetSyllables(GameObject syl1, GameObject syl2)
    {
        Image1.sprite = syl1.GetComponent<Image>().sprite;
        Image2.sprite = syl2.GetComponent<Image>().sprite;

        Audio1.clip = syl1.GetComponent<AudioSource>().clip;
        Audio2.clip = syl2.GetComponent<AudioSource>().clip;
    }

}
