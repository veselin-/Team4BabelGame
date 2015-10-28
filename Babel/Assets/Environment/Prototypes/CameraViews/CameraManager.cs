using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public float positionSpeed = 5f;
    public float rotationSpeed = 5f;
    public Transform Target;
    public float FollowSpeed = 15f;
    private Vector3 velocity = Vector3.zero;

    private Transform newCamera;
    private bool isNewCamera = false;

    public Transform CamZ;
    private Vector3 CamOffset;
    private Transform defaultCam;

    // Use this for initialization
    void Start () {
        defaultCam = transform;
        newCamera = transform;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        CamOffset = CamZ.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isNewCamera)
        {
            //CamZ.localPosition = Vector3.Lerp(CamZ.position, CamOffset, FollowSpeed * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref velocity, FollowSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultCam.rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
          //  CamZ.position = Vector3.Lerp(CamZ.position, new Vector3(0f, 0f, 0f), FollowSpeed * Time.deltaTime);
            LerpCamera(newCamera);
        }

	}

    public void SetNewCamera(Transform camera)
    {
		Debug.Log (camera.position);
        newCamera = camera;
        isNewCamera = true;
    }

    public void SetDefaultCamera()
    {
        isNewCamera = false;
    }

    public void LerpCamera(Transform camera)
    {
        transform.position = Vector3.Lerp(transform.position, camera.position, positionSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, camera.rotation, rotationSpeed * Time.deltaTime);
        //CamZ.localPosition = Vector3.Lerp(CamZ.localPosition, new Vector3(0f, 0f, 0f), FollowSpeed * Time.deltaTime);
    }
}
