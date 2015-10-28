using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts
{
    public class SidekickControls : MonoBehaviour
    {
        #region Instatiate
        
        private GameObject _sideKick;
        private NavMeshAgent _sideKickAgent;
        private AiMovement _sideKickMovement;
        private GameObject _player;

        // Use this for initialization
        void Start()
        {
            _sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
            _sideKickAgent = _sideKick.GetComponent<NavMeshAgent>();
            _sideKickMovement = _sideKick.GetComponent<AiMovement>();
            _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
        }

        #endregion

        public void GoHereAndStay()
        {
            var state = new GoSomewhereAndWaitState(_sideKickAgent, _player.transform.position);
            _sideKickMovement.AssignNewState(state);
        }

        public void GoToNearestLever()
        {
            var state = new InteractWithNearestState(_sideKickAgent, Constants.Tags.Lever);
            _sideKickMovement.AssignNewState(state);
        }

        public void FollowMe()
        {
            var state = new FollowThisState(_sideKickAgent, _player.gameObject);
            _sideKickMovement.AssignNewState(state);
        }

        public void PickupNearstStick()
        {
            var state = new PickupItemState(_sideKickAgent, Constants.Tags.Stick);
            _sideKickMovement.AssignNewState(state);
        }

        public void GoIntoTradeMode()
        {
            var state = new TradeState(_sideKickAgent);
            _sideKickMovement.AssignNewState(state);
        }
    }
}
