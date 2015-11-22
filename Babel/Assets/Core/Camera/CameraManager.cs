using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class CameraManager : MonoBehaviour {
	
	private Transform Player;
	private Transform SideKick;
	
	public static bool HaveMovedCamera;
	public static bool HaveRotatedCameraClock;
	public static bool HaveRotatedCameraCounterClock;
	public static bool HaveZoomedCamera;
	
	private Transform _cameraHolder;
	private Transform _cameraZoom;
	
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	private Vector3 pinchZ = Vector3.zero;
	public float MaxPinch = 7f;
	public float MinPinch = -30f;
	
	public float dragSpeed = 2f;
	private float _minDragSpeed = 0;
	private float _maxDragSpeed = 0;
	private float _normalizeDragSpeed;

	// ROTATION GESTURE
	public float rotationSpeed = 2f;
	// END ROTATION
	
	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;
	
	public bool isCameraDragging = false;
	public bool isCameraRotating = false;
	public bool isCameraZooming = false;

	private Vector2 _v2_currentDistance = Vector2.zero;
	private Vector2 _v2_previousDistance = Vector2.zero;
	private float _f_touch_delta = 0;
	
	private CameraMovementArea cameraMovementArea;
	private bool isInsideArea = true;
	public bool isCharacterMoving = false;
	public bool isTouchDown;
	// Use this for initialization
	void Start () {
		
		if(GameObject.FindGameObjectWithTag (Constants.Tags.Player) != null)
			Player = GameObject.FindGameObjectWithTag (Constants.Tags.Player).transform;
		
		if(GameObject.FindGameObjectWithTag (Constants.Tags.SideKick) != null)
			SideKick = GameObject.FindGameObjectWithTag (Constants.Tags.SideKick).transform;
		
		_cameraHolder = transform.GetChild (0);
		_cameraZoom = _cameraHolder.transform.GetChild (0).transform.GetChild(0);
		
		Transform Zoom = transform.GetChild (0).GetChild (0).GetChild (0);
		Zoom.transform.localPosition = new Vector3(0, 0, (MaxPinch + MinPinch) / 2f);
		cameraMovementArea = transform.FindChild ("CameraHook").GetComponent<CameraMovementArea>();
		
		pinchZ.z = Zoom.transform.localPosition.z;
		_maxDragSpeed = dragSpeed;
		_minDragSpeed = 0.5f;
		NormalizeDragSpeed();
	}

	// Update is called once per frame
	void Update () {
		if (isCharacterMoving)
			return;

		DragCamera ();
		RotateAndZoom ();
	}
	
	private float NormalizeZoom()
	{
		float scaleVal = (MinPinch - pinchZ.z) / (MinPinch - MaxPinch);
		return scaleVal;
	}
	
	private void NormalizeDragSpeed()
	{
		float scaleVal = _maxDragSpeed - (NormalizeZoom() * _maxDragSpeed);
		_normalizeDragSpeed = Mathf.Clamp(scaleVal, _minDragSpeed, _maxDragSpeed);
	}
	
	void DragCamera()
	{
		int touchCount = Input.touchCount;
		
		if (touchCount == 2) {
			isCameraDragging = false;
		}
		else if(touchCount == 1)
		{
			isCameraZooming = false;
			isCameraRotating = false;
		}
		
		// Drag camera
		if((touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0) ){
			hit_position = Input.mousePosition;
			camera_position = transform.position;
			isCameraDragging = true;
		}
		
		if((touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)){
			if(isTouchDown)
			{
				hit_position = Input.mousePosition;
				camera_position = transform.position;
				isCameraDragging = true;
			}
			isTouchDown = false;
			current_position = Input.mousePosition;
			if(isCameraDragging)
				LeftMouseDrag();     
		}
		
		if ((touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp (0)) {
			HaveMovedCamera = true;
			isCameraDragging = false;
			isCameraZooming = false;
			isCameraRotating = false;
			
			if(!cameraMovementArea.isInsideArea)
			{
				LerpBackToMovementArea();
				isInsideArea = false;
			}
		}
		
		if(!isInsideArea)
		{
			LerpBackToMovementArea();
			if(cameraMovementArea.isInsideArea)
			{
				isInsideArea = true;
			}
		}
	}
	
	public void LerpBackToMovementArea()
	{
		Vector3 lastPos = cameraMovementArea.lastPos;
		lastPos.y = transform.position.y;
		transform.position = Vector3.Lerp (transform.position, lastPos, Time.deltaTime * 5f);
		
	}
	
	void RotateAndZoom()
	{
		if (Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Moved && Input.GetTouch (1).phase == TouchPhase.Moved && !isCameraDragging) {
			_v2_currentDistance = Input.GetTouch (0).position - Input.GetTouch (1).position;
			_v2_previousDistance = ((Input.GetTouch (0).position - Input.GetTouch (0).deltaPosition) - (Input.GetTouch (1).position - Input.GetTouch (1).deltaPosition));
			_f_touch_delta = _v2_currentDistance.magnitude - _v2_previousDistance.magnitude;
			
			var angleOffset = Vector2.Angle (_v2_previousDistance, _v2_currentDistance);
			var CrossVector = Vector3.Cross (_v2_previousDistance, _v2_currentDistance);
			
			isCameraDragging = false;
			
			if (angleOffset > 0.2f)
			{
				//isCameraRotating = true;
				if (CrossVector.z > 0) {
					HaveRotatedCameraClock = true;
					transform.Rotate (Vector3.up, angleOffset * rotationSpeed);
				} else if (CrossVector.z < 0) {
					HaveRotatedCameraCounterClock = true;
					transform.Rotate (Vector3.up, -1f * angleOffset * rotationSpeed);
				}
			}
			
			if (Mathf.Abs (_f_touch_delta) > 1f)
			{
				HaveZoomedCamera = true;
				//isCameraZooming = true;
				if (_f_touch_delta >= 0) {
					pinchZ.x = 0f;
					pinchZ.y = 0f;
					pinchZ.z = Mathf.Clamp (Mathf.Lerp (pinchZ.z, pinchZ.z + perspectiveZoomSpeed * Mathf.Abs (_f_touch_delta), Time.deltaTime * 2f), MinPinch, MaxPinch);
					_cameraZoom.localPosition = pinchZ;
				} else if (_f_touch_delta < 0) {
					pinchZ.x = 0f;
					pinchZ.y = 0f;
					pinchZ.z = Mathf.Clamp (Mathf.Lerp (pinchZ.z, pinchZ.z - perspectiveZoomSpeed * Mathf.Abs (_f_touch_delta), Time.deltaTime * 2f), MinPinch, MaxPinch);
					_cameraZoom.localPosition = pinchZ;
				}
				NormalizeDragSpeed();
			}
		} else {
			//isCameraRotating = false;
			//isCameraZooming = false;
		}
	}
	
	void LeftMouseDrag(){
		// From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
		// with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
		
		current_position.z = hit_position.z = camera_position.y;
		
		// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
		// anyways.  
		Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);
		
		// Invert direction to that terrain appears to move with the mouse.
		direction = direction * -1;
		direction.x = direction.x * _normalizeDragSpeed;
		direction.z = direction.z * _normalizeDragSpeed;
		
		Vector3 position = camera_position + direction;
		position.y = transform.position.y;
		
		transform.position = position;
	}
}



