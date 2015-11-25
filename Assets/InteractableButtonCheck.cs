using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractableButtonCheck : MonoBehaviour {

    Button bt;
    public GameObject petlock;

    void Start() {
        bt = GetComponent<Button>();
    }

    public float RGBtoColorValue(int value)
    {
        if (value < 0 || value > 255)
            throw new System.Exception("Invalid value " + value + ". The value must be between 0 and 255");          
        return value/255.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (bt.interactable)
        {
            // bt.GetComponentInChildren<Text>().color = new Color(RGBtoColorValue(50), RGBtoColorValue(50), RGBtoColorValue(50));
            petlock.active = false;
            bt.GetComponentInChildren<Text>().enabled = true;


        }


        else
        {
            // bt.GetComponentInChildren<Text>().color = new Color(RGBtoColorValue(130), RGBtoColorValue(130), RGBtoColorValue(130));
            petlock.active = true;
            bt.GetComponentInChildren<Text>().enabled = false;
        }
    }
}
