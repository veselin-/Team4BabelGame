using UnityEngine;

namespace Assets.Core.InteractableObjects
{
    public interface IInteractable
    {
        bool HasBeenActivated();

        string Interact(string pickup);
    }
}
