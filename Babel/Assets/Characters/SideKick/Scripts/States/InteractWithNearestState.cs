using System.Linq;
using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts.States
{
    public class InteractWithNearestState : IState
    {
        private const float MovementSpeed = 3.5f;
        private const float WaitingTime = 10;

        private readonly NavMeshAgent _agent;
        private readonly GameObject _intaractableGoal;

        private bool _isDoneExecuting;
        private float _waitUntill;
        private State state;


        public InteractWithNearestState(NavMeshAgent agnet, string tag)
        {
            _agent = agnet;
            _agent.speed = MovementSpeed;

            var interactables = GameObject.FindGameObjectsWithTag(tag);
            if(interactables.Length < 1)
                Debug.LogError("No interactables in scene, FUCK!");

            _intaractableGoal = interactables.OrderBy(i => 
                Vector3.Distance(_agent.transform.position, i.transform.position)).FirstOrDefault();
        }

        public void ExecuteState()
        {
            switch (state)
            {
                case State.Neutral:
                    _agent.SetDestination(_intaractableGoal.transform.position);
                    state = State.GoToLever;
                    return;
                case State.GoToLever:
                    if (_agent.HasReachedTarget())
                        state = State.PullLever;
                    return;
                case State.PullLever:
                    _intaractableGoal.GetComponent<IInteractable>().Interact();
                    _waitUntill = Time.time + WaitingTime;
                    state = State.WaitSomeTime;
                    return;
                case State.WaitSomeTime:
                    if (_waitUntill < Time.time)
                        state = State.Done;
                    return;
                default:
                    return;
            }
        }

        public bool IsDoneExecuting()
        {
            return state == State.Done;
        }

        enum State
        {
            Neutral, GoToLever, PullLever, WaitSomeTime, Done
        }
    }
}
