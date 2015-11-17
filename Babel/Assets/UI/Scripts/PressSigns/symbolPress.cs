using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class symbolPress : MonoBehaviour {

    public GameObject slot, sylPanel;
    public static int count = 0;
    //GameObject chosenSign;
    //int childIndex;
    Transform parentBuf;
    bool moved = false;
    bool first = false, second = false, third = false;
    AudioManager am;
    public int ID;

    private DatabaseManager db;
    // Use this for initialization


    void Start()
    {
        InitializeSyllable();
    }

    public void InitializeSyllable()
    {
        db = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
        am = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();
        GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(ID).ImageName);
    }

    // Update is called once per frame
    void Update () {
	
	}

    //void SaveChildIndex()
    //{
    //    //chosenSign = Instantiate(gameObject);
    //    childIndex = transform.GetSiblingIndex();
    //}

    void SaveTransformOfParent()
    {
        parentBuf = transform.parent.transform;
    }

    public void MoveSignBackInBook()
    {
        transform.SetParent(parentBuf);
        //transform.SetSiblingIndex(childIndex);
        transform.localScale = Vector3.one;
        count -= 1;
        first = false;
        second = false;
        third = false;
        moved = false;
        //Debug.Log("childindex er nu: " + childIndex);
    }

    //public void ResetSyllabels()
    //{
    //    if (count > 0 && first || second || third && moved)
    //    {
    //        if (first)
    //        {
    //            first = false;
    //        }
    //        if (second)
    //        {
    //            second = false;
    //        }
    //        if (third)
    //        {
    //            third = false;
    //        }
    //        MoveSignBackInBook();
    //        moved = false;
    //    }
    //    Debug.Log("THIS HAS BEEN DONE");
    //}

    public void PressedSymbol()
    {
        if (count > 0 && first || second || third && moved)
        {
            MoveSignBackInBook();
        }
        else
        {
            moved = true;
            if (count == 2)
            {
                //SaveChildIndex();
                SaveTransformOfParent();
                third = true;
                transform.SetParent(slot.transform.GetChild(0).transform.GetChild(0).transform);
                transform.localScale = new Vector2(1f, 1f);
                count += 1;
            }
            else if (count == 1)
            {
                //SaveChildIndex();
                SaveTransformOfParent();
                second = true;
                transform.SetParent(slot.transform.GetChild(0).transform);
                transform.localScale = new Vector2(1, 1);
                count += 1;
            }
            else if (count == 0)
            {
                //SaveChildIndex();
                SaveTransformOfParent();
                first = true;
                transform.SetParent(slot.transform);
                transform.localScale = new Vector2(2, 2);
                count += 1;
            }
        }
        //Debug.Log("COUNT ER: " + count);
        //Debug.Log("moved ER: " + moved);
        //Debug.Log("first ER: " + first);
        //Debug.Log("second ER: " + second);
        //Debug.Log("third ER: " + third);
        //Debug.Log("childindex er nu: " + childIndex);
        am.FemaleSyllabusSoundPlay(ID);
    }
}
