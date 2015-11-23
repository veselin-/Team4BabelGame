using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class UnlockAllLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CheatAllLevels()
    {
        PlayerPrefs.SetString(Constants.PlayerPrefs.UserLevels, "all");
    }

}
