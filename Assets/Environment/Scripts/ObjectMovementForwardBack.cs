using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using Assets.Core.LevelMaster;
using UnityEngine;

namespace Assets.Environment.Scripts
{
    public class ObjectMovementForwardBack : MonoBehaviour
    {

        public GameObject[] Interactables;
    //    public float MovementSpeed;

        private List<IInteractable> _interactables;

     //   private Vector3 _startPosition;
    //    private Vector3 _endPosistion;
        private bool _objectIsShown;

        public GameObject obstacle;
   //     public float moveValue;

        private Animator anim;

        private bool someoneOnStairs = false;

        // Use this for initialization
        void Start()
        {
            // Sliding positions
         //   _startPosition = transform.localPosition;
         //   _endPosistion = new Vector3(_startPosition.x + moveValue, _startPosition.y, _startPosition.z);
            anim = GetComponent<Animator>();
            // Get all IInteractables
            _interactables = new List<IInteractable>();
            foreach (var interactable in Interactables)
                _interactables.Add(interactable.GetComponent<IInteractable>());

            StartCoroutine(CheckForInputs());
        }

        void Update()
        {


            //if (!someoneOnStairs)
            //{

            //    transform.localPosition = Vector3.MoveTowards(transform.localPosition,
            //        _objectIsShown ? _endPosistion : _startPosition, Time.deltaTime*MovementSpeed);
            //}
            //else
            //{
            //    obstacle.SetActive(false);
            //}

        }

        IEnumerator CheckForInputs()
        {
            while (true)
            {
                if (_interactables.All(i => i.HasBeenActivated()))
                {
					if(!_objectIsShown){
						StairsMovingOut();
                        _objectIsShown = true;
                    }
                    
                }
                else
                {
					if(_objectIsShown && !someoneOnStairs)
                    {
                        StairsMovingIn();
                        _objectIsShown = false;
                    }
                    
                
                    }
                yield return new WaitForSeconds(0.2f);
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == Constants.Tags.Player || coll.tag == Constants.Tags.SideKick)
            {
                someoneOnStairs = true;
                Debug.Log(someoneOnStairs);
            }
        }
        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == Constants.Tags.Player || coll.tag == Constants.Tags.SideKick)
            {
                someoneOnStairs = false;
                Debug.Log(someoneOnStairs);
            }
        }

        void StairsMovingOut()
        {
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();

            obstacle.SetActive(false);

            anim.SetTrigger("ActivateStairs");
        }

        void StairsMovingIn()
        {
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();

            obstacle.SetActive(true);
            anim.SetTrigger("ActivateStairs");
        }

    }
}
