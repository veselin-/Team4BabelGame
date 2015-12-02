using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyControl : MonoBehaviour {

    public static int currencyAmount;
    public Text amountText;
    AudioManager am;

	void Start () {
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
	
	void Update () {
        amountText.text = "" + PlayerPrefs.GetInt("CurrencyAmount", currencyAmount);
    }

    public void EarnCurrency(int earned)
    {
        PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", currencyAmount) + earned);
        am.ClickBtnPlay();
    }

    public void OpenInventory(GameObject can)
    {
        can.SetActive(true);
    }
}
