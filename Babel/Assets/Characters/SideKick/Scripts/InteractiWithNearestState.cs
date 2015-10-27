using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts
{
    public class InteractiWithNearestState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly GameObject _intaractableGoal;

        private bool _isDoneExecuting;

        public InteractiWithNearestState(NavMeshAgent navAgent, string tag)
        {
            _agent = navAgent;
            var interactables = GameObject.FindGameObjectsWithTag(tag);
            if(interactables.Length < 1)
                Debug.LogError("No interactables in scene, FUCK!");

            _intaractableGoal = interactables[Random.Range(0, interactables.Length)];
            _agent.SetDestination(_intaractableGoal.transform.position);
        }

        public void ExecuteState()
        {
            if (!_agent.HasReachedTarget()) return;
            _intaractableGoal.GetComponent<IInteractable>().Interact();
            _isDoneExecuting = true;
        }

        public bool IsDoneExecuting()
        {
            return _isDoneExecuting;
        }
    }
}
