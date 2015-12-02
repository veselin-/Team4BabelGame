using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loadsixseveneigth : MonoBehaviour {

    public GameObject bucket, fond, pool;

	// Use this for initialization
	void Start () {
        bucket.SetActive(true);
        fond.SetActive(true);
        pool.SetActive(true);
        bucket.transform.parent.GetComponent<Image>().color = new Color32(94, 40, 40, 106);
        fond.transform.parent.GetComponent<Image>().color = new Color32(94, 40, 40, 106);
        pool.transform.parent.GetComponent<Image>().color = new Color32(94, 40, 40, 106);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
