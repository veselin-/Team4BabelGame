using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateBuyButtons : MonoBehaviour {

    public Image buyb1, buyb2, buyb3, buyb4, buyb5, buyb, buy123, buy456, buyall;
    public Sprite checkmark;

    // Use this for initialization
    void Start () {
        PlayerPrefsBool.SetBool("Pack", false);
        PlayerPrefsBool.SetBool("Pack (1)", false);
        PlayerPrefsBool.SetBool("Pack (2)", false);
        PlayerPrefsBool.SetBool("Pack (3)", false);
        PlayerPrefsBool.SetBool("Pack (4)", false);
        PlayerPrefsBool.SetBool("Pack (5)", false);
        PlayerPrefsBool.SetBool("Pack123", false);
        PlayerPrefsBool.SetBool("Pack456", false);
        PlayerPrefsBool.SetBool("PackAll", false);
    }
	
	// Update is called once per frame
	void Update () {
        //if (PlayerPrefsBool.GetBool("PackAll") == true)
        //{
        //    buy123.sprite = checkmark;
        //    buy456.sprite = checkmark;
        //    Debug.Log("PACKALL");
        //}
        //else if (PlayerPrefsBool.GetBool("Pack123") == true)
        //{
        //    buyb.sprite = checkmark;
        //    buyb1.sprite = checkmark;
        //    buyb2.sprite = checkmark;
        //    Debug.Log("PACK123");
        //}
        //else if (PlayerPrefsBool.GetBool("Pack456") == true)
        //{
        //    buyb3.sprite = checkmark;
        //    buyb4.sprite = checkmark;
        //    buyb5.sprite = checkmark;
        //    Debug.Log("PACK456");
        //}
        //else if (PlayerPrefsBool.GetBool("Pack") == true)
        //    {
        //        buyb.sprite = checkmark;
        //    }
        //else if (PlayerPrefsBool.GetBool("Pack (1)") == true)
        //{
        //    buyb1.sprite = checkmark;
        //}
        //else if (PlayerPrefsBool.GetBool("Pack (2)") == true)
        //{
        //    buyb2.sprite = checkmark;
        //}
        //else if (PlayerPrefsBool.GetBool("Pack (3)") == true)
        //{
        //    buyb3.sprite = checkmark;
        //}
        //else if (PlayerPrefsBool.GetBool("Pack (4)") == true)
        //{
        //    buyb4.sprite = checkmark;
        //}
        //else if (PlayerPrefsBool.GetBool("Pack (5)") == true)
        //{
        //    buyb5.sprite = checkmark;
        //}
    }
}
