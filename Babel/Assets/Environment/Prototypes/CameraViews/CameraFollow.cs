using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform Target;
    public float speed = 1f;
    private Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       
        transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref velocity, speed * Time.deltaTime);
	}
}
