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
        private int _state;

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
                case 0:
                    _agent.SetDestination(_goalPosition);
                    _state = 1;
                    return;
                case 1:
                    if (_agent.HasReachedTarget())
                    {
                        _waitUntill = Time.time + _waitingTime;
                        _state = 2;
                    }
                    return;
                case 2:
                    if (Time.time > _waitUntill)
                        _state = 3;
                    return;
                default:
                    return;
            }                
        }

        public bool IsDoneExecuting()
        {
            return _state == 3;
        }
    }
}
