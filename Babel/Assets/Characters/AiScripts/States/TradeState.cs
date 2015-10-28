using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class TradeState : IState {

        public float WaitingTime { get; set; }

        private PickupHandler _pickupHandler;
        private State _state;
        private float _waitUntil;
        private NavMeshAgent _agent;

        public TradeState(NavMeshAgent agent)
        {
            _pickupHandler = agent.gameObject.GetComponent<AiMovement>().FindPickUpHandeder();
            _agent = agent;
        }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _waitUntil = Time.time + WaitingTime;
                    _state = State.Waiting;
                    _pickupHandler.InTradingMode = true;
                    _agent.destination = _agent.transform.position;
                    return;
                case State.Waiting:
                    if (Time.time > _waitUntil)
                    {
                        _state = State.Done;
                        _pickupHandler.InTradingMode = false;
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
