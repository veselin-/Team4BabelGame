using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Assets.Core.Configuration;
using System.Collections;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private Dictionary<int, Syllable> AlphabetDatabase;
    private Dictionary<int, Word> WordsDatabase;
    private Dictionary<int, Sentence> SentencesDatabase;

    private bool AlphabetDBLoaded = false;
    private bool WordsDBLoaded = false;
    private bool SentencesDBLoaded = false;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Adds the word to the predefined id and name and sets it active
    public void AddWord(int id, List<int> syllableSequence)
    {
        if (WordsDatabase.ContainsKey(id))
        {
            Word word = WordsDatabase[id];
            word.SyllableSequence = syllableSequence;
            word.IsActive = true;
            WordsDatabase[id] = word;
        }
    }

    // Adds the sentence to the predefined id 
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

    // Returns the word of the database with the given ID if it is active
    public Word GetWord(int id)
    {
        if (WordsDatabase.ContainsKey(id) && WordsDatabase[id].IsActive)
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

    public void LoadData()
    {
        StartCoroutine("LoadAlphabetDB");
        StartCoroutine("LoadWordsDB");
        StartCoroutine("LoadSentencesDB");
       
    }

    public bool DatabasesLoaded()
    {
        return (AlphabetDBLoaded && WordsDBLoaded && SentencesDBLoaded);
    }


    private IEnumerator LoadAlphabetDB()
    {
        WWW alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);
        WWW alphabetData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Alphabet);

        yield return alphabetData;

        if (!File.Exists(alphabetPath.url))
        {

            File.WriteAllBytes(alphabetPath.url, alphabetData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(AlphabetContainer));
        var charStream = new FileStream(alphabetPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as AlphabetContainer;
        charStream.Close();

        AlphabetDatabase = new Dictionary<int, Syllable>();
        foreach (Syllable syllable in container.Syllables)
        {
                AlphabetDatabase.Add(syllable.id, syllable);
        }
        AlphabetDBLoaded = true;
    }


    private IEnumerator LoadWordsDB()
    {
        WWW wordsPath = GetFilePath(Constants.XmlFiles.Words);
        WWW wordsData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Words);

        yield return wordsData;

        if (!File.Exists(wordsPath.url))
        {
            File.WriteAllBytes(wordsPath.url, wordsData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(WordsContainer));
        var charStream = new FileStream(wordsPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as WordsContainer;
        charStream.Close();

        WordsDatabase = new Dictionary<int, Word>();
        foreach (Word word in container.Words)
        {
            WordsDatabase.Add(word.id, word);
        }
        WordsDBLoaded = true;
    }

    private IEnumerator LoadSentencesDB()
    {
        WWW sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);
        WWW sentencesData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Sentences);

        yield return sentencesData;

        if (!File.Exists(sentencesPath.url))
        {
            File.WriteAllBytes(sentencesPath.url, sentencesData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(SentencesContainer));
        var charStream = new FileStream(sentencesPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as SentencesContainer;
        charStream.Close();

        SentencesDatabase = new Dictionary<int, Sentence>();
        foreach (Sentence sentence in container.Sentences)
        {
            SentencesDatabase.Add(sentence.id, sentence);
        }

        SentencesDBLoaded = true;
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

    public bool IsActive;
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


