using System.Collections;
using Assets.Core.Configuration;
using Assets.Core.GameMaster.Scripts;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class EndGameState : IState
    {

        private State _state;
        private readonly NavMeshAgent _agent;
        private readonly GameObject _destination;
        
        public float WaitingTime { get; set; }

        public EndGameState(NavMeshAgent agent)
        {
            _agent = agent;

            var endPoints = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).
                GetComponent<EndPoints>().Endpoints;

            if (endPoints.Length < 1)
                Debug.LogError("No interactables in scene, FUCK!");

            foreach (var i in endPoints)
            {
                var path = new NavMeshPath();
                agent.CalculatePath(i.transform.position, path);

                if (path.status != NavMeshPathStatus.PathComplete) continue;

                _destination = i;
                break;
            }

            if (_destination == null)
                _state = State.Done;
        }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.GoToPoint:
                    _agent.SetDestination(_destination.transform.position);
                    _state = State.Wait;
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
            GoToPoint, Wait, Done
        }
    }
}
