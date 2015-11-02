using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.UI;

public class SyllableHandler : DragHandler
{

    public int ID;

    private DatabaseManager db;

    public override void InitializeSyllable()
    {
        db = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();

        GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(ID).ImageName);
    }

}
