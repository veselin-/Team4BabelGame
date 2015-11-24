using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;

public class WaypointSystem : MonoBehaviour
{

    private int CurrentWaypoint = 0;

    public List<GameObject> WayPoints;
	// Use this for initialization
	void Start ()
	{

	    WayPoints = GameObject.FindGameObjectsWithTag(Constants.Tags.SysWaypoint).OrderBy(x => x.name).ToList();
	   

        GetComponent<SidekickControls>().enabled = false;
        
	    StartCoroutine(FirstWaypoint());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextWaypoint()
    {
        if (CurrentWaypoint < WayPoints.Count)
        {
            WayPoints[CurrentWaypoint].GetComponent<SideKickWayPoint>().EngageWaypoint();
            CurrentWaypoint++;
        }
    }

    IEnumerator FirstWaypoint()
    {
        
        yield return new WaitForSeconds(0.5f);

        NextWaypoint();
    }

}
