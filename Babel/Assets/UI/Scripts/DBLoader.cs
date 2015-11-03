using UnityEngine;
using System.Collections;

public class DBLoader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	
        GetComponent<DatabaseManager>().LoadData();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
