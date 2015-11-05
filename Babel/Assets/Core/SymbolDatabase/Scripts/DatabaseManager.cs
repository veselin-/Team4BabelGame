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
using Assets.Environment.Levers.LeverExample.Scripts;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private Dictionary<int, Syllable> AlphabetDatabase;
    private Dictionary<int, Sign> SignsDatabase;
    private Dictionary<int, Sentence> SentencesDatabase;

    private bool AlphabetDBLoaded = false;
    private bool SignsDBLoaded = false;
    private bool SentencesDBLoaded = false;
    //bool fuckyou = true;

    public GameObject ExceptionImage;

    private 

    void Awake()
    {
        try
        {
          LoadData();
          //DontDestroyOnLoad(transform.gameObject);
        }
        catch (Exception e)
        {

            Debug.Log("Couldn't load database. Probably an error in the XML" + e);
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
        if (signSequence.Count > 0)
        {
            Sentence sentence =
                SentencesDatabase.FirstOrDefault(x => x.Value.SignSequence.SequenceEqual(signSequence)).Value;

            return sentence != null ? sentence.id : -1;
        }
        return -1;
    }

public void SaveAllDB()
    {
        SaveAlphabetDB();
        SaveSignsDB();
        SaveSentencesDB();
    }

    public void SaveAlphabetDB()
    {
        var alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);

        AlphabetContainer alphabetContainer = new AlphabetContainer();
        alphabetContainer.Syllables = new List<Syllable>(AlphabetDatabase.Values);

        var serializer = new XmlSerializer(typeof(AlphabetContainer));

        var stream = new MemoryStream();
        serializer.Serialize(stream, alphabetContainer);

        
        stream.Close();
    }

    public void SaveSignsDB()
    {
        var signsPath = GetFilePath(Constants.XmlFiles.Signs);

        signsContainer signsContainer = new signsContainer();
        signsContainer.Signs = new List<Sign>(SignsDatabase.Values);

        var serializer = new XmlSerializer(typeof(signsContainer));

        var stream = new MemoryStream();
        serializer.Serialize(stream, signsContainer);
        File.WriteAllBytes(signsPath, stream.ToArray());
        stream.Close();
    }

    public void SaveSentencesDB()
    {
        var sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);

        SentencesContainer sentencesContainer = new SentencesContainer();
        sentencesContainer.Sentences = new List<Sentence>(SentencesDatabase.Values);

        var serializer = new XmlSerializer(typeof(SentencesContainer));
        var stream = new FileStream(sentencesPath, FileMode.Create);
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

    public void ResetSavedSigns()
    {
        PlayerPrefsBool.SetBool("SideKickHat", false);
        PlayerPrefsBool.SetBool("PlayerHat", false);
        PlayerPrefs.SetInt("CurrencyAmount", CurrencyControl.currencyAmount * 0);
        PlayerPrefsBool.SetBool("SevenElever", false);
        LeverPulls.leverpulls = 0;
        var signsPath = GetFilePath(Constants.XmlFiles.Signs);
        var bindata = (TextAsset) Resources.Load("Signs");
        File.WriteAllBytes(signsPath, bindata.bytes);
        LoadData();
    }

    private void LoadAlphabetDB()
    {
        var alphabetPath = GetFilePath(Constants.XmlFiles.Alphabet);
        //WWW alphabetData = new WWW(Application.streamingAssetsPath + "/" + Constants.XmlFiles.Alphabet);

        //WWW alphabetData = alphabetPath;


        if (!File.Exists(alphabetPath))
        {
            TextAsset bindata = Resources.Load("Alphabet") as TextAsset;
            if(bindata == null)
                ExceptionImage.SetActive(true);
            else 
                File.WriteAllBytes(alphabetPath, bindata.bytes);
            //ExceptionImage.SetActive(true);
        }

        var charSerializer = new XmlSerializer(typeof(AlphabetContainer));

        //string escapeURL = WWW.EscapeURL(alphabetPath.url);

        var bytes = File.ReadAllBytes(alphabetPath);
        var charStream = new MemoryStream(bytes);//new FileStream(alphabetPath.url, FileMode.Open);

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
        var signsPath = GetFilePath(Constants.XmlFiles.Signs);
        if (!File.Exists(signsPath))
        {
            TextAsset bindata = Resources.Load("Signs") as TextAsset;
            if (bindata == null)
                ExceptionImage.SetActive(true);
            else
                File.WriteAllBytes(signsPath, bindata.bytes);
            //ExceptionImage.SetActive(true);
        }

        var charSerializer = new XmlSerializer(typeof(signsContainer));
        var bytes = File.ReadAllBytes(signsPath);
        var charStream = new MemoryStream(bytes);
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
        var sentencesPath = GetFilePath(Constants.XmlFiles.Sentences);
        if (!File.Exists(sentencesPath))
        {
            TextAsset bindata = Resources.Load("Sentences") as TextAsset;
            if (bindata == null)
                ExceptionImage.SetActive(true);
            else
                File.WriteAllBytes(sentencesPath, bindata.bytes);
            //ExceptionImage.SetActive(true);
        }

        
        var charSerializer = new XmlSerializer(typeof(SentencesContainer));
        var bytes = File.ReadAllBytes(sentencesPath);
        var charStream = new MemoryStream(bytes);
        var container = charSerializer.Deserialize(charStream) as SentencesContainer;
        charStream.Close();

        SentencesDatabase = new Dictionary<int, Sentence>();
        foreach (Sentence sentence in container.Sentences)
        {
            SentencesDatabase.Add(sentence.id, sentence);
        }

        SentencesDBLoaded = true;
    }

    private string GetFilePath(string fileName)
    {

        return Application.persistentDataPath + "/" + fileName;

        WWW www = new WWW("fml!");
    #if UNITY_ANDROID//For running in Android
            www = new WWW("jar:file://" + Application.dataPath + "!/assets/" + fileName);       
    #endif
    #if UNITY_EDITOR // For running in Unity
            www = new WWW(Application.streamingAssetsPath + "/" + fileName);
    #endif

        if (www.url == Application.streamingAssetsPath + "/" + fileName)
        {
            ExceptionImage.SetActive(true);
        }

        //while (fuckyou)
        //{
        //    StartCoroutine(GetShitDone(www, fileName));
        //}
        //    if (www.isDone)
        //    {
        //        File.WriteAllBytes(Application.streamingAssetsPath + "/" + fileName, www.bytes);
        //    //ExceptionImage.SetActive(true);
        //    }
        //if (www.error == null)
        //{
        //    ExceptionImage.SetActive(true);
        //}

    }

    //IEnumerator GetShitDone(WWW www, string path)
    //{
    //    yield return www;
    //    File.WriteAllBytes(Application.persistentDataPath + path, www.bytes);
    //    //fuckyou = false;
    //}
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


