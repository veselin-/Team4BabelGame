using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Doors.BigDoubleDoor.Scripts
{
    public class DoubleDoorOpen : MonoBehaviour
    {

        public GameObject[] Interactables;
        public float MovementSpeed;

        public bool FixActive {
            set
            {
                var obstacles = GetComponentsInChildren<NavMeshObstacle>();
                foreach (var o in obstacles)
                {
                    o.carving = !value;
                }
            }
        }

        private List<IInteractable> _interactables;

        private bool _objectIsShown;
        private Animator _anim;

        private bool ObjectIsShown {
            get { return _objectIsShown; }
            set
            {
                if (_objectIsShown != value && _anim != null)
                    StartCoroutine(aa(value));
                _objectIsShown = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            _anim = GetComponentInChildren<Animator>(); 

            // Get all IInteractables
            _interactables = new List<IInteractable>();
            foreach (var interactable in Interactables)
                _interactables.Add(interactable.GetComponent<IInteractable>());

            StartCoroutine(CheckForInputs());
        }

        IEnumerator CheckForInputs()
        {
            while (true)
            {
                if (_interactables.All(i => i.HasBeenActivated()))
                {
					if(!ObjectIsShown){
						GetComponent<AudioSource>().Play();
					}
                    ObjectIsShown = true;
                }
                else
                {
                    ObjectIsShown = false;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        IEnumerator aa(bool val)
        {
            yield return new WaitForSeconds(2);
            _anim.SetTrigger(val ? "DoorOpen" : "DoorClose");
        }
    }
}
