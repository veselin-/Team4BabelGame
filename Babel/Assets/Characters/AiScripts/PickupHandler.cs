using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Characters.AiScripts
{
    public class PickupHandler : MonoBehaviour
    {
        private readonly Dictionary<string, MeshRenderer> _pickUps 
            = new Dictionary<string, MeshRenderer>();
        private string _currentPickup;

        public string CurrentPickup {
            get
            {
                return _currentPickup;
            }
            set
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
                _pickUps.Add(pickUp.name, pickUp.GetComponent<MeshRenderer>());
            }
            UpdateDict();

        }

        #region  Picking up stuff
        public void PickUpItem(GameObject pickup)
        {
            string pickupItemName = null;
            if (pickup != null)
                pickupItemName = pickup.tag;
            
            CurrentPickup = pickupItemName;
        }

        public void UpdateDict()
        {
            foreach (var mr in _pickUps.Values)
                mr.enabled = false;
            if(_currentPickup == null || !_pickUps.ContainsKey(_currentPickup)) return;
            _pickUps[_currentPickup].enabled = true;
        }
        
        #endregion

        #region Trading
        public string Trade(string tradeItem)
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
