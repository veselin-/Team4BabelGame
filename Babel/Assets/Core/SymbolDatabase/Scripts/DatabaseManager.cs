using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Assets.Core.Configuration;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private Dictionary<int, Syllable> AlphabetDatabase;
    private Dictionary<int, Word> WordsDatabase;
    private Dictionary<int, Sentence> SentencesDatabase;

    // Use this for initialization
    void Awake()
    {
        LoadData();
    }

    // Adds the word to the predefined id and name
    public void AddWord(int id, List<int> syllableSequence)
    {
        if (WordsDatabase.ContainsKey(id))
        {
            Word word = WordsDatabase[id];
            word.SyllableSequence = syllableSequence;
            WordsDatabase[id] = word;
        }
    }

    // Adds the sentence to the end of the database
    public void AddSentence(int id, List<int> wordSequence)
    {
        if (SentencesDatabase.ContainsKey(id))
        {
            Sentence sentence = SentencesDatabase[id];
            sentence.WordSequence = wordSequence;
            SentencesDatabase[id] = sentence;
        }
    }


    // Returns the character of the database with the given ID
    public Syllable GetSyllable(int id)
    {
        if (AlphabetDatabase.ContainsKey(id))
        {
            return AlphabetDatabase[id];
        }

        return null;
    }

    // Returns the word of the database with the given ID
    public Word GetWord(int id)
    {
        if (WordsDatabase.ContainsKey(id))
        {
            return WordsDatabase[id];
        }
        return null;
    }

    // Returns the sentence of the database with the given ID
    public Sentence GetSentence(int id)
    {
        if (SentencesDatabase.ContainsKey(id))
        {
            return SentencesDatabase[id];
        }
        return null;
    }

    public void SaveAllDB()
    {
        SaveAlphabetDB();
        SaveWordsDB();
        SaveSentencesDB();
    }

    public void SaveAlphabetDB()
    {
        WWW alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);

        AlphabetContainer alphabetContainer = new AlphabetContainer();
        alphabetContainer.Syllables = new List<Syllable>(AlphabetDatabase.Values);

        var serializer = new XmlSerializer(typeof(AlphabetContainer));
        var stream = new FileStream(alphabetPath.url, FileMode.Create);
        serializer.Serialize(stream, alphabetContainer);
        stream.Close();
    }

    public void SaveWordsDB()
    {
        WWW wordsPath = GetFilePath(Constants.XmlFiles.Words);

        WordsContainer wordsContainer = new WordsContainer();
        wordsContainer.Words = new List<Word>(WordsDatabase.Values);

        var serializer = new XmlSerializer(typeof(WordsContainer));
        var stream = new FileStream(wordsPath.url, FileMode.Create);
        serializer.Serialize(stream, wordsContainer);
        stream.Close();
    }

    public void SaveSentencesDB()
    {
        WWW sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);

        SentencesContainer sentencesContainer = new SentencesContainer();
        sentencesContainer.Sentences = new List<Sentence>(SentencesDatabase.Values);

        var serializer = new XmlSerializer(typeof(SentencesContainer));
        var stream = new FileStream(sentencesPath.url, FileMode.Create);
        serializer.Serialize(stream, sentencesContainer);
        stream.Close();
    }

    //Used to get an image from the resources folder in the game
    //It is used to get the default images
    public Sprite GetImage(string fileName)
    {
        return (Sprite)Resources.Load(fileName, typeof(Sprite));
    }

    private void LoadData()
    {
        AlphabetDatabase = LoadAlphabetDB();
        WordsDatabase = LoadWordsDB();
        SentencesDatabase = LoadSentencesDB();
    }


    private Dictionary<int, Syllable> LoadAlphabetDB()
    {
        WWW alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);
        WWW alphabetData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Alphabet);

        while (!alphabetData.isDone)
        { }

        if (!File.Exists(alphabetPath.url))
        {

            File.WriteAllBytes(alphabetPath.url, alphabetData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(AlphabetContainer));
        var charStream = new FileStream(alphabetPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as AlphabetContainer;
        charStream.Close();

        Dictionary<int, Syllable> tempDict = new Dictionary<int, Syllable>();
        foreach (Syllable character in container.Syllables)
        {
            tempDict.Add(character.id, character);
        }
        return tempDict;
    }


    private Dictionary<int, Word> LoadWordsDB()
    {
        WWW wordsPath = GetFilePath(Constants.XmlFiles.Words);
        WWW wordsData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Words);

        while (!wordsData.isDone)
        { }

        if (!File.Exists(wordsPath.url))
        {
            File.WriteAllBytes(wordsPath.url, wordsData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(WordsContainer));
        var charStream = new FileStream(wordsPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as WordsContainer;
        charStream.Close();

        Dictionary<int, Word> tempDict = new Dictionary<int, Word>();
        foreach (Word word in container.Words)
        {
            tempDict.Add(word.id, word);
        }
        return tempDict;

    }

    private Dictionary<int, Sentence> LoadSentencesDB()
    {
        WWW sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);
        WWW sentencesData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Sentences);
        while (!sentencesData.isDone)
        { }

        if (!File.Exists(sentencesPath.url))
        {
            File.WriteAllBytes(sentencesPath.url, sentencesData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(SentencesContainer));
        var charStream = new FileStream(sentencesPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as SentencesContainer;
        charStream.Close();

        Dictionary<int, Sentence> tempDict = new Dictionary<int, Sentence>();
        foreach (Sentence sentence in container.Sentences)
        {
            tempDict.Add(sentence.id, sentence);
        }
        return tempDict;

    }

    private WWW GetFilePath(string fileName)
    {

#if UNITY_ANDROID && !UNITY_EDITOR //For running in Android
           return new WWW(Application.persistentDataPath + "/"+fileName);          
#endif
#if UNITY_EDITOR // For running in Unity
        return new WWW(Application.streamingAssetsPath + "/" + fileName);
#endif

    }


}

public class Syllable
{
    [XmlAttribute("id")]
    public int id;

    public string ImageName;

    public string SoundName;
}

[XmlRoot("AlphabetCollection")]
public class AlphabetContainer
{
    [XmlArray("Syllables")]
    [XmlArrayItem("Syllable")]
    public List<Syllable> Syllables = new List<Syllable>();
}

public class Word
{
    [XmlAttribute("id")]
    public int id;

    public string Name;

    public List<int> SyllableSequence;
}

[XmlRoot("WordsCollection")]
public class WordsContainer
{
    [XmlArray("Words")]
    [XmlArrayItem("Word")]
    public List<Word> Words = new List<Word>();
}


public class Sentence
{
    [XmlAttribute("id")]
    public int id;

    public List<int> WordSequence;
}

[XmlRoot("SentencesCollection")]
public class SentencesContainer
{
    [XmlArray("Sentences")]
    [XmlArrayItem("Sentence")]
    public List<Sentence> Sentences = new List<Sentence>();
}


