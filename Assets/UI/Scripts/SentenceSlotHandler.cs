using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SentenceSlotHandler : MonoBehaviour, IDropHandler
{

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
        //If there is already a symbol in the sentence slot, and the new symbol comes from the book, it should be destoyed and replaced by the new one.
        if (symbol && DragHandler._startParent.tag == "BookSlot")
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        //If there is already a symbol in the sentence slot, and the new symbol comes from another sentence slot, it should switch places with the new one.
        if (symbol && DragHandler.SymbolBeingDragged.transform.parent.tag == "SentenceSlot")
        {

            transform.GetChild(0).gameObject.transform.SetParent(DragHandler._startParent);
            DragHandler.SymbolBeingDragged.transform.SetParent(transform);

        }

        //Need a new instace of the symbol at the old slot if it was dragged from the book.
        if (DragHandler._startParent.tag == "BookSlot")
        {
            GameObject sym;
            sym = Instantiate(DragHandler.SymbolBeingDragged);
            sym.transform.SetParent(DragHandler._startParent);
            sym.transform.name = DragHandler.SymbolBeingDragged.transform.name;
            sym.transform.localScale = Vector3.one;
            //Need to set the blockraycasts flag to true again.
            sym.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        DragHandler.SymbolBeingDragged.transform.SetParent(transform);



    }
}
