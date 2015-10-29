using System.Collections;
using Assets.Core.Configuration;
using UnityEngine;
using Assets.Core.InteractableObjects;

public class KeyHole : MonoBehaviour, IInteractable {

    private bool _hasBeenPulled;
    private Color _oldColor;

    void Start()
    {
        _oldColor = GetComponent<Renderer>().material.color;
    }

    public bool HasBeenActivated()
    {
        return _hasBeenPulled;
    }

    public GameObject Interact(GameObject pickup)
    {
        if (pickup == null || pickup.tag != Constants.Tags.Key || _hasBeenPulled)
            return pickup;

        StartCoroutine(ChangeColor());
        return null;
    }

    IEnumerator ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.red;
        _hasBeenPulled = true;
        yield return new WaitForSeconds(5);
        GetComponent<Renderer>().material.color = _oldColor;
        _hasBeenPulled = false;
    }
}
