using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Characters.AiScripts
{
    public class PickupHandler : MonoBehaviour
    {
        private Dictionary<string, MeshRenderer> _pickUps = new Dictionary<string, MeshRenderer>(); 

        // Use this for initialization
        void Start ()
        {
            var pickups = transform.Find("Pickups");
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

            NoPickup();
            _pickUps[pickup.tag].enabled = true;
        }

        public void NoPickup()
        {
            foreach (var mr in _pickUps.Values)
            {
                mr.enabled = false;
            }
        }

    }
}
