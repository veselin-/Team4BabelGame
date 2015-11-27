using System;
using System.Collections;
using System.Linq;
using Assets.Core.Configuration;
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
        private Vector2 _interactGoalPosition;
        private CharacterAnimMovement cam;

        private GameObject _interactGameObject;
        
        public InteractWithNearestState(NavMeshAgent agent, string tag, GameObject pickup)
        {
            _agent = agent;

            cam = _agent.gameObject.GetComponent<CharacterAnimMovement>();

            _interactGameObject = pickup;

            var interactables = GameObject.FindGameObjectsWithTag(tag);
            if (interactables.Length < 1)
            {
                _state = State.Done;
                return;
            }

            foreach (var i in interactables)
            {
                _interactGameObject = i;
                var dest = i.GetComponent<IInteractable>();
                if(!dest.CanThisBeInteractedWith(pickup)) continue;
                var path = new NavMeshPath();
                agent.CalculatePath(dest.InteractPosition(_agent.transform.position), path);

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
            cam = _agent.gameObject.GetComponent<CharacterAnimMovement>();
            _intaractableGoal = goal.GetComponent<IInteractable>();
            _interactGameObject = goal;
        }

        public float WaitingTime { get; set; }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    var pos = _intaractableGoal.InteractPosition(_agent.transform.position);
                    _interactGoalPosition = new Vector2(pos.x, pos.z);
                    _agent.SetDestination(pos);
                    _state = State.GoToIntactable;
                    return;
                case State.GoToIntactable:
                    if (_agent.HasReachedTarget())
                    {
                        _agent.ResetPath();

                        if (_agent.enabled)
                        {
                            cam.StartAdjustPosition(_interactGameObject);
                        }

                        _agent.ResetPath();

                        if (_agent.updateRotation)
                        {
                            _agent.ResetPath();
                            var t = Vector2.Distance(_interactGoalPosition,
                                new Vector2(_agent.transform.position.x, _agent.transform.position.z));
                            _state = t < .6 ? State.Interact : State.Done;
                        }
                    }
                    return;
                case State.Interact:
                    var puh = _agent.gameObject.GetComponent<PickupHandler>();


                    if (_interactGameObject.tag == Constants.Tags.Lever && _intaractableGoal.CanThisBeInteractedWith(puh.CurrentPickup))
                    {
                        _agent.gameObject.GetComponent<Animator>().SetTrigger("PullLever");
                    }
                    else if (_interactGameObject.tag == Constants.Tags.Brazier && _intaractableGoal.CanThisBeInteractedWith(puh.CurrentPickup))
                    {
                        _agent.gameObject.GetComponent<Animator>().SetTrigger("LightFire");
                    }
                    else if(_interactGameObject.tag == Constants.Tags.Keyhole)
                    {
                        if (_intaractableGoal.CanThisBeInteractedWith(puh.CurrentPickup))
                        {
                            _agent.gameObject.GetComponent<Animator>().SetTrigger("OpenDoor");
                            
                        }
                        else
                        {
                            _agent.gameObject.GetComponent<Animator>().SetTrigger("DoorLocked");
                            
                        }
                    }
                    else if (_interactGameObject.tag == Constants.Tags.Key ||
                             _interactGameObject.tag == Constants.Tags.Stick)
                    {
                        _agent.gameObject.GetComponent<Animator>().SetTrigger("PickUp");
                    }
                    else
                    {
                        _agent.gameObject.GetComponent<Animator>().SetTrigger("DoorLocked");
                    }

                    var returnItem = _intaractableGoal.Interact(puh.CurrentPickup);
                    puh.PickUpItem(returnItem);
                    _agent.ResetPath();

                    _waitUntill = Time.time + WaitingTime;
                    _state = State.WaitSomeTime;
                    return;
                case State.WaitSomeTime:
                    _agent.ResetPath();
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