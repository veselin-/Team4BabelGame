using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.UI.Scripts
{
    public class AiControls : MonoBehaviour
    {

        private GameObject _sideKick;
        private GameObject _player;

        public void GoHereAndStay()
        {
            var state = new GoSomewhereAndWaitState(_sideKick.GetComponent<NavMeshAgent>(), _player.transform.position);
            _sideKick.GetComponent<AiMovement>().AssignNewState(state);
        }

        public void GoToNearestLever()
        {
            var state = new InteractWithNearestState(_sideKick.GetComponent<NavMeshAgent>(), Constants.Tags.Lever);
            _sideKick.GetComponent<AiMovement>().AssignNewState(state);
        }

        public void FollowMe()
        {
            var state = new FollowThisState(_sideKick.GetComponent<NavMeshAgent>(), _player.gameObject);
            _sideKick.GetComponent<AiMovement>().AssignNewState(state);
        }

        public void PickupNearstStick()
        {
            var state = new PickupItemState(_sideKick.GetComponent<NavMeshAgent>(), Constants.Tags.Stick);
            _sideKick.GetComponent<AiMovement>().AssignNewState(state);
        }

        // Use this for initialization
        void Start ()
        {
            _sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
            _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
