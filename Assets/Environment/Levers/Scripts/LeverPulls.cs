using System.Collections;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Levers.LeverExample.Scripts
{
    public class LeverPulls : MonoBehaviour, IInteractable
    {
        private Color _oldColor;
        private bool _hasBeenPulled;
        public bool Timelimit = false;
        public float SecForLever = 5;
        public GameObject PlayerPos;
        public static int leverpulls = 0;

        public Vector3 InteractPosition(Vector3 ai)
        {
            return PlayerPos.transform.position;
        }
        
        public bool HasBeenActivated()
        {
            return _hasBeenPulled;
        }

        public GameObject Interact(GameObject pickup)
        {
            if(CanThisBeInteractedWith(pickup))
                StartCoroutine(ChangeColor());
            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return !_hasBeenPulled;
        }

        IEnumerator ChangeColor()
        {
            
            yield return new WaitForSeconds(1f);
            
            GetComponentInChildren<Animator>().SetTrigger("PullLever");

            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2f);
            _hasBeenPulled = true;
            if(leverpulls < 7)
            {
                leverpulls += 1;
            }
            if (Timelimit)
            {
                yield return new WaitForSeconds(SecForLever);
                GetComponentInChildren<Animator>().SetTrigger("PullLever");
                _hasBeenPulled = false;
            }
        }
    }
}
