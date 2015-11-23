using UnityEngine;

namespace Assets.Core.InteractableObjects
{
    public interface IInteractable
    {
        bool HasBeenActivated();

        GameObject Interact(GameObject pickup);

        bool CanThisBeInteractedWith(GameObject pickup);

        Vector3 InteractPosition(Vector3 ai);
    }
}
