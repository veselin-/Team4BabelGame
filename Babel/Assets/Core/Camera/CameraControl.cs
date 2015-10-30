using System;
using UnityEngine;
using UnityStandardAssets.Utility;

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
	public float FollowSpeed = 2f;
	public float CameraFollowSpeed = 2f;

	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	private Vector3 pinchZ = Vector3.zero;
	public float MaxPinch = 15f;
	public float MinPinch = -15f;

	public bool followPlayer = true;

	// ROTATION GESTURE
	public Transform RotationGesture;
	private bool rotating = false;
	private Vector2 startVector = Vector2.zero;
	private float rotGestureWidth = 5f;
	private float rotAngleMinimum = 5f;

	// END ROTATION

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		rotationOffset = Quaternion.identity;
		rotationOffset.eulerAngles = camRot;

		nextCamera = transform;

		if (!followPlayer) {
			transform.localPosition = new Vector3 (-5f, 7.5f, -7.5f);
			transform.localRotation = Quaternion.Euler (35f, 35f, 0);
			transform.parent.GetComponent<FollowTarget> ().enabled = true;
		} else {
			transform.parent.GetComponent<FollowTarget> ().enabled = false;
		}
	}

	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2 )
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			float pinchValue = deltaMagnitudeDiff * perspectiveZoomSpeed;

			pinchZ += new Vector3(0f,0f, pinchValue);
			pinchZ.z = Mathf.Clamp(pinchZ.z, MinPinch, MaxPinch);

			Transform camera = Camera.main.transform;
			camera.localPosition = pinchZ;
		}

		/*
		// rotation gesture
		if (Input.touchCount == 2) {
			if (!rotating) {
				startVector = Input.GetTouch(1).position - Input.GetTouch(0).position;
				rotating = startVector.sqrMagnitude > rotGestureWidth * rotGestureWidth;
			} else {
				var currVector = Input.GetTouch(1).position - Input.GetTouch(0).position;
				var angleOffset = Vector2.Angle(startVector, currVector);
				var LR = Vector3.Cross(startVector, currVector);

				if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
				{
				if (angleOffset > rotAngleMinimum) {
					if (LR.z > 0) {
						// Anticlockwise turn equal to angleOffset.

						Vector3 tempRot = RotationGesture.eulerAngles;
						tempRot += new Vector3(0, angleOffset, 0) * 0.01f;
						RotationGesture.eulerAngles = tempRot;

						//RotationGesture.localRotation = Quaternion.Euler (0, angleOffset, 0);

					
					} else if (LR.z < 0) {
						// Clockwise turn equal to angleOffset.

						Vector3 tempRot = RotationGesture.eulerAngles;
							tempRot -= new Vector3(0, angleOffset, 0) * 0.01f;
						RotationGesture.eulerAngles = tempRot;

						//RotationGesture.localRotation = Quaternion.Euler (0, angleOffset, 0);
	
					}
				}

				}
			}
			
		} else {
			rotating = false;
		}

		*/
	}

	void FixedUpdate()
	{
		if (followPlayer) {
			if (cameraSwitcher) {
				MoveFromTo (transform, nextCamera, FollowSpeed);
			} else {
				MoveCameraToPlayer ();
			}
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
