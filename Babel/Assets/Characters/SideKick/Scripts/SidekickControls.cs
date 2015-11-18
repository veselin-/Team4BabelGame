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

            var db = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager);
            if (db != null)
                _dbManager = db.GetComponent<DatabaseManager>();
            else
                Debug.Log("No dbmanager in scene!! - Problem?");

        }

        #endregion

        public void ExecuteAction(int i)
        {
            switch (i)
            {
                case 0:
                    _sideKickMovement.AssignNewState(new GoSomewhereAndWaitState(_sideKickAgent,
                        _player.transform.position));
                    return;
                case 1:
                    _sideKickMovement.AssignNewState(new WaitState(_sideKickAgent));
                    return;
                case 2:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Lever,
                        _sidekickPickupHandler.CurrentPickup));
                    return;
                case 3:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Stick));
                    return;
                case 4:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Brazier,
                        _sidekickPickupHandler.CurrentPickup));
                    return;
                case 5:
                    _sideKickMovement.AssignNewState(new TradeState(_sideKickAgent, _player.GetComponent<NavMeshAgent>()));
                    return;
                case 6:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Bucket));
                    return;
                case 7:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Pool,
                        _sidekickPickupHandler.CurrentPickup));
                    return;
                case 8:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Font,
                        _sidekickPickupHandler.CurrentPickup));
                    return;
                case 9:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Key));
                    return;
            }
        }
    }
}
