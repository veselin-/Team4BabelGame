using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.UI;

public class SymbolHandler : DragHandler
{

    public int ID;

    public Image Image1;
    public Image Image2;
    public Image Image3;

    private DatabaseManager databaseManager;

    // Use this for initialization
    void OnEnable ()
    {

        databaseManager =
            GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();


      //  UpdateSymbol();

    }

   public void PlaySound()
   {

        //AudioManager.Play(ID);

   }

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
            return;


        if (s.SyllableSequence.Count == 2)
        {
            Syllable s1 = databaseManager.GetSyllable(s.SyllableSequence[0]);
            Syllable s2 = databaseManager.GetSyllable(s.SyllableSequence[1]);
            SetSyllables(s1.ImageName, s2.ImageName);
        }
        else if (s.SyllableSequence.Count == 3)
        {
            Syllable s1 = databaseManager.GetSyllable(s.SyllableSequence[0]);
            Syllable s2 = databaseManager.GetSyllable(s.SyllableSequence[1]);
            Syllable s3 = databaseManager.GetSyllable(s.SyllableSequence[2]);
            SetSyllables(s1.ImageName, s2.ImageName, s3.ImageName);
        }


    }

    public override void InitializeSyllable()
    {
        
    }

}
