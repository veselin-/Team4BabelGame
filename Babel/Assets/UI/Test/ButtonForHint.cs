using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonForHint : MonoBehaviour
{

    public int ID;
    public Text hintText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void GetHintForSign()
    {
        switch (ID)
        {
            case 0:
                hintText.text = "A waving gesture for uniting people.";
                break;
            case 1:
                hintText.text = "If you tell a dog to sit it ***** put.";
                break;
            case 2:
                hintText.text = "Requires pulling.. activates mechanism..";
                break;
            case 3:
                hintText.text = "It’s brown and can be set on fire.";
                break;
            case 4:
                hintText.text = "It looks like a pan.. but I wouldn't eat anything from it!";
                break;
            case 5:
                hintText.text = "Can be used to transport fluids.";
                break;
            case 6:
                hintText.text = "It can contain several buckets of water, looks like a birdbath.";
                break;
            case 7:
                hintText.text = "An endless supply of water at your disposal!";
                break;
            case 8:
                hintText.text = "It’s tiny, it’s shiny.. but it’s the only means to get out!";
                break;
            case 9:
                hintText.text = "Can be opened with a certain shiny little object.";
                break;
            case 10:
                hintText.text = "Give or take!";
                break;
        }
    }
}
