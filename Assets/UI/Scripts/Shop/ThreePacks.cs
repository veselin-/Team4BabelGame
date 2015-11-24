using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThreePacks : MonoBehaviour {
    public GameObject pack, pack1, pack2;
    public Image buyButton;
    //itemImg;
    public Sprite purchased;
    public Text costText;
    public int cost;

    void Start()
    {
        costText.text = "" + cost;
        IsBought();
    }

    void Update()
    {
        if (PlayerPrefsBool.GetBool("Pack") == true)
        {
            APack(pack);
        }
        if (PlayerPrefsBool.GetBool("Pack (1)") == true)
        {
            APack(pack1);
        }
        if (PlayerPrefsBool.GetBool("Pack (2)") == true)
        {
            APack(pack2);
        }
    }

    public void BuyThisItem()
    {
        Debug.Log("FIX SÅ DEN HAKKER SINGLE PACKS AF");
        //if (PlayerPrefsBool.GetBool("Pack123") == true)
        //{
        //    APack(pack);
        //    APack(pack1);
        //    APack(pack2);
        //}
        //else 
        if (PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) >= cost && PlayerPrefsBool.GetBool("Pack") == false || PlayerPrefsBool.GetBool("Pack (1)") == false || PlayerPrefsBool.GetBool("Pack (2)") == false)
        {
            PlayerPrefsBool.SetBool("Pack", true);
            PlayerPrefsBool.SetBool("Pack (1)", true);
            PlayerPrefsBool.SetBool("Pack (2)", true);
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
        if (PlayerPrefsBool.GetBool("Pack") == true && PlayerPrefsBool.GetBool("Pack (1)") == true && PlayerPrefsBool.GetBool("Pack (2)") == true)
        {
            buyButton.sprite = purchased;
            APack(pack);
            APack(pack1);
            APack(pack2);
        }
    }

    void APack(GameObject pack)
    {
        pack.transform.GetChild(0).GetComponent<Text>().enabled = false;
        pack.transform.GetChild(1).gameObject.SetActive(true);
        pack.transform.GetChild(2).gameObject.SetActive(true);
        pack.transform.GetChild(3).gameObject.SetActive(true);
        pack.transform.GetChild(4).gameObject.SetActive(true);
    }
}

