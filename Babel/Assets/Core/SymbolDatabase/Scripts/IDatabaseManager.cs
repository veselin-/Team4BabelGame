using UnityEngine;
using System.Collections.Generic;

public interface IDatabaseManager
{
    bool DatabasesLoaded();

    void AddSign(int id, List<int> syllableSequence);
    void AddSentence(int id, List<int> signSequence);

    Syllable GetSyllable(int id);
    Sign GetSign(int id);
    Sentence GetSentenceById(int id);
    int GetSentenceBySeq(List<int> signSequence);

    void SaveAllDb();
    void SaveSignsDb();
    void SaveSentencesDb();

    Sprite GetImage(string fileName);

}
