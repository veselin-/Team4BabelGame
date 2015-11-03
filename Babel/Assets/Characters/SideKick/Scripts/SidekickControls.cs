using System;
using System.Collections.Generic;
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
        private PickupHandler _sidekickPickupHandler;
        private GameObject _player;
        private DatabaseManager _dbManager;

        // Use this for initialization
        void Start()
        {
            _sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
            _sideKickAgent = _sideKick.GetComponent<NavMeshAgent>();
            _sideKickMovement = _sideKick.GetComponent<AiMovement>();
            _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            _sidekickPickupHandler = _sideKick.GetComponent<PickupHandler>();
            _dbManager =
                GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
            //_dbManager.LoadData();
        }

        #endregion

        public void RespondToSentence(List<int> signs)
        {
            var id = _dbManager.GetSentenceBySeq(signs);
            if (id > 0)
                ExecuteAction(id);
        }

        public void ExecuteAction(int i)
        {
            switch (i)
            {
                case 1:
                case 4:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Brazier, _sidekickPickupHandler.CurrentPickup));
                    return;
                case 6:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Lever, _sidekickPickupHandler.CurrentPickup));
                    return;
                case 9:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Stick));
                    return;
                case 10:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Key));
                    return;
                case 11:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Torch));
                    return;
                case 12:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Bottle));
                    return;
                case 13:
                    _sidekickPickupHandler.DropCurrent();
                    return;
                case 17:
                    _sideKickMovement.AssignNewState(new GoSomewhereAndWaitState(_sideKickAgent,
                        _player.transform.position));
                    return;
                case 18:
                    _sideKickMovement.AssignNewState(new FollowThisState(_sideKickAgent, _player.gameObject));
                    return;
                case 19:
                    _sideKickMovement.AssignNewState(new WaitState(_sideKickAgent));
                    return;
                case 20:
                    _sideKickMovement.AssignNewState(new TradeState(_sideKickAgent, _player.GetComponent<NavMeshAgent>()));
                    return;
                case 21:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Keyhole, _sidekickPickupHandler.CurrentPickup));
                    return;
                default:
                    throw new Exception("That action is not implemented yet!");
            }   
            
        }
    }
}
