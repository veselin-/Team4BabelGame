using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public Transform target;
    public Vector3 offset = new Vector3(-10f, 15f, -15f);
	public Vector3 camRot = new Vector3(35f, 35f, 0f);
	private Quaternion rotationOffset;
	private Vector3 destination = Vector3.zero;
	//public float PlayerFollowDelay = 3f;
	private Vector3 velocity = Vector3.zero;

	public bool cameraSwitcher = false;

	public Transform nextCamera;
	public float LerpSpeed = 2f;
	public float CameraFollowSpeed = 2f;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		rotationOffset = Quaternion.identity;
		rotationOffset.eulerAngles = camRot;

		nextCamera = transform;
	}


	void FixedUpdate()
	{

		if(cameraSwitcher)
		{
			MoveFromTo(transform, nextCamera, LerpSpeed);
		}
		else
		{
			MoveCameraToPlayer();
		}
	}

	public void SwitchCameraImmediately(Transform to)
	{
		transform.position = to.position;
		transform.rotation = to.rotation;
	}

	public void SwitchCameraToDefaultState()
	{
		rotationOffset.eulerAngles = camRot;
		destination = target.position + offset;
		transform.position = destination;
		transform.rotation = rotationOffset;
	}

	void MoveFromTo(Transform from, Transform to, float speed)
	{
			
		transform.position = Vector3.Lerp(from.transform.position, to.transform.position, speed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(from.transform.rotation, to.transform.rotation, speed * Time.deltaTime);
	}

	void MoveCameraToPlayer()
	{
		rotationOffset.eulerAngles = camRot;
		destination = target.position + offset;
		//transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity ,PlayerFollowDelay * Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, destination, CameraFollowSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotationOffset, CameraFollowSpeed * Time.deltaTime);
	}
}
