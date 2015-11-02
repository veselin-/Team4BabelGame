using System;
using System.Linq;
using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class InteractWithNearestState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly IInteractable _intaractableGoal;
        private float _waitUntill;
        private State _state;
        
        public InteractWithNearestState(NavMeshAgent agnet, string tag, GameObject pickup)
        {
            _agent = agnet;

            var interactables = GameObject.FindGameObjectsWithTag(tag);
            if(interactables.Length < 1)
                Debug.LogError("No interactables in scene, FUCK!");
            
            foreach (var i in interactables)
            {
                var dest = i.GetComponent<IInteractable>();
                var path = new NavMeshPath();
                agnet.CalculatePath(dest.InteractPosition(_agent.transform.position), path);

                Debug.Log(i.name + path.status);
                if (path.status != NavMeshPathStatus.PathComplete) continue;

                _intaractableGoal = dest;
                break;
            }

            if(_intaractableGoal == null)
                _state = State.Done;
        }

        public InteractWithNearestState(NavMeshAgent agent, GameObject goal)
        {
            _agent = agent;
            _intaractableGoal = goal.GetComponent<IInteractable>();
        }

        public float WaitingTime { get; set; }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _agent.SetDestination(_intaractableGoal.InteractPosition(_agent.transform.position));
                    _state = State.GoToIntactable;
                    return;
                case State.GoToIntactable:
                    if (_agent.HasReachedTarget())
                        _state = Vector3.Distance(_intaractableGoal.InteractPosition(_agent.transform.position), _agent.transform.position) < 2 ?
                            State.Interact : State.Done;
                    return;
                case State.Interact:
                    var puh = _agent.gameObject.GetComponent<PickupHandler>();
                    var returnItem = _intaractableGoal.Interact(puh.CurrentPickup);
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