using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SyllabelHandler : MonoBehaviour //DragHandler
{

    //public int ID;

    //private DatabaseManager db;

    //void Start()
    //{
    //    InitializeSyllable();
    //}

    //public void InitializeSyllable()
    //{
    //    db = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();

    //    GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(ID).ImageName);
    //}

    //public override void OnBeginDrag(PointerEventData eventData)
    //{
    //    SymbolBeingDragged = gameObject;

    //    _startPosition = transform.position;
    //    _startParent = transform.parent;
    //    GetComponent<CanvasGroup>().blocksRaycasts = false;

    //    audioManager.FemaleSyllabusSoundPlay(ID);

    //}

}
