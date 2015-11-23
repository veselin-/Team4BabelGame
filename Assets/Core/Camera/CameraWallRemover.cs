using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.Rendering;

public class CameraWallRemover : MonoBehaviour
{

    public Transform target;

	// Use this for initialization
	void Start () {
	   // InvokeRepeating("SphereCast", Time.timeSinceLevelLoad, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	

     //   SphereCast();


	}


    void SphereCast()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 10, (target.position - transform.position).normalized, out hit, 10))
        {
            if (hit.transform.tag == Constants.Tags.Wall)
            {
                StartCoroutine(RenderChange(hit.transform.gameObject));
            }
        }
    }

    IEnumerator RenderChange(GameObject wall)
    {
        wall.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;

        yield return new WaitForSeconds(2f);

        wall.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;
    }

    void OnTriggerStay(Collider wall)
    {
        if (Vector3.Dot(wall.transform.right.normalized, (Camera.main.transform.position - wall.transform.position).normalized) < 0f)
        {
            wall.transform.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }
        else
        {
            wall.transform.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;
        }
    }

    void OnTriggerExit(Collider wall)
    {

            wall.transform.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;

    }

}
