using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	private CameraControl cameraController;
	private Transform attachedCamera;
	public float LerpSpeed = 2f;
	public bool switchImmediately = false;
	// Use this for initialization
	void Start () {
		cameraController = GameObject.FindObjectOfType<CameraControl> ().GetComponent<CameraControl>();  //get camera controller 
		attachedCamera = transform.GetChild (0).transform; //get the attached camera
	}
	

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player")
		{
			if(!switchImmediately)
			{
				cameraController.LerpSpeed = LerpSpeed;
				cameraController.cameraSwitcher = true;
				cameraController.nextCamera = attachedCamera;
			}
			else
			{
				cameraController.SwitchCameraImmediately(attachedCamera);
				cameraController.enabled = false;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Player")
		{
			if(!switchImmediately)
			{
				cameraController.cameraSwitcher = false;
			}
			else
			{
				cameraController.SwitchCameraToDefaultState();
				cameraController.enabled = true;
			}
		}
	}


}
