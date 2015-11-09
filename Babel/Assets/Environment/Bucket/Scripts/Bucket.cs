using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Bucket.Scripts
{
    public class Bucket : MonoBehaviour, ICollectable
    {
        public bool HasWather;
        public GameObject Water;

        public bool HasWaterInIt {
            get { return HasWather; }
            set { HasWather = value; LetThereBeWhater(); }
        }

        void Start()
        {
            LetThereBeWhater();
        }

        public GameObject PickUp()
        {
            gameObject.SetActive(false);
            return gameObject;
        }

        private void LetThereBeWhater()
        {
            Water.SetActive(HasWather);
        }
    }
}
