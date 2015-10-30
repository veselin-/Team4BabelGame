using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyControl : MonoBehaviour {

    //public static int addMoney = 0;
    public static int currencyAmount;
    //public int addMoneyTest;
    public Text amountText;
	// Use this for initialization
    void Awake()
    {
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //currencyAmount += addMoney;
        amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);
    }

    public void EarnCurrency(int earned)
    {
        currencyAmount += earned;
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
        //amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);

    }

    public void ButtonForBuying(int cost)
    {
        if (PlayerPrefs.GetInt("CurrencyAmount", currencyAmount) - cost < 0)
        {
            Debug.Log("YOU NEED " + cost + " (currency) TO BUY THIS");
        }
        else
        {
            currencyAmount -= cost;
            PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
            //amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);
        }
    }
}
