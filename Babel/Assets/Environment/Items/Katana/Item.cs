using UnityEngine;
using Assets.Core.InteractableObjects;

public class Item : MonoBehaviour, ICollectable {

    public GameObject PickUp()
    {
        gameObject.SetActive(false);
        return gameObject;
    }
}
