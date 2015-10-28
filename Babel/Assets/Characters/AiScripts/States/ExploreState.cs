using Assets.Core.NavMesh;
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
            _agent.destination = Waypoints[Random.Range(0, Waypoints.Length)].position;
        }
    }
}

