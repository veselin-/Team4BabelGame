using UnityEngine;

namespace Assets.Core.InteractableObjects
{
    public interface IInteractable
    {
        bool HasBeenActivated();

        GameObject Interact(GameObject pickup);
    }
}
