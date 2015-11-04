using System;
using System.Collections;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using Assets.Core.LevelMaster;
using UnityEngine;

namespace Assets.Characters.AiScripts
{
    public class AiMovement : MonoBehaviour
    {
        #region Public Fields
        [Header("Movent")]
        [Range(0, 10)]
        public float MovementSpeed;
        [Range(0, 10)]
        public float StrollSpeed;

        public float TimeBeforeStolling;
        #endregion

        public float Happines { get; set; }

        private IState _currentState;
        private NavMeshAgent _agent;
        private RoomManager _rm;
        private ExploreState _exploreState;

        // Use this for initialization
        void Start ()
        {
            _agent = GetComponent<NavMeshAgent>();
            Happines = 1;

            // Room manager 
            var gm = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster);
            if(gm == null || gm.GetComponent<RoomManager>() == null)
                throw new Exception("We need a RoomManager in the scene, for the AI to work..!");
            _rm = gm.GetComponent<RoomManager>();
            _rm.Ai = this;

            // Default state
            _exploreState = new ExploreState(_agent, StrollSpeed) {Waypoints = _rm.GetCurrnetWaypoints()};

            StartCoroutine(StateExecuter());           
        }
        
        IEnumerator StateExecuter()
        {
            while (true)
            {
                _agent.speed = MovementSpeed;

                // Default state
                if (_currentState == null || _currentState.IsDoneExecuting() || Happines < 0.1f)
                    _currentState = _exploreState;

                _currentState.WaitingTime = TimeBeforeStolling * Happines;
                _currentState.ExecuteState();
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void AssignNewState(IState state)
        {
            _currentState = state;
        }

        public void RoomChanged()
        {
            _exploreState.Waypoints = _rm.GetCurrnetWaypoints();
        }

        public PickupHandler FindPickUpHandeder()
        {
            Transform root = transform;
            while (root.parent != null)
                root = root.parent;
            return root.GetComponentInChildren<PickupHandler>();
        }
    }
}
