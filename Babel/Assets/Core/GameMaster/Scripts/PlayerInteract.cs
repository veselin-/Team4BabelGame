using UnityEngine;
using System.Collections;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Characters.Player.Scripts;
using Assets.Core.Configuration;
using Assets.Core.NavMesh;

public class PlayerInteract : MonoBehaviour
{
    public bool PlayerCanAct = true;
    private PlayerMovement _playerMovementScript;
    private AiMovement _ai;
    private NavMeshAgent _sideKickAgent;
    public Transform[] Waypoints;
    private Transform _currentWayPoint;
    private int _waypointIndex = 0;
    private bool _targetIsNotHit = true;

    void Start ()
    {
        _ai = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick).GetComponent<AiMovement>();
        _sideKickAgent = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick).GetComponent<NavMeshAgent>();
        _playerMovementScript = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<PlayerMovement>();
	    _playerMovementScript.enabled = PlayerCanAct;
        GoToNextWaypoint();
    }

    private void Update()
    {
        if (_targetIsNotHit)
        {
            if (_sideKickAgent.HasReachedTarget())
            {
                StartCoroutine("PerformAnimation");
                WaypointAction action = _currentWayPoint.gameObject.GetComponent<WaypointAction>();
                if (action != null)
                {
                    action.SideKickPerformAction();
                }
                GoToNextWaypoint();
            }
        }
    }
    IEnumerator PerformAnimation()
    {
        _targetIsNotHit = false;
        _sideKickAgent.Stop();
        yield return new WaitForSeconds(2);
        _targetIsNotHit = true;
        _sideKickAgent.Resume();
    }

    public void GoToNextWaypoint()
    {
        if (_waypointIndex != Waypoints.Length)
        {
            _currentWayPoint = Waypoints[_waypointIndex++];
            _ai.AssignNewState(new GoSomewhereAndWaitState(_sideKickAgent, _currentWayPoint.position));
        }
    }

    public void ChangePlayerInteractState(bool state)
    {
        _playerMovementScript.enabled = state;
    }
}
