using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SymbolHandler : DragHandler
{

    public int ID;

    public Image Image1;
    public Image Image2;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void PlaySound()
   {

        //AudioManager.Play(ID);

   }

    public void SetSyllables(GameObject syl1, GameObject syl2)
    {
        Image1.sprite = syl1.GetComponent<Image>().sprite;
        Image2.sprite = syl2.GetComponent<Image>().sprite;

    }

}
