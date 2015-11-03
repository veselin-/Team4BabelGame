using UnityEngine;
using System.Collections;

public class LoadDB : MonoBehaviour {

    private DatabaseManager db;

    // Use this for initialization
    void Awake () {
        db = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
