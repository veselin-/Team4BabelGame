using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class FollowThisState : IState
    {

        private readonly GameObject _target;
        private readonly NavMeshAgent _agent;

        public float WaitingTime { get; set; }

        public FollowThisState(NavMeshAgent agent, GameObject itemToFollow)
        {
            _target = itemToFollow;
            _agent = agent;
        }
        
        public void ExecuteState()
        {
            _agent.SetDestination(Vector3.Distance(_agent.transform.position, _target.transform.position) < 3
                ? _agent.transform.position
                : _target.transform.position);
        }

        public bool IsDoneExecuting()
        {
            // Should maybe be determed by mood?
            return false;
        }
    }
}
