using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class symbolPress : MonoBehaviour
{

    public GameObject slot, sylPanel;
    //public Text text;
    public static int count = 0;
    //GameObject chosenSign;
    //int childIndex;
    Transform parentBuf;
    bool moved = false;
    bool first = false, second = false, third = false;
    AudioManager am;
    public int ID;
    GameObject sylBuf, sylBlack;
    DatabaseManager db;
    //UiController uic;

    // Use this for initialization
    void Start()
    {
        InitializeSyllable();
    }

    void InitializeSyllable()
    {
        db = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
        am = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();
        //uic = GameObject.FindObjectOfType<UiController>().GetComponent<UiController>();
        //if (ID == 48)
        //{
        //    return;
        //}
        GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(ID).ImageName);
        //GetComponent<Image>().transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void SaveChildIndex()
    //{
    //    //chosenSign = Instantiate(gameObject);
    //    childIndex = transform.GetSiblingIndex();
    //}

    void PressedSylEffect()
    {
        parentBuf = transform.parent.transform;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.2f);
        //count += 1;
    }

    void MoveSignBackInBook()
    {
        //if (count == 3)
        //{
        //    Debug.Log("IM A CHILDE WITH A NEW PARENT");
        //    if (sylBuf.transform.parent.transform.parent.transform == (slot.transform))
        //    {
        //        Debug.Log("IM A CHILDE WITH A NEW PARENT");
        //        sylBuf.transform.GetChild(0).transform.GetChild(0).SetParent(slot.transform.GetChild(0).transform);
        //    }
        //}
        //else 
        //transform.SetSiblingIndex(childIndex);
        //transform.localScale = Vector3.one;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        //Debug.Log("CHANGE THE ALPHA BACK!");
        count -= 1;
        first = false;
        second = false;
        third = false;
        moved = false;
        //Debug.Log("COUNT IS DECREASING!!!: " + count);
        //Debug.Log("childindex er nu: " + childIndex);
        Destroy(sylBuf);
    }

    public void MoveSignBackInBookCombine()
    {
        sylBlack.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        //Debug.Log("CHANGE THE ALPHA BACK!");
        count -= 1;
        first = false;
        second = false;
        third = false;
        moved = false;
        //Debug.Log("COUNT IS DECREASING!!!: " + count);
        //Debug.Log("MINE BOOLS BLIVER OGSÅ SAT TIL FALSE!!!: " + third + second + first);
    }

    void PressedSymbol()
    {
        if (gameObject.name.Contains("Clone"))
        {
            //sylBlack.GetComponent<symbolPress>().MoveSignBackInBook();
            sylBlack.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            //sylBlack.transform.SetParent(parentBuf);
            count -= 1;
            first = false;
            second = false;
            third = false;
            moved = false;
            Destroy(gameObject);
            Debug.Log("JEG KOMMER ALDRIG HERIND");
            //count -= 1;
        }
        else if (count > 0 && (first || second || third) && moved && gameObject.GetComponent<Image>().color == new Color(0, 0, 0, 0.2f))
        {
            if (count == 3)
            {
                //Debug.Log("COUNT ER 3!!!");
                if (sylBuf.transform.parent.transform == (slot.transform))
                {
                    sylBuf.transform.GetChild(0).SetParent(slot.transform);
                    //Debug.Log("Jeg trykker på FØRSTE syl!!!");
                }
                else if (sylBuf.transform.parent.transform.parent.transform == (slot.transform))
                {
                    //sylBuf.transform.parent.transform.SetParent(slot.transform);
                    Transform what = sylBuf.transform.GetChild(0);
                    what.SetParent(sylBuf.transform.parent.transform);
                    what.localScale = Vector3.one;
                    //slot.transform.GetChild(0).transform.GetChild(0).localScale = Vector3.one;
                    //Debug.Log("Jeg trykker på ANDET syl!!!");
                }
            }
            else if (count == 2)
            {
                if (sylBuf.transform.parent.transform == (slot.transform))
                {
                    sylBuf.transform.GetChild(0).SetParent(slot.transform);
                }
            }
            else
            {
                transform.SetParent(parentBuf);
            }
            MoveSignBackInBook();
        }
        else
        {
            if (count != 3)
            {
                sylBuf = Instantiate(gameObject);
                sylBuf.GetComponent<symbolPress>().sylBlack = gameObject;
            }
            if (count == 2)
            {
                //SaveChildIndex();
                sylBuf.transform.SetParent(slot.transform.GetChild(0).transform.GetChild(0).transform);
                sylBuf.transform.localScale = new Vector2(1, 1);
                PressedSylEffect();
                count += 1;
                third = true;
                moved = true;
            }
            else if (count == 1)
            {
                //SaveChildIndex();
                sylBuf.transform.SetParent(slot.transform.GetChild(0).transform);
                sylBuf.transform.localScale = new Vector2(1, 1);
                PressedSylEffect();
                count += 1;
                second = true;
                moved = true;
            }
            else if (count == 0)
            {
                //SaveChildIndex();
                sylBuf.transform.SetParent(slot.transform);
                sylBuf.transform.localScale = new Vector2(2, 2);
                PressedSylEffect();
                count += 1;
                first = true;
                moved = true;
            }
        }
        Debug.Log("COUNT ER: " + count);
        //Debug.Log("pressing on real syllable!");
        //Debug.Log("moved ER: " + moved);
        //Debug.Log("first ER: " + first);
        //Debug.Log("second ER: " + second);
        //Debug.Log("third ER: " + third);
        //Debug.Log("childindex er nu: " + childIndex);
        am.FemaleSyllabusSoundPlay(ID);
    }
}