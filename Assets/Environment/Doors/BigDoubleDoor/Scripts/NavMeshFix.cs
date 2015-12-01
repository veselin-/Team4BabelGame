using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Environment.Doors.BigDoubleDoor.Scripts
{
    public class NavMeshFix : MonoBehaviour
    {

        public GameObject WallDoor;
        private DoubleDoorOpen _openScript;

        private bool _isPlayerHere;
        private bool _isSidekickHere;

        private void Start()
        {
            if (WallDoor != null)
                _openScript = WallDoor.GetComponent<DoubleDoorOpen>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Player)
                _isPlayerHere = true;

            if (other.tag == Constants.Tags.SideKick)
                _isSidekickHere = true;

            if (_openScript != null)
                _openScript.FixActive = _isPlayerHere || _isSidekickHere;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == Constants.Tags.Player)
                _isPlayerHere = false;

            if (other.tag == Constants.Tags.SideKick)
                _isSidekickHere = false;

            if (_openScript != null)
                _openScript.FixActive = _isPlayerHere || _isSidekickHere;
        }
    }
}
