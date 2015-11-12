using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using Assets.Core.LevelMaster;
using UnityEngine;

namespace Assets.Environment.Scripts
{
    public class DoubleDoorOpen : MonoBehaviour
    {

        public GameObject[] Interactables;
        public float MovementSpeed;

        private List<IInteractable> _interactables;

        //private Vector3 _startPosition;
        //private Vector3 _endPosistion;
        private Vector3 _startRotation;
        private Vector3 _endRotation;
        private Vector3 _startRotation2;
        private Vector3 _endRotation2;
        private bool _objectIsShown;

        //public GameObject obstacle;
        //public int moveValue = 0;
        public float degreeValue = 120;
        public GameObject door, door2;
        // Use this for initialization
        void Start()
        {
            // Sliding positions
            //_startPosition = transform.localPosition;
            //_endPosistion = new Vector3(_startPosition.x + transform.localScale.x + moveValue, _startPosition.y, _startPosition.z);

            // Rotate positions
            _startRotation = door.transform.eulerAngles;
            _endRotation = new Vector3(_startRotation.x, _startRotation.y + degreeValue, _startRotation.z);

            _startRotation2 = door2.transform.eulerAngles;
            _endRotation2 = new Vector3(_startRotation2.x, -degreeValue, _startRotation2.z);
            // Get all IInteractables
            _interactables = new List<IInteractable>();
            foreach (var interactable in Interactables)
                _interactables.Add(interactable.GetComponent<IInteractable>());

            StartCoroutine(CheckForInputs());
        }

        void Update()
        {
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, _objectIsShown ? _endPosistion : _startPosition, Time.deltaTime * MovementSpeed);
            door.transform.eulerAngles = Vector3.MoveTowards(door.transform.eulerAngles, _objectIsShown ? _endRotation : _startRotation, Time.deltaTime * MovementSpeed);
            if(door2.transform.localEulerAngles.y >= 240)
            {
                door2.transform.eulerAngles = Vector3.MoveTowards(door2.transform.eulerAngles, _objectIsShown ? _endRotation2 : _startRotation2, Time.deltaTime * MovementSpeed);
            }
        }

        IEnumerator CheckForInputs()
        {
            while (true)
            {
                if (_interactables.All(i => i.HasBeenActivated()))
                {
					if(!_objectIsShown){
						GetComponent<AudioSource>().Play();
					}
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
