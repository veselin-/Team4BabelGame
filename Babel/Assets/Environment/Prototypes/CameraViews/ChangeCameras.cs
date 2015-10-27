using UnityEngine;
using System.Collections;

public class ChangeCameras : MonoBehaviour {

    public GameObject OIsometric;
    public GameObject PIsometric;

	// Use this for initialization
	void Start () {
	
	}

    public void ChangeCamera()
    {
        if (OIsometric.activeSelf)
        {
            PerspectiveIsometric();
        }
        else
        {
            OrthographicIsometric();
        }
    }

    private void OrthographicIsometric()
    {
        OIsometric.SetActive(true);
        PIsometric.SetActive(false);
    }

    private void PerspectiveIsometric()
    {
        OIsometric.SetActive(false);
        PIsometric.SetActive(true);
    }
}
