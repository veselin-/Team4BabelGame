using System.Linq;
using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class InteractWithNearestState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly GameObject _intaractableGoal;

        private float _waitUntill;
        private State _state;


        public InteractWithNearestState(NavMeshAgent agnet, string tag)
        {
            _agent = agnet;

            var interactables = GameObject.FindGameObjectsWithTag(tag);
            if(interactables.Length < 1)
                Debug.LogError("No interactables in scene, FUCK!");

            _intaractableGoal = interactables.OrderBy(i => 
                Vector3.Distance(_agent.transform.position, i.transform.position)).FirstOrDefault();
        }

        public InteractWithNearestState(NavMeshAgent agnet, GameObject goal)
        {
            _agent = agnet;
            _intaractableGoal = goal;
        }

        public float WaitingTime { get; set; }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _agent.SetDestination(_intaractableGoal.transform.position);
                    _state = State.GoToIntactable;
                    return;
                case State.GoToIntactable:
                    if (_agent.HasReachedTarget())
                        _state = Vector3.Distance(_intaractableGoal.transform.position, _agent.transform.position) < 2 ? 
                            State.Interact : State.Done;
                    return;
                case State.Interact:
                    var puh = _agent.gameObject.GetComponent<PickupHandler>();
                    var returnItem = _intaractableGoal.GetComponent<IInteractable>().Interact(puh.CurrentPickup);
                    puh.PickUpItem(returnItem);

                    _waitUntill = Time.time + WaitingTime;
                    _state = State.WaitSomeTime;
                    return;
                case State.WaitSomeTime:
                    if (_waitUntill < Time.time)
                        _state = State.Done;
                    return;
                default:
                    return;
            }
        }

        public bool IsDoneExecuting()
        {
            return _state == State.Done;
        }

        enum State
        {
            Neutral, GoToIntactable, Interact, WaitSomeTime, Done
        }
    }
}
