using UnityEngine;

namespace Assets.Core.InteractableObjects
{
    public interface IInteractable
    {
        bool HasBeenActivated();

        void Interact();
    }
}
