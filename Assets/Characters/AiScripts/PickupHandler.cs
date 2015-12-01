using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using Assets.Environment.Bucket.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Characters.AiScripts
{
    public class PickupHandler : MonoBehaviour
    {
        public Transform Pickups;

        private Animator anim;

        private readonly Dictionary<string, GameObject> _pickUps 
            = new Dictionary<string, GameObject>();
        private GameObject _currentPickup;

        public GameObject CurrentPickup {
            get
            {
                return _currentPickup;
            }
            private set
            {
                _currentPickup = value;
                UpdateDict();
            }
        }

        // Use this for initialization
        void Start ()
        {
            anim = GetComponent<Animator>();

            Transform root = transform;
            while (root.parent != null)
                root = root.parent;

            foreach (Transform pickUp in Pickups)
            {
                _pickUps.Add(pickUp.name, pickUp.gameObject);
            }
            UpdateDict();
        }

        #region  Picking up stuff
        public void PickUpItem(GameObject pickup)
        {
            DropCurrent();
            CurrentPickup = pickup;

            if (CurrentPickup != null) 
				CurrentPickup.SetActive (false);

        }

        public void UpdateDict()
        {
            foreach (var mr in _pickUps.Values)
                mr.SetActive(false);
            if(_currentPickup == null || !_pickUps.ContainsKey(_currentPickup.tag)) return;
            _pickUps[_currentPickup.tag].SetActive(true);
			_pickUps[_currentPickup.tag].GetComponent<AudioSource>().Play();
            if(_currentPickup.tag == Constants.Tags.Bucket)
                _pickUps["Water"].SetActive(CurrentPickup.GetComponent<Bucket>().HasWaterInIt);

            if (_currentPickup.tag == Constants.Tags.Torch)
            {
                anim.SetBool("Torch", true);
            }
            else
            {
                anim.SetBool("Torch", false);
            }
        }

        public void DropCurrent()
        {
            if(CurrentPickup == null) return;

            CurrentPickup.transform.position = transform.position;
            CurrentPickup.SetActive(true);
            CurrentPickup = null;
            UpdateDict();
        }
        
        #endregion

        #region Trading
        public GameObject Trade(GameObject tradeItem)
        {
            var old = CurrentPickup;
            CurrentPickup = tradeItem;
            return old;
        }

        public void InitiateTrade(PickupHandler other)
        {
            CurrentPickup = other.Trade(CurrentPickup);
        }
        #endregion
    }
}
