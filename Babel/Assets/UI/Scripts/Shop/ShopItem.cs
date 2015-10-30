using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour {

    public Image buyButton;
    public Sprite purchased;
    public Text costText;
    public int cost;
    bool isBought = false;

	// Use this for initialization
	void Start () {
        costText.text = "" + cost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuyThisItem()
    {
        if (isBought)
        {
            Debug.Log("GO TO INVENTORY SCREEN");
        }
        else if(CurrencyControl.currencyAmount >= cost && isBought == false)
        {
            buyButton.sprite = purchased;
            isBought = true;
            Debug.Log("SET ACTIVE IN INVENTORY SCREEN");
            CurrencyControl.currencyAmount = CurrencyControl.currencyAmount - cost;
            PlayerPrefs.SetInt("CurrencyAmount", CurrencyControl.currencyAmount);
        }
        else
        {
            Debug.Log("YOU DONT HAVE SO MUCH MONEY");
        }
    }
}
