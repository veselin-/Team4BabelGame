using UnityEngine;
using System.Collections;

public class InteractiveCam : MonoBehaviour {

    CameraManager cm;

	// Use this for initialization
	void Start () {
        cm = GameObject.FindObjectOfType<CameraManager>().GetComponent<CameraManager>();
		//Debug.Log (transform.GetChild(0).transform.position);
		//Debug.Log (transform.GetChild(0).transform.localPosition);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            cm.SetNewCamera(transform.GetChild(0).transform);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            cm.SetDefaultCamera();
        }
    }
}
