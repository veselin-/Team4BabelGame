using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using UnityEngine.UI;

public class SymbolHandler : DragHandler
{

    public int ID;

    public Image Image1;
    public Image Image2;

    private DatabaseManager databaseManager;

    // Use this for initialization
    void OnEnable ()
    {

        databaseManager =
            GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>();

        Word w = databaseManager.GetWord(ID);

        Syllable s1 = databaseManager.GetSyllable(w.SyllableSequence[0]);
        Syllable s2 = databaseManager.GetSyllable(w.SyllableSequence[1]);

        SetSyllables(s1.ImageName, s2.ImageName);

    }

   public void PlaySound()
   {

        //AudioManager.Play(ID);

   }

    public void SetSyllables(string syl1, string syl2)
    {
        Image1.sprite = databaseManager.GetImage(syl1);
        Image2.sprite = databaseManager.GetImage(syl2);
    }

}
