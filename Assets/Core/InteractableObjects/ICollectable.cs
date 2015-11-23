using UnityEngine;

namespace Assets.Core.InteractableObjects
{
    public interface ICollectable
    {
        /// <summary>
        /// Used to pick up a collectable.
        /// </summary>
        GameObject PickUp();
    }
}
