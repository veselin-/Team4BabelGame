using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SymbolHandler : MonoBehaviour //DragHandler
{
    public int ID;
    public Image Image1;
    public Image Image2;
    public Image Image3;
    public GameObject sens1, sens2, sens3, sens4, book;
    bool oneSens = false, twoSens = false, threeSens = false, fourSens = false;
    bool movedSign = false, isMade = false;
    int childIndex;
    //GameObject goTemp;
    //Transform parentBuffer;
    public static int count = 0;
    private DatabaseManager databaseManager;
    AudioManager am;


    // Use this for initialization
    void OnEnable()
    {
        databaseManager = GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();
    }

    void Start()
    {
        am = GameObject.FindGameObjectWithTag(Constants.Tags.AudioManager).GetComponent<AudioManager>();
    }
    public void PlaySound()
    {
        //AudioManager.Play(ID);
    }

    void SaveChildIndex()
    {
        //chosenSign = Instantiate(gameObject);
        childIndex = transform.GetSiblingIndex();
    }

    //void SaveTransformOfParent()
    //{
    //    parentBuffer = transform.parent.transform;
    //}

    void MoveSignBackInBook()
    {
        //transform.SetParent(parentBuffer);
        transform.SetParent(book.transform);
        transform.SetSiblingIndex(childIndex);
        //DestroyObject(gameObject);
        transform.localScale = Vector3.one;
        count -= 1;
        movedSign = false;
        oneSens = false;
        //twoSens = false;
        //threeSens = false;
        //fourSens = false;
    }

    public void ResetSigns()
    {
        if (count > 0 && oneSens || twoSens || threeSens || fourSens && movedSign)
        {
            if (oneSens)
            {
                oneSens = false;
            }
            //else if (twoSens)
            //{
            //    twoSens = false;
            //}
            //else if (threeSens)
            //{
            //    threeSens = false;
            //}
            //else if (fourSens)
            //{
            //    fourSens = false;
            //}
            MoveSignBackInBook();
            movedSign = false;
        }
        Debug.Log("IM HERE DOIJNG IT");
    }

    public void PressedSign()
    {
        if (isMade)
        {
            //goTemp = Instantiate(gameObject.transform.GetChild(0).gameObject);
            if (count > 0 && oneSens || twoSens || threeSens || fourSens && movedSign)
            {
                //Debug.Log("IM HERE DOIJNG IT");
                if (oneSens)
                {
                    oneSens = false;
                }
                //else if (twoSens)
                //{
                //    twoSens = false;
                //}
                //else if (threeSens)
                //{
                //    threeSens = false;
                //}
                //else if (fourSens)
                //{
                //    fourSens = false;
                //}
                MoveSignBackInBook();
                //DestroyObject(goTemp.gameObject);
                movedSign = false;
            }
            else
            {
                movedSign = true;
                //if (count == 3)
                //{
                //    SaveChildIndex();
                //    fourSens = true;
                //    transform.SetParent(sens4.transform);
                //    count += 1;
                //}
                //else if (count == 2)
                //{
                //    SaveChildIndex();
                //    threeSens = true;
                //    transform.SetParent(sens3.transform);
                //    count += 1;
                //}
                //else if (count == 1)
                //{
                //    SaveChildIndex();
                //    twoSens = true;
                //    transform.SetParent(sens2.transform);
                //    count += 1;
                //}
                //else 
                if (count == 0)
                {
                    SaveChildIndex();
                    //SaveTransformOfParent();
                    oneSens = true;
                    //goTemp.transform.SetParent(transform.GetChild(0).transform);
                    //goTemp.transform.localScale = Vector3.one;
                    //transform.GetChild(0).transform.SetParent(sens1.transform);
                    transform.SetParent(sens1.transform);
                    count += 1;
                }
            }
            Debug.Log("COUNT ER: " + count);
            Debug.Log("moved ER: " + movedSign);
            Debug.Log("first ER: " + oneSens);
            //Debug.Log("second ER: " + twoSens);
            //Debug.Log("third ER: " + threeSens);
            //Debug.Log("third ER: " + fourSens);
            am.StartPlayCoroutine(ID);
        }
    }

    //public override void OnBeginDrag(PointerEventData eventData)
    //{
    //    SymbolBeingDragged = gameObject;
    //    _startPosition = transform.position;
    //    _startParent = transform.parent;
    //    GetComponent<CanvasGroup>().blocksRaycasts = false;
    //    audioManager.StartPlayCoroutine(ID);
    //}

    public void SetSyllables(string syl1, string syl2)
    {
        Image1.sprite = databaseManager.GetImage(syl1);
        Image1.color = Color.white;
        Image2.sprite = databaseManager.GetImage(syl2);
        Image2.color = Color.white;
        Image3.color = Color.clear;
    }

    public void SetSyllables(string syl1, string syl2, string syl3)
    {
        Image1.sprite = databaseManager.GetImage(syl1);
        Image1.color = Color.white;
        Image2.sprite = databaseManager.GetImage(syl2);
        Image2.color = Color.white;
        Image3.sprite = databaseManager.GetImage(syl3);
        Image3.color = Color.white;
    }

    public void UpdateSymbol()
    {
        Sign s = databaseManager.GetSign(ID);
        if (s == null)
        {
            SetSyllables(null, null, null);
            Image1.color = Color.clear;
            Image2.color = Color.clear;
            Image3.color = Color.clear;
            isMade = false;
            return;
        }
        if (s.SyllableSequence.Count == 2)
        {
            Syllable s1 = databaseManager.GetSyllable(s.SyllableSequence[0]);
            Syllable s2 = databaseManager.GetSyllable(s.SyllableSequence[1]);
            SetSyllables(s1.ImageName, s2.ImageName);
            isMade = true;
        }
        else if (s.SyllableSequence.Count == 3)
        {
            Syllable s1 = databaseManager.GetSyllable(s.SyllableSequence[0]);
            Syllable s2 = databaseManager.GetSyllable(s.SyllableSequence[1]);
            Syllable s3 = databaseManager.GetSyllable(s.SyllableSequence[2]);
            SetSyllables(s1.ImageName, s2.ImageName, s3.ImageName);
            isMade = true;
        }
    }
    //public override void InitializeSyllable()
    //{
        
    //}
}
