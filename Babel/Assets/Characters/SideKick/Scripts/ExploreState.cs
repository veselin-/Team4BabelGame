using System;
using Assets.Core.Configuration;
using Assets.Core.LevelMaster;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts
{
    public class ExploreState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly RoomManager rm;
        
        public ExploreState(NavMeshAgent navAgent)
        {
            _agent = navAgent;
            var gameMaster = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster);
            if (gameMaster == null || gameMaster.GetComponent<RoomManager>() == null)
                throw new Exception("We need a roommanager in this level, to runs the AI");
            rm = gameMaster.GetComponent<RoomManager>();
        }

        public void ExecuteState()
        {
            if (!_agent.HasReachedTarget()) return;

            // Pick random waypoint
            var waypoints = rm.GetCurrnetWaypoints();
            var a = UnityEngine.Random.Range(0, waypoints.Length);
            _agent.SetDestination(waypoints[a].transform.position);
        }

        public bool IsDoneExecuting()
        {
            return true;
        }
    }
}
