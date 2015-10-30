using UnityEngine;
using System.Collections.Generic;

public interface IDatabaseManager
{

    void AddWord(int id, List<int> syllableSequence);
    void AddSentence(int id, List<int> wordSequence);

    Syllable GetSyllable(int id);
    Word GetWord(int id);
    Sentence GetSentence(int id);

    void SaveAllDB();
    void SaveAlphabetDB();
    void SaveWordsDB();
    void SaveSentencesDB();

    Sprite GetImage(string fileName);

}
