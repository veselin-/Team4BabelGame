using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyControl : MonoBehaviour {

    public static int addMoney = 0;
    int currencyAmount;
    public int addMoneyTest;
    public Text amountText;
	// Use this for initialization
    void Awake()
    {
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
    }

	void Start () {
        amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);
	}
	
	// Update is called once per frame
	void Update () {
        currencyAmount += addMoney;
    }

    public void EarnMoney()
    {
        currencyAmount += addMoneyTest;
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
        amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);

    }

    public void SpendMoney()
    {
        if (PlayerPrefs.GetInt("CurrencyAmount", currencyAmount) <= 0)
        {
            Debug.Log("YOU HAVE TO EARN MORE MONEY BITCH!");
        }
        else
        {
            currencyAmount -= addMoneyTest;
            PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
            amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);
        }
    }
}
