using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour {

    public GameObject inventoryCan, item;
    public Image buyButton, itemImg;
    public Sprite purchased;
    public Text costText;
    public int cost;

    //private Image itemImage;

	// Use this for initialization
	void Start () {
        costText.text = "" + cost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuyThisItem()
    {
        if (item.activeSelf == true)
        {
            inventoryCan.SetActive(true);
        }
        else if(CurrencyControl.currencyAmount >= cost)
        {
            buyButton.sprite = purchased;
            item.SetActive(true);
            //itemImg.gameObject.SetActive(true);
            CurrencyControl.currencyAmount = CurrencyControl.currencyAmount - cost;
            PlayerPrefs.SetInt("CurrencyAmount", CurrencyControl.currencyAmount);
        }
        else
        {
            Debug.Log("YOU DONT HAVE SO MUCH MONEY");
        }
    }
}
