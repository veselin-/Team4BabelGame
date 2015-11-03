﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyControl : MonoBehaviour {

    public static int currencyAmount;
    public Text amountText;

	void Start () {
        //PlayerPrefs.SetInt("CurrencyAmount", currencyAmount * 0);
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
        can.transform.position = new Vector3(0,0,0);
    }
}
