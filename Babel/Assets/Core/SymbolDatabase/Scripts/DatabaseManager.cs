using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Assets.Core.Configuration;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private Dictionary<int, Syllable> AlphabetDatabase;
    private Dictionary<int, Sign> SignsDatabase;
    private Dictionary<int, Sentence> SentencesDatabase;

    private bool AlphabetDBLoaded = false;
    private bool SignsDBLoaded = false;
    private bool SentencesDBLoaded = false;

    public GameObject ExceptionImage;

    private 

    void Awake()
    {
        try
        {
          LoadData();
          DontDestroyOnLoad(transform.gameObject);
        }
        catch (Exception e)
        {
            ExceptionImage.SetActive(true);
        }
       
    }


    // Adds the sign to the predefined id and name and sets it active
    public void AddSign(int id, List<int> syllableSequence)
    {
        if (SignsDatabase.ContainsKey(id))
        {
            Sign sign = SignsDatabase[id];
            sign.SyllableSequence = syllableSequence;
            sign.IsActive = true;
            SignsDatabase[id] = sign;
        }
    }

    // Adds the sentence to the predefined id 
    public void AddSentence(int id, List<int> signSequence)
    {
        if (SentencesDatabase.ContainsKey(id))
        {
            Sentence sentence = SentencesDatabase[id];
            sentence.SignSequence = signSequence;
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

    // Returns the sign of the database with the given ID if it is active
    public Sign GetSign(int id)
    {
        if (SignsDatabase.ContainsKey(id) && SignsDatabase[id].IsActive)
        {
            return SignsDatabase[id];
        }
        return null;
    }

    // Returns the sentence of the database with the given ID
    public Sentence GetSentenceById(int id)
    {
        if (SentencesDatabase.ContainsKey(id))
        {
            return SentencesDatabase[id];
        }
        return null;
    }


    // Returns the sentence of the database with the given ID
    public int GetSentenceBySeq(List<int> signSequence)
    {
        Sentence sentence = SentencesDatabase.SingleOrDefault(x => x.Value.SignSequence.SequenceEqual(signSequence)).Value;

        return sentence != null ? sentence.id : -1;
    }

public void SaveAllDB()
    {
        SaveAlphabetDB();
        SaveSignsDB();
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

    public void SaveSignsDB()
    {
        WWW signsPath = GetFilePath(Constants.XmlFiles.Signs);

        signsContainer signsContainer = new signsContainer();
        signsContainer.Signs = new List<Sign>(SignsDatabase.Values);

        var serializer = new XmlSerializer(typeof(signsContainer));
        var stream = new FileStream(signsPath.url, FileMode.Create);
        serializer.Serialize(stream, signsContainer);
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
        LoadAlphabetDB();
        LoadsignsDB();
        LoadSentencesDB();
    }

    public bool DatabasesLoaded()
    {
        return (AlphabetDBLoaded && SignsDBLoaded && SentencesDBLoaded);
    }


    private void LoadAlphabetDB()
    {
        WWW alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);
        WWW alphabetData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Alphabet);

        while(!alphabetData.isDone)
        { }

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


    private void LoadsignsDB()
    {
        WWW signsPath = GetFilePath(Constants.XmlFiles.Signs);
        WWW signsData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Signs);

        while (!signsData.isDone)
        { }

        if (!File.Exists(signsPath.url))
        {
            File.WriteAllBytes(signsPath.url, signsData.bytes);
        }

        var charSerializer = new XmlSerializer(typeof(signsContainer));
        var charStream = new FileStream(signsPath.url, FileMode.Open);
        var container = charSerializer.Deserialize(charStream) as signsContainer;
        charStream.Close();

        SignsDatabase = new Dictionary<int, Sign>();
        foreach (Sign sign in container.Signs)
        {
            SignsDatabase.Add(sign.id, sign);
        }
        SignsDBLoaded = true;
    }

    private void LoadSentencesDB()
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

public class Sign
{
    [XmlAttribute("id")]
    public int id;

    public string Name;

    public List<int> SyllableSequence;

    public bool IsActive;
}

[XmlRoot("SignsCollection")]
public class signsContainer
{
    [XmlArray("Signs")]
    [XmlArrayItem("Sign")]
    public List<Sign> Signs = new List<Sign>();
}


public class Sentence
{
    [XmlAttribute("id")]
    public int id;

    public List<int> SignSequence;
}

[XmlRoot("SentencesCollection")]
public class SentencesContainer
{
    [XmlArray("Sentences")]
    [XmlArrayItem("Sentence")]
    public List<Sentence> Sentences = new List<Sentence>();
}


