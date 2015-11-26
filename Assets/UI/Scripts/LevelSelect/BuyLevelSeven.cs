using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;
using Assets.Core.LevelSelector;

public class BuyLevelSeven : MonoBehaviour {
    public GameObject ls;
	// Use this for initialization
	void Start () {
        //wh = GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DoYouWannaBuy()
    {
        GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().CreateConfirmDialog("Phrases/LevelSeven", "Phrases/BuyLevel", "Phrases/BuyText", "Phrases/BackText", UnlockLevelSeven, null);
    }

    void UnlockLevelSeven()
    {
        if (PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) >= 20 && PlayerPrefsBool.GetBool("Level7") == false)
        {
            PlayerPrefsBool.SetBool("Level7", true);
            ls.GetComponent<LevelSelector>().UpdateLevels();
            Userlevels.GetInstance().AddUserLevel("7");
            PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) - 20);
        }

    }
}
