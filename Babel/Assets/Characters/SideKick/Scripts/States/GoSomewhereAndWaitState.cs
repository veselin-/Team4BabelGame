using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts.States
{
    public class GoSomewhereAndWaitState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly Vector3 _goalPosition;
        private readonly float _waitingTime;

        private float _waitUntill;
        private State _state;

        public GoSomewhereAndWaitState(NavMeshAgent navAgent, Vector3 position, float waitingTime)
        {
            _agent = navAgent;
            _goalPosition = position;
            _waitingTime = waitingTime;
        }

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
                        _waitUntill = Time.time + _waitingTime;
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
