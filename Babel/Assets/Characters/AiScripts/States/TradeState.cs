using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class TradeState : IState {

        public float WaitingTime { get; set; }

        private PickupHandler _pickupHandler;
        private State _state;
        private float _waitUntil;
        private readonly NavMeshAgent _agent;
        private readonly NavMeshAgent _otherAgent;

        public TradeState(NavMeshAgent agent, NavMeshAgent otherAgent)
        {
            _pickupHandler = agent.gameObject.GetComponent<AiMovement>().FindPickUpHandeder();
            _agent = agent;
            _otherAgent = otherAgent;
        }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _waitUntil = Time.time + WaitingTime;
                    _state = State.Waiting;
                    _agent.destination = _agent.transform.position;
                    return;
                case State.Waiting:

                    if (Vector3.Distance(_agent.transform.position, _otherAgent.transform.position) < 2)
                    {
                        RaycastHit hit;
                        Physics.Raycast(_agent.transform.position, (_otherAgent.transform.position - _agent.transform.position), out hit, 2);
                        bool hitPlayer = hit.transform.tag == Constants.Tags.Player;
                        if (hitPlayer)
                        {
                            _agent.gameObject.GetComponent<PickupHandler>().InitiateTrade(_otherAgent.gameObject.GetComponent<PickupHandler>());
                        }
                        _state = State.Done;
                    }
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

        enum State { Neutral, Waiting, Done}
    }
}
