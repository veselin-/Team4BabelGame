using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using Assets.Core.LevelMaster;
using UnityEngine;

namespace Assets.Environment.Stairs.Scripts
{
    public class StairMovement : MonoBehaviour
    {

        public GameObject[] Interactables;
        public float MovementSpeed;

        private List<IInteractable> _interactables;

        private Vector3 _startPosition;
        private Vector3 _endPosistion;
        private bool _stairIsShown;

        public GameObject obstacle;

        // Use this for initialization
        void Start ()
        {
            // Sliding positions
            _startPosition = transform.localPosition;
            _endPosistion = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z + transform.localScale.z + 2);

            // Get all IInteractables
            _interactables = new List<IInteractable>();
            foreach (var interactable in Interactables)
                _interactables.Add(interactable.GetComponent<IInteractable>());
            
            StartCoroutine(CheckForInputs());
        }

        void Update()
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _stairIsShown ? _endPosistion : _startPosition, Time.deltaTime * MovementSpeed);
        }

        IEnumerator CheckForInputs()
        {
            while (true)
            {
                if (_interactables.All(i => i.HasBeenActivated()))
                {
                    _stairIsShown = true;
                    obstacle.SetActive(false);
                    //GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).GetComponent<RoomManager>().SetCurrentRoom(1);
                }
                else
                {
                    _stairIsShown = false;
                    obstacle.SetActive(true);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
