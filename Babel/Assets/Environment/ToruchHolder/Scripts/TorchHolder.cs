using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.ToruchHolder.Scripts
{
    public class TorchHolder : MonoBehaviour, IInteractable
    {

        public Transform InteractPos;
        public GameObject TorchInHolder;
        public GameObject TorchPrefab;
        public bool HasTorch;
        public bool Interactale;

        // Use this for initialization
        void Start ()
        {
            UpdateTorch();
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public bool HasBeenActivated()
        {
            return HasTorch;
        }

        public GameObject Interact(GameObject pickup)
        {
            if (!CanThisBeInteractedWith(pickup)) return pickup;
            HasTorch = !HasTorch;
            UpdateTorch();
            if (pickup == null)
                return Instantiate(TorchPrefab);

            Destroy(pickup);
            return null;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return Interactale && (
                (pickup != null && pickup.tag == Constants.Tags.Torch && !HasTorch) ||
                (pickup == null && HasTorch));
        }

        public Vector3 InteractPosition(Vector3 ai)
        {
            return InteractPos.position;
        }

        private void UpdateTorch()
        {
            TorchInHolder.SetActive(HasTorch);
        }
    }
}
