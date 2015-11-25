using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractableButtonCheck : MonoBehaviour {

    Button bt;
	void Start () {
        bt = GetComponent<Button>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (bt.interactable)
            bt.GetComponentInChildren<Text>().color = new Color(50f, 50f, 50f);
        else
            bt.GetComponentInChildren<Text>().color = new Color(130f, 130f, 130f);
    }
}
