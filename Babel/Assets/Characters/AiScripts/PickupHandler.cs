using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.AiScripts
{
    public class PickupHandler : MonoBehaviour
    {

        public bool InTradingMode;

        private readonly Dictionary<string, MeshRenderer> _pickUps 
            = new Dictionary<string, MeshRenderer>();

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
            NoPickup();
        }

        public void PickUpItem(GameObject pickup)
        {
            if (!_pickUps.ContainsKey(pickup.tag))
                throw new Exception("This object cannot be shown on the player, problem?");

            PickUpItem(pickup.tag);
        }

        private void PickUpItem(string pickup)
        {
            NoPickup();
            if (pickup == null || !_pickUps.ContainsKey(pickup)) return;
            _pickUps[pickup].enabled = true;
        }

        public void NoPickup()
        {
            foreach (var mr in _pickUps.Values)
                mr.enabled = false;
        }

        public string Trade(string tradeItem)
        {
            var currentItem = _pickUps.Values.FirstOrDefault(i => i.enabled);
            PickUpItem(tradeItem);
            return currentItem == null ? null : currentItem.name;
        }

        public void InitiateTrade(PickupHandler other)
        {
            if(!other.InTradingMode) return;
            var currentItem = _pickUps.Values.FirstOrDefault(i => i.enabled);
            PickUpItem(other.Trade(currentItem == null ? null : currentItem.name));
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != Constants.Tags.SideKick) return;
            InitiateTrade(other.GetComponent<PickupHandler>());
        }
    }
}
