using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Pool.Scripts
{
    public class Pool : MonoBehaviour, IInteractable
    {

        public Transform InteractPoint;

        public bool HasBeenActivated()
        {
            return false;
        }

        public GameObject Interact(GameObject pickup)
        {
            if (CanThisBeInteractedWith(pickup))
                pickup.GetComponent<Bucket.Scripts.Bucket>().HasWaterInIt = true;
            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return pickup != null && pickup.tag == Constants.Tags.Bucket && !pickup.GetComponent<Bucket.Scripts.Bucket>().HasWaterInIt;
        }

        public Vector3 InteractPosition(Vector3 ai)
        {
            return InteractPoint.position;
        }
    }
}
