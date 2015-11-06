using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Bucket.Scripts
{
    public class Bucket : MonoBehaviour, ICollectable
    {
        private bool _hasWather;
        public bool HasWaterInIt {
            get { return _hasWather; }
            set { _hasWather = value; LetThereBeWhater(); }
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
            // TODO Add Rosens implementation of whater in the bucket.. 
            transform.GetChild(0).GetComponent<Renderer>().material.color = _hasWather ? Color.blue : Color.white;
        }
    }
}
