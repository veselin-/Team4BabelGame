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
        //If there is already a symbol in the sentence slot it should be destoyed and replaced by the new one.
        if (symbol)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
      
        //Need a new instace of the symbol at the old slot
        GameObject sym;
        sym = Instantiate(DragHandler.SymbolBeingDragged);
        sym.transform.SetParent(DragHandler._startParent);
        sym.transform.name = DragHandler.SymbolBeingDragged.transform.name;
        //Need to set the blockraycasts flag to true again.
        sym.GetComponent<CanvasGroup>().blocksRaycasts = true;
        DragHandler.SymbolBeingDragged.transform.SetParent(transform);



    }
}