/*
		// working so far (junk)
		if ( Input.GetMouseButtonDown(0)){
			Origin = new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y);
			Origin = Camera.main.ScreenToWorldPoint(Origin);
			oldPos = transform.position;
			//Debug.Log(Origin);
		}
		
		if ( Input.GetMouseButton(0)){
			Vector3 currentPos = new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y);
			currentPos = Camera.main.ScreenToWorldPoint(currentPos);
			Origin.y = 0;
			currentPos.y = 0;
			Debug.Log(currentPos);
			Vector3 movePos = Origin - currentPos;
			//transform.position = new Vector3((transform.position.x + movePos.x) / panSpeed, transform.position.y, (transform.position.z + movePos.z) / panSpeed);
			//transform.position = new Vector3((oldPos.x + movePos.x) / panSpeed, oldPos.y, (oldPos.z + movePos.z) / panSpeed); 
			transform.position = transform.position + movePos;
		}
		*/

/*
		if(Input.GetMouseButtonDown(0))
		{
			bDragging = true;
			oldPos = transform.position;
			panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);                    //Get the ScreenVector the mouse clicked
		}
		
		if(Input.GetMouseButton(0))
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;    //Get the difference between where the mouse clicked and where it moved
			//Vector3 tempPos = new Vector3(pos.x, 0, pos.y);
			//new Vector3(oldPos.x + (-pos.x * panSpeed), oldPos.y, oldPos.z + (-pos.y *panSpeed));

			Transform m_Cam = Camera.main.transform;
			// calculate camera relative direction to move:
			Vector3 m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;

			transform.position = oldPos + (transform.forward )
			//transform.position = pos.x*m_CamForward + pos.y*m_Cam.right;

			//transform.position = new Vector3(oldPos.x + (-pos.x * panSpeed), oldPos.y, oldPos.z + (-pos.y *panSpeed));  //oldPos + -pos * panSpeed;                                         //Move the position of the camera to simulate a drag, speed * 10 for screen to worldspace conversion
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			bDragging = false;
		}
		*/
