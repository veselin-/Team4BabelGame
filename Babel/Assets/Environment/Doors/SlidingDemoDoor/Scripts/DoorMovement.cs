using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using Assets.Core.LevelMaster;
using UnityEngine;

namespace Assets.Environment.Doors.SlidingDemoDoor.Scripts
{
    public class DoorMovement : MonoBehaviour
    {

        public GameObject[] Interactables;
        public float MovementSpeed;

        private List<IInteractable> _interactables;

        private Vector3 _startPosition;
        private Vector3 _endPosistion;
        private bool _doorIsOpen;
    

        // Use this for initialization
        void Start ()
        {
            // Sliding positions
            _startPosition = transform.localPosition;
            _endPosistion = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z - transform.localScale.z);

            // Get all IInteractables
            _interactables = new List<IInteractable>();
            foreach (var interactable in Interactables)
                _interactables.Add(interactable.GetComponent<IInteractable>());
            
            StartCoroutine(CheckForInputs());
        }

        void Update()
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _doorIsOpen ? _endPosistion : _startPosition, Time.deltaTime * MovementSpeed);
        }

        IEnumerator CheckForInputs()
        {
            while (!_doorIsOpen)
            {
                if (_interactables.All(i => i.HasBeenActivated()))
                {
                    _doorIsOpen = true;
                    GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).GetComponent<RoomManager>().SetCurrentRoom(1);
                }   
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
