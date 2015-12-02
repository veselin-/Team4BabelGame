using UnityEngine;
using System.Collections;

public class MainMenuSound : MonoBehaviour {

    private void Awake()
    {
        if (GameObject.FindObjectsOfType<MainMenuSound>().Length > 1)
        {
            //Debug.Log(GameObject.FindObjectsOfType<MainMenuSound>().Length);
            Destroy(gameObject);
        }

        if (GameObject.FindObjectOfType<MainMenuSound>().gameObject != null)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    // Use this for initialization
	void Start () {

	    if (!GetComponent<AudioSource>().isPlaying)
	    {
            GetComponent<AudioSource>().Play();
	    }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
