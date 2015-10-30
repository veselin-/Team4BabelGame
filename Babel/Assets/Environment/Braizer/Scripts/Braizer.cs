using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Braizer.Scripts
{
    public class Braizer : MonoBehaviour, IInteractable
    {
        public GameObject TorchPrefab;
        public GameObject StickPrefab;
        public bool StartLighted;

        public bool OnFire {
            set { _psystem.enableEmission = value; }
            get { return _psystem.enableEmission; }
        }

        private ParticleSystem _psystem;

        void Start()
        {
            _psystem = GetComponent<ParticleSystem>();
            OnFire = StartLighted;
        }

        public bool HasBeenActivated()
        {
            return OnFire;
        }

        public GameObject Interact(GameObject pickup)
        {
            if (pickup == null) return null;

            if (OnFire && pickup.tag == Constants.Tags.Stick)
            {
                Destroy(pickup);
                return Instantiate(TorchPrefab);
            }

            if (!OnFire && pickup.tag == Constants.Tags.Torch)
            {
                Destroy(pickup);
                OnFire = true;
                return Instantiate(StickPrefab);
            }

            return pickup;
        }
    }
}
