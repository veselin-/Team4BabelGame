using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class GoSomewhereAndWaitState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly Vector3 _goalPosition;

        private float _waitUntill;
        private State _state;

        public GoSomewhereAndWaitState(NavMeshAgent navAgent, Vector3 position)
        {
            _agent = navAgent;
            _goalPosition = position;
        }

        public float WaitingTime { get; set; }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _agent.SetDestination(_goalPosition);
                    _state = State.GoSomewhere;
                    return;
                case State.GoSomewhere:
                    if (_agent.HasReachedTarget())
                    {
                        _agent.ResetPath();
                        _waitUntill = Time.time + WaitingTime;
                        _state = State.WaitSomeTime;
                    }
                    return;
                case State.WaitSomeTime:
                    if (Time.time > _waitUntill)
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
            Neutral, GoSomewhere, WaitSomeTime, Done
        }
    }
}
