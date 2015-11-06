using System.Linq;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Font.Scripts
{
    public class Font : MonoBehaviour, IInteractable
    {

        public GameObject[] InteractPoints;
        public GameObject Wather;
        private int _whaterLevel;

        void Start()
        {
            Wather.GetComponent<Renderer>().material.color = Color.blue;
        }

        public bool HasBeenActivated()
        {
            return _whaterLevel == 3;
        }

        public GameObject Interact(GameObject pickup)
        {
            if (!CanThisBeInteractedWith(pickup)) return null;
            pickup.GetComponent<Bucket.Scripts.Bucket>().HasWaterInIt = false;
            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return (pickup != null && pickup.GetComponent<Bucket.Scripts.Bucket>() != null && pickup.GetComponent<Bucket.Scripts.Bucket>().HasWaterInIt);
        }

        public Vector3 InteractPosition(Vector3 ai)
        {
            return InteractPoints.OrderBy(g => Vector3.Distance(g.transform.position, ai)).
                ToArray()[0].transform.position;
        }

        void RaiseWhater()
        {
            _whaterLevel += _whaterLevel > 2 ? 0 : 1;
            
        }
    }
}
