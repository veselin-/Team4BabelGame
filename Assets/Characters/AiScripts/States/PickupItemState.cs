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
        private float _waitUntil;
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


			if (_pickupGoal == null || _pickupGoal.GetComponent<ICollectable>() == null) {
				// sound
				if(_agent.gameObject.tag == "SideKick")
				{
					//Debug.Log("Nothing to pick up"); // doesnt work
					GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().FemaleNoSoundPlay();
				}

				_state = State.Done;
			}
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
                        _state = State.PickupAnim;
                    return;
                case State.PickupAnim: //1.1
                    if(_pickupGoal.GetComponent<ICollectable>() == null)
                        throw new Exception("You cannot make the AI pick up an item, without it having a ICollectable script attached!");
                    if(Vector3.Distance(_pickupGoal.transform.position, _agent.transform.position) < 2)
                        _agent.ResetPath();
                    if (Vector3.Distance(_pickupGoal.transform.position, _agent.transform.position) < 2 && _agent.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StandingStill"))
                    {
                        _agent.gameObject.GetComponent<Animator>().SetTrigger("PickUp");
                        _state = State.PickupAction;
                        _waitUntil = Time.time + 1.2f;
                    }
                    return;
                    case State.PickupAction:
                        if (_pickupGoal.GetComponent<ICollectable>() == null)
                            throw new Exception("You cannot make the AI pick up an item, without it having a ICollectable script attached!");
                        if (_waitUntil < Time.time)
                        {
                            var pickup = _pickupGoal.GetComponent<ICollectable>().PickUp();
                            _agent.gameObject.GetComponent<AiMovement>().FindPickUpHandeder().PickUpItem(pickup);
                            _waitUntil = Time.time + WaitingTime;
                            _agent.ResetPath();
                            _state = State.Wait;
                        }
                        return;
                case State.Wait:
                    if (Time.time > _waitUntil)
                    {
                        _state = State.Done;
                    }
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
            Neutral, GoToPickup, PickupAnim, PickupAction, Wait, Done
        }
    }
}
