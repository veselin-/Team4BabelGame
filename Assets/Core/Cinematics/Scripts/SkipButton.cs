using UnityEngine;
using System.Collections;

public class SkipButton : MonoBehaviour
{

    public string Level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Skip()
    {
        Application.LoadLevel(Level);
    }

}
