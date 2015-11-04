using UnityEngine;
using System.Xml;
using System.IO;
using System;

public class LanguageManager : ScriptableObject
{
    #region Singleton
    private static LanguageManager instance = null;

    public static LanguageManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateInstance<LanguageManager>();
            }
            return instance;
        }
    }
    #endregion

    private XmlDocument mainDoc = null;
    private XmlElement root = null;

    private string languagePath = string.Empty;
    private string[] languageFiles = null;

    void Awake()
    {
        //languagePath = Application.dataPath + "/Core/Languages/";
        //CollectLanguages();
    }

    void OnDestroy()
    {
        mainDoc = null;
        root = null;
    }

	/*
    private void CollectLanguages()
    {
        try
        {
            DirectoryInfo langDir = new DirectoryInfo(languagePath);
            FileInfo[] files = langDir.GetFiles("*.xml");
            languageFiles = new string[files.Length];
            int i = 0;
            foreach (FileInfo fileGo in files)
            {
                languageFiles[i] = fileGo.FullName;
                i++;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
	*/

	/*
    private string GetLanguageFile(string language)
    {
        foreach (string langGo in languageFiles)
        {
            if (langGo.EndsWith(language + ".xml"))
            {
                return langGo;
            }
        }
        return string.Empty;
    }
	*/

    public void LoadLanguage(string language)
    {
        try
        {
			TextAsset textAsset = (TextAsset)Resources.Load(language, typeof(TextAsset));
            //string loadFile = GetLanguageFile(language);
            mainDoc = new XmlDocument();
            //StreamReader reader = new StreamReader(loadFile);
            mainDoc.LoadXml(textAsset.text);
            root = mainDoc.DocumentElement;
            //reader.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public string Get(string path)
    {
        try
        {
            XmlNode node = root.SelectSingleNode(path);
            if (node == null)
            {
                return path;
            }
            else
            {
                string value = node.InnerText;
                value = value.Replace("\\n", "\n");
                return value;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return path;
        }
    }
}