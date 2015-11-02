using UnityEngine;
using System.Collections.Generic;

public interface IDatabaseManager
{

    void LoadData();
    bool DatabasesLoaded();

    void AddSign(int id, List<int> syllableSequence);
    void AddSentence(int id, List<int> signSequence);

    Syllable GetSyllable(int id);
    Sign GetSign(int id);
    Sentence GetSentence(int id);

    void SaveAllDB();
    void SaveAlphabetDB();
    void SaveSignsDB();
    void SaveSentencesDB();

    Sprite GetImage(string fileName);

}
