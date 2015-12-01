using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyControl : MonoBehaviour {

    public static int currencyAmount;
    public Text amountText;

	void Start () {
        //PlayerPrefsBool.SetBool("Pack", false);
        //PlayerPrefsBool.SetBool("Pack (1)", false);
        //PlayerPrefsBool.SetBool("Pack (2)", false);
        //PlayerPrefsBool.SetBool("Pack (3)", false);
        //PlayerPrefsBool.SetBool("Pack (4)", false);
        //PlayerPrefsBool.SetBool("Pack (5)", false);
        //PlayerPrefsBool.SetBool("Pack123", false);
        //PlayerPrefsBool.SetBool("Pack456", false);
        //PlayerPrefsBool.SetBool("PackAll", false);
    }
	
	void Update () {
        amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);
    }

    public void EarnCurrency(int earned)
    {
        PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", currencyAmount) + earned);
    }

    public void OpenInventory(GameObject can)
    {
        can.SetActive(true);
    }
}
