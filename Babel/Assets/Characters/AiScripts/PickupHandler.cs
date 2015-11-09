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
            Transform root = transform;
            while (root.parent != null)
                root = root.parent;

            var pickups = root.FindChild("Pickups");
            if(pickups == null)
                throw new Exception("You need to have a child called 'Pickups', for this script to work!");
            foreach (Transform pickUp in pickups)
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

            if(CurrentPickup != null)
                CurrentPickup.SetActive(false);
        }

        public void UpdateDict()
        {
            foreach (var mr in _pickUps.Values)
                mr.SetActive(false);
            if(_currentPickup == null || !_pickUps.ContainsKey(_currentPickup.tag)) return;
            _pickUps[_currentPickup.tag].SetActive(true);
            
            if(_currentPickup.tag == Constants.Tags.Bucket)
                _pickUps["Water"].SetActive(CurrentPickup.GetComponent<Bucket>().HasWaterInIt);
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
