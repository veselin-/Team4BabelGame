using UnityEngine;
using System.Collections;

public class sentenceSlots : MonoBehaviour {

    public static bool haveChild = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.childCount == 1)
        {
            haveChild = true;
        }
	}
}
