using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class WaitState : IState
    {
        private State _state;
        private readonly NavMeshAgent _agent;
        private float _waitUntil;

        public WaitState(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public float WaitingTime { get; set; }
        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _waitUntil = Time.time + WaitingTime;
                    _state = State.Waiting;
                    _agent.ResetPath();
                    return;
                case State.Waiting:
                    if (Time.time > _waitUntil)
                        _state = State.Done;
                    return;
                default: return;
            }

        }

        public bool IsDoneExecuting()
        {
            return _state == State.Done;
        }

        enum State { Neutral, Waiting, Done }
    }
}
