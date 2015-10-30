﻿using System.Linq;
using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;
using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class InteractWithNearestState : IState
    {
        private readonly NavMeshAgent _agent;
        private readonly GameObject _intaractableGoal;

        private float _waitUntill;
        private State _state;


        public InteractWithNearestState(NavMeshAgent agnet, string tag)
        {
            _agent = agnet;

            var interactables = GameObject.FindGameObjectsWithTag(tag);
            if(interactables.Length < 1)
                Debug.LogError("No interactables in scene, FUCK!");

            _intaractableGoal = interactables.OrderBy(i => 
                Vector3.Distance(_agent.transform.position, i.transform.position)).FirstOrDefault();
        }

        public InteractWithNearestState(NavMeshAgent agnet, GameObject goal)
        {
            _agent = agnet;
            _intaractableGoal = goal;
        }

        public float WaitingTime { get; set; }

        public void ExecuteState()
        {
            switch (_state)
            {
                case State.Neutral:
                    _agent.SetDestination(_intaractableGoal.transform.position);
                    _state = State.GoToLever;
                    return;
                case State.GoToLever:
                    if (_agent.HasReachedTarget())
                        _state = State.PullLever;
                    return;
                case State.PullLever:
                    _intaractableGoal.GetComponent<IInteractable>().Interact();
                    _waitUntill = Time.time + WaitingTime;
                    _state = State.WaitSomeTime;
                    return;
                case State.WaitSomeTime:
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
            Neutral, GoToLever, PullLever, WaitSomeTime, Done
        }
    }
}