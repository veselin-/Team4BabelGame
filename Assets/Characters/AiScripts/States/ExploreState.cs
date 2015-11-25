using System.Linq;
using Assets.Core.NavMesh;
//using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Characters.AiScripts.States
{
    public class ExploreState : IState
    {
        public Transform[] Waypoints {
            set {
                _waypoints = value;
                PickWaypoint();
            }
            get { return _waypoints; }
        }
        private Transform[] _waypoints;

        private readonly float _movementSpeed;
        private readonly NavMeshAgent _agent;
        public float WaitingTime { get; set; }

        public ExploreState(NavMeshAgent navAgent, float movementSpeed)
        {
            _movementSpeed = movementSpeed;
            _agent = navAgent;
        }

        public void ExecuteState()
        {
            // Make sure we go back to default speed

            _agent.speed = _movementSpeed;

            if (_movementSpeed < 0.1f)
                return;

           

            // Continue only if we have reached taget, or the room numner has changes 
            if (!_agent.HasReachedTarget()) return;
            PickWaypoint();
        }

        public bool IsDoneExecuting()
        {
            return true;
        }

        private void PickWaypoint()
        {
            var waypoints = Waypoints.OrderBy(w => Random.Range(0, Waypoints.Length));
            Transform waypoint = null;
            foreach (var i in waypoints)
            {
                var path = new NavMeshPath();
                _agent.CalculatePath(i.position, path);

                if (path.status != NavMeshPathStatus.PathComplete) continue;

                waypoint = i;
                break;
            }

            if(waypoint != null)
                _agent.SetDestination(waypoint.position);
        }
    }
}