using System.Collections;
using Assets.Characters.SideKick.Scripts.States;
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

        // Use this for initialization
        void Start ()
        {
            _agent = GetComponent<NavMeshAgent>();
            StartCoroutine(StateExecuter());           
        }

        private void Update()
        {
        }

        IEnumerator StateExecuter()
        {
            var explorerState = new ExploreState(_agent, StrollSpeed);
            _currentState = explorerState;

            while (true)
            {
                _agent.speed = MovementSpeed;

                // Default state
                if (_currentState == null || _currentState.IsDoneExecuting())
                    _currentState = explorerState;

                _currentState.ExecuteState();
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void AssignNewState(IState state)
        {
            _currentState = state;
        }
    }
}
