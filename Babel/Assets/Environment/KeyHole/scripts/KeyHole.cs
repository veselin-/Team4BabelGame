using System.Collections;
using Assets.Core.Configuration;
using UnityEngine;
using Assets.Core.InteractableObjects;

public class KeyHole : MonoBehaviour, IInteractable
{

    public GameObject InteractPositionObject;

    public bool _hasBeenPulled;
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
        if (!CanThisBeInteractedWith(pickup)) return pickup;

        StartCoroutine(ChangeColor());
        Destroy(pickup);
        return null;
    }

    public bool CanThisBeInteractedWith(GameObject pickup)
    {
        return pickup != null && pickup.tag == Constants.Tags.Key && !_hasBeenPulled;
    }

    public Vector3 InteractPosition (Vector3 ai){
        return InteractPositionObject.transform.position;
    }

    IEnumerator ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.red;
        _hasBeenPulled = true;
        yield return new WaitForSeconds(5);
        GetComponent<Renderer>().material.color = _oldColor;
    }
}
