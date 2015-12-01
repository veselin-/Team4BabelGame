using System.Collections;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Levers.Scripts
{
    public class LeverPullOnlyAya : MonoBehaviour, IInteractable
    {
        private bool _hasBeenPulled;
        private int timesPulled;

        public Transform InteractPos;


        // Use this for initialization
        void Start () {

        }

        public bool HasBeenActivated()
        {
            return _hasBeenPulled;
        }

        public GameObject Interact(GameObject pickup)
        {
            StartCoroutine(DelayedStuff());
            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return !_hasBeenPulled;
        }

        public Vector3 InteractPosition(Vector3 ai)
        {
            return InteractPos.position;
        }

        private IEnumerator DelayedStuff()
        {
            yield return new WaitForSeconds(1f);

            GetComponentInChildren<Animator>().SetTrigger("PullLever");

            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2f);
            _hasBeenPulled = true;

            if (timesPulled++ == 0)
            {
                yield return new WaitForSeconds(6);
                GetComponentInChildren<Animator>().SetTrigger("PullLever");
                _hasBeenPulled = false;
            }
        }
    }
}
