using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.EventSystems;

public abstract class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject SymbolBeingDragged;
    protected Vector3 _startPosition;
    public static Transform _startParent;

    protected AudioManager audioManager;

    void Start()
    {

        audioManager = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();

        InitializeSyllable();
    }

    public abstract void OnBeginDrag(PointerEventData eventData);
 

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //If the object is dragged from the book to anywhere but the sentence slots it should be returned. 
       // If the object is dragged from the sentence slots to anywhere but another sentence slot it should be destroyed.
        SymbolBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (_startParent.tag == "SentenceSlot")
        {
            if (transform.parent == _startParent)
            {
                Destroy(gameObject);
            }
            if (transform.parent.tag != "SentenceSlot")
            {
                Destroy(gameObject);
            }
        }
        if (_startParent.tag == "BookSlot")
        {

            if (transform.parent == _startParent)
            {
                transform.position = _startPosition;
            }
        }
    }

   public abstract void InitializeSyllable();


}
