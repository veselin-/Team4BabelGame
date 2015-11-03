using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour{

    public GameObject inventoryCan, item;
    public Image buyButton, itemImg;
    public Sprite purchased;
    public Text costText;
    public int cost;

	void Start () {
        costText.text = "" + cost;
        //PlayerPrefsBool.SetBool(item.name, false);
        IsBought();
    }

    public void BuyThisItem()
    {
        if (PlayerPrefsBool.GetBool(item.name) == true)
        {
            inventoryCan.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) >= cost && PlayerPrefsBool.GetBool(item.name) == false)
        {
            PlayerPrefsBool.SetBool(item.name, true);
            IsBought();
            PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) - cost);
        }
        else
        {
            Debug.Log("YOU DONT HAVE SO MUCH MONEY");
        }
    }

    void IsBought()
    {
        if (PlayerPrefsBool.GetBool(item.name) == true)
        {
            buyButton.sprite = purchased;
            item.SetActive(true);
        }
    }
}

public class PlayerPrefsBool
{
    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }
}
