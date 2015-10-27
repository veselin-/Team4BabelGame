using System.Collections;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public IState CurrentState;

        // Use this for initialization
        void Start ()
        {
            StartCoroutine(StateExecuter());
        }

        IEnumerator StateExecuter()
        {
            var explorerState = new ExploreState(GetComponent<NavMeshAgent>());
            CurrentState = explorerState;

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (CurrentState == null || CurrentState.IsDoneExecuting())
                    CurrentState = explorerState;
                
                CurrentState.ExecuteState();
            }
        }

        public void AssignNewState(IState state)
        {
            CurrentState = state;
        }
    }
}
