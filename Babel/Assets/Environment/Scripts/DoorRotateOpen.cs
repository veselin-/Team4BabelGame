using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using Assets.Core.LevelMaster;
using UnityEngine;

namespace Assets.Environment.Scripts
{
    public class DoorRotateOpen : MonoBehaviour
    {

        public GameObject[] Interactables;
        public float MovementSpeed;

        private List<IInteractable> _interactables;

        private Vector3 _startPosition;
        private Vector3 _endPosistion;
        private Vector3 _startRotation;
        private Vector3 _endRotation;
        private bool _objectIsShown;

        //public GameObject obstacle;
        //public int moveValue = 0;
        private float degreeValue = 89;

        // Use this for initialization
        void Start()
        {
            // Sliding positions
            //_startPosition = transform.localPosition;
            //_endPosistion = new Vector3(_startPosition.x + transform.localScale.x + moveValue, _startPosition.y, _startPosition.z);

            // Rotate positions
            _startRotation = transform.eulerAngles;
            _endRotation = new Vector3(_startRotation.x, _startRotation.y + degreeValue, _startRotation.z);

            // Get all IInteractables
            _interactables = new List<IInteractable>();
            foreach (var interactable in Interactables)
                _interactables.Add(interactable.GetComponent<IInteractable>());

            StartCoroutine(CheckForInputs());
        }

        void Update()
        {
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, _objectIsShown ? _endPosistion : _startPosition, Time.deltaTime * MovementSpeed);
            transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, _objectIsShown ? _endRotation : _startRotation, Time.deltaTime * MovementSpeed);
        }

        IEnumerator CheckForInputs()
        {
            while (true)
            {
                if (_interactables.All(i => i.HasBeenActivated()))
                {
                    _objectIsShown = true;
                    //obstacle.SetActive(false);
                    //GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).GetComponent<RoomManager>().SetCurrentRoom(1);
                }
                else
                {
                    _objectIsShown = false;
                    //obstacle.SetActive(true);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
