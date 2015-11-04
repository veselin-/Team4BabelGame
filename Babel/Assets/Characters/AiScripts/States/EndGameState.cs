using UnityEngine;

namespace Assets.Characters.AiScripts.States
{
    public class EndGameState : MonoBehaviour, IState {

        public float WaitingTime { get; set; }
        public void ExecuteState()
        {
            throw new System.NotImplementedException();
        }

        public bool IsDoneExecuting()
        {
            throw new System.NotImplementedException();
        }
    }
}
