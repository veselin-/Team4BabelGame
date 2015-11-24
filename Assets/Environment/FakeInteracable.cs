using UnityEngine;
using Assets.Core.InteractableObjects;

public class FakeInteracable : MonoBehaviour, IInteractable
{

    public bool Activated;

    public bool HasBeenActivated()
    {
        return Activated;
//        throw new System.NotImplementedException();
    }

    public GameObject Interact(GameObject pickup)
    {
        return null;
//        throw new System.NotImplementedException();
    }

    public bool CanThisBeInteractedWith(GameObject pickup)
    {
        return false;
//        throw new System.NotImplementedException();
    }

    public Vector3 InteractPosition(Vector3 ai)
    {
        return Vector3.zero;
//        throw new System.NotImplementedException();
    }
}
