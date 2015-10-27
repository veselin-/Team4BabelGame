using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IDropHandler {

    public GameObject symbol
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //If the slot is already taken by a symbol it should switch places with the new one
        if (symbol)
        {
            transform.GetChild(0).gameObject.transform.SetParent(DragHandler._startParent);
            DragHandler.SymbolBeingDragged.transform.SetParent(transform);

        }
        else
        {
            DragHandler.SymbolBeingDragged.transform.SetParent(transform);
        }

        


    }
}
