using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Environment.Levers.LeverExample.Scripts;

public class AchievementHandler : MonoBehaviour {

    public GameObject achievement;

	// Use this for initialization
	void Start () {
        //PlayerPrefsBool.SetBool(achievement.name, false);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void RefreshAchievements()
    {
        if (LeverPulls.leverpulls == 7 && !PlayerPrefsBool.GetBool(achievement.name))
        {
            PlayerPrefsBool.SetBool(achievement.name, true);
            achievement.SetActive(true);
        }
        else
        {
            achievement.SetActive(false);
        }
    }
}
