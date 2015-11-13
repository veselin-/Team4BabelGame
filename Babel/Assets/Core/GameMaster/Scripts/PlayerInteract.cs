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
    private AiMovement _ai, _playerspeed;
    private NavMeshAgent _sideKickAgent, _player;
    public Transform[] Waypoints;
    private Transform _currentWayPoint;
    private int _waypointIndex = 0;
    public static bool _targetIsNotHit = true;
    float speed;

    void Start ()
    {
        _ai = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick).GetComponent<AiMovement>();
        _playerspeed = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<AiMovement>();
        speed = _playerspeed.MovementSpeed;
        _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<NavMeshAgent>();
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
                _targetIsNotHit = false;
                _sideKickAgent.Stop();
                WaypointAction action = _currentWayPoint.gameObject.GetComponent<WaypointAction>();
                if (action != null)
                {
                    action.SideKickPerformAction();
                }
                _player.ResetPath();
                _player.Resume();
                GoToNextWaypoint();
            }
        }
    }

    //void OnCollisionEnter(Collision col)
    //{

    //}

    //IEnumerator PerformAnimation()
    //{

    //    yield return new WaitForSeconds(2);
    //}

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
