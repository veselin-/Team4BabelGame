using System;
using Assets.Core.Configuration;
using Assets.Core.LevelMaster;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts.States
{
    public class ExploreState : IState
    {
        private readonly float _movementSpeed;
        private readonly NavMeshAgent _agent;
        private readonly RoomManager _rm;
        
        public ExploreState(NavMeshAgent navAgent, float movementSpped)
        {
            _movementSpeed = movementSpped;
            _agent = navAgent;
            var gameMaster = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster);
            if (gameMaster == null || gameMaster.GetComponent<RoomManager>() == null)
                throw new Exception("We need a roommanager in this level, to runs the AI");
            _rm = gameMaster.GetComponent<RoomManager>();
        }

        public void ExecuteState()
        {
            _agent.speed = _movementSpeed;
            if (!_agent.HasReachedTarget()) return;
            // Pick random waypoint
            var waypoints = _rm.GetCurrnetWaypoints();
            var a = UnityEngine.Random.Range(0, waypoints.Length);
            _agent.SetDestination(waypoints[a].transform.position);
        }

        public bool IsDoneExecuting()
        {
            return true;
        }
    }
}
