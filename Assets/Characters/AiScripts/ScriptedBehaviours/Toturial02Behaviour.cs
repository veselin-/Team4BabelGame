using System;
using System.Collections.Generic;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.AiScripts.ScriptedBehaviours
{
    public class Toturial02Behaviour : MonoBehaviour
    {
        private State _state;        private bool _waitingForPlayer;        private float _movementSpeed;        private NavMeshAgent _agent;

        private Transform waypoint01;
        private Transform waypoint02;

        private InteractableSpeechBubble bubble;

        // Use this for initialization
        void Start ()
        {
            var colider = gameObject.AddComponent<SphereCollider>();
            colider.radius = 5;            colider.isTrigger = true;            _agent = GetComponent<NavMeshAgent>();            GetComponent<SidekickControls>().enabled = false;
            var aiMovement = GetComponent<AiMovement>();
            aiMovement.StrollSpeed = 0;            _movementSpeed = aiMovement.MovementSpeed;
            _agent.destination = _agent.transform.position;
            _waitingForPlayer = true;

            var waypoints = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).transform.FindChild("Waypoints");
            waypoint01 = waypoints.FindChild("waypoint");
            waypoint02 = waypoints.FindChild("waypoint (1)");

            bubble = FindObjectOfType<InteractableSpeechBubble>();        }

        // Update is called once per frame
        void Update () {
            _agent.speed = _movementSpeed;
                        if (_state == State.First && Vector3.Distance(_agent.transform.position, waypoint01.position) < 2)            {
                // TODO: waving animation
                bubble.ActivateSidekickSignBubble(new List<int>(new[] { 8 }));                _waitingForPlayer = true;
                _state = State.Second;            } else if (_state == State.Second && Vector3.Distance(_agent.transform.position, waypoint02.position) < 2)            {
                // TODO: waving animation                bubble.ActivateSidekickSignBubble(new List<int>(new[] { 8 }));                _state = State.Thrid;                _waitingForPlayer = true;            }        }
                void OnTriggerStay(Collider other)        {
            if(other.tag != Constants.Tags.Player || !_waitingForPlayer) return;            _waitingForPlayer = false;            switch (_state)            {
                case State.First:
                    // TODO: waving animation
                    CreateNewSymbol.SymbolID = 8;
                    GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UIControl>().SignCreationEnter();
                    _agent.destination = waypoint01.position;                    _agent.speed = _movementSpeed;
                    break;
                case State.Second:
                    _agent.destination = waypoint02.position;                    _agent.speed = _movementSpeed;
                    break;
            }
        }
                enum State        {
            First, Second, Thrid        }
    }
}

