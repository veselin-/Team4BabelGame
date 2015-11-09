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

	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	private Vector3 pinchZ = Vector3.zero;
	public float MaxPinch = 7f;
	public float MinPinch = -30f;
	
	public float dragSpeed = 2f;

	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;

	// ROTATION GESTURE
	public Transform RotationGesture;
	private bool rotating = false;
	private Vector2 startVector = Vector2.zero;
	private float rotGestureWidth = 5f;
	private float rotAngleMinimum = 5f;
    // END ROTATION

	// Use this for initialization
	void Start () {

		if(GameObject.FindGameObjectWithTag (Constants.Tags.Player).transform != null)
			Player = GameObject.FindGameObjectWithTag (Constants.Tags.Player).transform;

		if(GameObject.FindGameObjectWithTag (Constants.Tags.SideKick).transform != null)
			SideKick = GameObject.FindGameObjectWithTag (Constants.Tags.SideKick).transform;

		_cameraHolder = transform.GetChild (0);

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			hit_position = Input.mousePosition;
			camera_position = transform.position;
			
		}
		if(Input.GetMouseButton(0)){
			current_position = Input.mousePosition;
			LeftMouseDrag();        
		}

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
			
			pinchZ -= new Vector3(0f,0f, pinchValue);
			pinchZ.z = Mathf.Clamp(pinchZ.z, MinPinch, MaxPinch);
			
			//Transform camera = Camera.main.transform;
			_cameraHolder.localPosition = pinchZ;
		}


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
							
							Vector3 tempRot = transform.eulerAngles;
							tempRot += new Vector3(0, angleOffset, 0) * 0.01f;
							transform.eulerAngles = tempRot;
							
							//RotationGesture.localRotation = Quaternion.Euler (0, angleOffset, 0);
							
							
                        } else if (LR.z < 0) {
                            // Clockwise turn equal to angleOffset.
                            
                            Vector3 tempRot = transform.eulerAngles;
                            tempRot -= new Vector3(0, angleOffset, 0) * 0.01f;
                            transform.eulerAngles = tempRot;
                            
                            //RotationGesture.localRotation = Quaternion.Euler (0, angleOffset, 0);
                            
                        }
					}
					
				}
			}
			
		} else {
			rotating = false;
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
