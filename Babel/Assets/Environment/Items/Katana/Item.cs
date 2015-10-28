using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public static bool pressed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        pressed = true;
        Debug.Log("THIS IS NOW PICKED UP");
    }
}
