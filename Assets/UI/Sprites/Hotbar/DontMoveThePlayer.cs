using UnityEngine;
using System.Collections;
using Assets.Characters.Player.Scripts;

public class DontMoveThePlayer : MonoBehaviour {

    private PlayerMovement _playerMovement;

    // Use this for initialization
    void Start () {
	    _playerMovement = GameObject.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        _playerMovement._tap = false;
        Debug.Log("JEG TRYKKEDE PÅ DIG");
    }
}
