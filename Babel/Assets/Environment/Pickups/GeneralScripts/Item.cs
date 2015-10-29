using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Items.GeneralScripts
{
    public class Item : MonoBehaviour, ICollectable {

        public GameObject PickUp()
        {
            gameObject.SetActive(false);
            return gameObject;
        }
    }
}
