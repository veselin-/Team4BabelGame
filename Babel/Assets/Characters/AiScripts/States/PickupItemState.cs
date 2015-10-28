using System;
using System.Linq;
using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class PickupItemState : IState
    {

        private State _state;
        private readonly NavMeshAgent _agent;
        private readonly GameObject _pickupGoal;

        public float WaitingTime { get; set; }

        public PickupItemState(NavMeshAgent agent, string tag)
        {
            _agent = agent;
            agent.gameObject.GetComponent<PickupHandler>();
            var pickups = GameObject.FindGameObjectsWithTag(tag);
            _pickupGoal = pickups.OrderBy(i =>
                Vector3.Distance(_agent.transform.position, i.transform.position)).FirstOrDefault();
        }

        public PickupItemState(NavMeshAgent agent, GameObject item)
        {
            _agent = agent;
            _pickupGoal = item;
        }
        
        public void ExecuteState()
        {
            if(_pickupGoal == null || !_pickupGoal.activeSelf)
                _state = State.Done;

            switch (_state)
            {
                case State.Neutral:
                    _agent.destination = _pickupGoal.transform.position;
                    _state = State.GoToPickup;
                    return;
                case State.GoToPickup:
                    if(_agent.HasReachedTarget())
                        _state = State.Pickup;
                    return;
                case State.Pickup:
                    if(_pickupGoal.GetComponent<ICollectable>() == null)
                        throw new Exception("You cannot make the AI pick up an item, without it having a ICollectable script attached!");
                    var pickup = _pickupGoal.GetComponent<ICollectable>().PickUp();
                    _agent.gameObject.GetComponent<PickupHandler>().PickUpItem(pickup);
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
            Neutral, GoToPickup, Pickup, Done
        }
    }
}
