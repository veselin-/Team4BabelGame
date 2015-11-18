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
    bool moved = false, first = false, second = false, third = false;
    AudioManager am;
    public int ID;
    GameObject sylBuf, sylBlack;
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

    void PressedSylEffect()
    {
        parentBuf = transform.parent.transform;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.2f);
        count += 1;
    }

    public void MoveSignBackInBook()
    {
        transform.SetParent(parentBuf);
        //transform.SetSiblingIndex(childIndex);
        //transform.localScale = Vector3.one;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
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

    //void InsSignInSlot()
    //{
    //    sylBuf = Instantiate(gameObject);
    //    sylBuf.transform.SetParent(slot.transform);
    //    sylBuf.transform.localScale = Vector2.one;
    //}

    public void PressedSymbol()
    {
        if (gameObject.name.Contains("Clone"))
        {
            sylBlack.GetComponent<symbolPress>().MoveSignBackInBook();
            //sylBlack.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            Destroy(gameObject);
            //count -= 1;
        }
        else if (count > 0 && first || second || third && moved)
        {
            Destroy(sylBuf);
            MoveSignBackInBook();
        }
        else
        {
            sylBuf = Instantiate(gameObject);
            //sylBuf.GetComponent<Button>().enabled = false;
            sylBuf.GetComponent<symbolPress>().sylBlack = gameObject;
            moved = true;
            if (count == 2)
            {
                //SaveChildIndex();
                sylBuf.transform.SetParent(slot.transform.GetChild(0).transform.GetChild(0).transform);
                sylBuf.transform.localScale = new Vector2(1, 1);
                PressedSylEffect();
                third = true;

                //InsSignInSlot();
            }
            else if (count == 1)
            {
                //SaveChildIndex();
                sylBuf.transform.SetParent(slot.transform.GetChild(0).transform);
                sylBuf.transform.localScale = new Vector2(1, 1);
                PressedSylEffect();
                second = true;
                //InsSignInSlot();
            }
            else if (count == 0)
            {
                //SaveChildIndex();
                sylBuf.transform.SetParent(slot.transform);
                sylBuf.transform.localScale = new Vector2(2, 2);
                PressedSylEffect();
                first = true;
                //InsSignInSlot();
            }
        }
        Debug.Log("COUNT ER: " + count);
        //Debug.Log("moved ER: " + moved);
        //Debug.Log("first ER: " + first);
        //Debug.Log("second ER: " + second);
        //Debug.Log("third ER: " + third);
        //Debug.Log("childindex er nu: " + childIndex);
        am.FemaleSyllabusSoundPlay(ID);
    }
}
