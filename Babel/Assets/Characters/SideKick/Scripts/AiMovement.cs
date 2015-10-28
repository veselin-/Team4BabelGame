using System;
using System.Collections;
using Assets.Characters.SideKick.Scripts.States;
using Assets.Core.Configuration;
using Assets.Core.LevelMaster;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.Characters.SideKick.Scripts
{
    public class AiMovement : MonoBehaviour
    {
        #region Public Fields
        [Header("Movent speeds")]
        [Range(0, 10)]
        public float MovementSpeed;
        [Range(0, 10)]
        public float StrollSpeed;
        #endregion
        
        private IState _currentState;
        private NavMeshAgent _agent;
        private RoomManager _rm;
        private ExploreState _exploreState;

        // Use this for initialization
        void Start ()
        {
            _agent = GetComponent<NavMeshAgent>();
            
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
                if (_currentState == null || _currentState.IsDoneExecuting())
                    _currentState = _exploreState;

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
    }
}
