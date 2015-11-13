using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class CameraManager : MonoBehaviour {

	public Transform Player;
	public Transform SideKick;

	/*
	public Vector3 offset = new Vector3(-10f, 15f, -15f);
	public Vector3 camRot = new Vector3(35f, 35f, 0f);
	*/

	private Transform _cameraHolder;
	private Transform _cameraZoom;

	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	private Vector3 pinchZ = Vector3.zero;
	public float MaxPinch = 7f;
	public float MinPinch = -30f;
	
	public float dragSpeed = 2f;

	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;

	public bool isCameraDragging = false;
	public bool isCameraRotating = false;
	public bool isCameraZooming = false;

	// ROTATION GESTURE
	public Transform RotationGesture;
	private bool rotating = false;
	private Vector2 startVector = Vector2.zero;
	private float rotGestureWidth = 20f;
	private float rotAngleMinimum = 5f;
	public float rotationSpeed = 2f;
    // END ROTATION


	public Vector2 v2_currentDistance = Vector2.zero;
	public Vector2 v2_previousDistance = Vector2.zero;
	public float f_touch_delta = 0;
    
	private CameraMovementArea cameraMovementArea;
	public bool isInsideArea = true;
	public bool isCharacterMoving = false;
	public bool isTouchDown = false;
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

	}
	

	// Update is called once per frame
	void Update () {
	
		if (isCharacterMoving)
			return;

		//Debug.Log ("drag camera");
		DragCamera ();
		RotateAndZoom ();
		

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
			//Debug.Log("drag");
			hit_position = Input.mousePosition;
			camera_position = transform.position;
			isCameraDragging = true;
		}

		if((touchCount == 1  && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)){
			//Debug.Log("moved");
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

		if (Input.GetMouseButtonUp (0)) {
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
			v2_currentDistance = Input.GetTouch (0).position - Input.GetTouch (1).position;
			v2_previousDistance = ((Input.GetTouch (0).position - Input.GetTouch (0).deltaPosition) - (Input.GetTouch (1).position - Input.GetTouch (1).deltaPosition));
			f_touch_delta = v2_currentDistance.magnitude - v2_previousDistance.magnitude;

			var angleOffset = Vector2.Angle (v2_previousDistance, v2_currentDistance);
			var CrossVector = Vector3.Cross (v2_previousDistance, v2_currentDistance);

			isCameraDragging = false;

			if (angleOffset > 0.1f) {
				if (CrossVector.z > 0) {
					transform.Rotate (Vector3.up, angleOffset * rotationSpeed);
				} else if (CrossVector.z < 0) {
					transform.Rotate (Vector3.up, -1f * angleOffset * rotationSpeed);
				}
			}
			//Debug.Log("angleOffset " + angleOffset);
			//Debug.Log("f_touch_delta " + f_touch_delta);
			if (Mathf.Abs (f_touch_delta) > 1f) {
				if (f_touch_delta >= 0) {
					//pinchZ -= new Vector3(0f,0f, pinchValue);
					//pinchZ.z = Mathf.Clamp(pinchZ.z, MinPinch, MaxPinch);
					pinchZ.x = 0f;
					pinchZ.y = 0f;
					pinchZ.z = Mathf.Clamp (Mathf.Lerp (pinchZ.z, pinchZ.z + perspectiveZoomSpeed * Mathf.Abs (f_touch_delta), Time.deltaTime * 2f), MinPinch, MaxPinch);

					//Transform camera = Camera.main.transform;
					_cameraZoom.localPosition = pinchZ;


				} else if (f_touch_delta < 0) {
					pinchZ.x = 0f;
					pinchZ.y = 0f;
					pinchZ.z = Mathf.Clamp (Mathf.Lerp (pinchZ.z, pinchZ.z - perspectiveZoomSpeed * Mathf.Abs (f_touch_delta), Time.deltaTime * 2f), MinPinch, MaxPinch);
					
					//Transform camera = Camera.main.transform;
					_cameraZoom.localPosition = pinchZ;
					//Debug.Log("zoomOut");
				}
			}
		} else {
			isCameraRotating = false;
			isCameraZooming = false;
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
		direction.x = direction.x * dragSpeed;
		direction.z = direction.z * dragSpeed;

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
