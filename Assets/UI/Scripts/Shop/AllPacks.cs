using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AllPacks : MonoBehaviour
{
    public GameObject pack, pack1, pack2, pack3, pack4, pack5;
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
        if(PlayerPrefsBool.GetBool("Pack") == true)
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
        if (PlayerPrefsBool.GetBool("Pac (3)") == true)
        {
            APack(pack3);
        }
        if (PlayerPrefsBool.GetBool("Pack (4)") == true)
        {
            APack(pack4);
        }
        if (PlayerPrefsBool.GetBool("Pack (5)") == true)
        {
            APack(pack5);
        }
    }

    public void BuyThisItem()
    {
        //if (PlayerPrefsBool.GetBool("PackAll") == true)
        //{
        //    APack(pack);
        //    APack(pack1);
        //    APack(pack2);
        //    APack(pack3);
        //    APack(pack4);
        //    APack(pack5);
        //}
        //else 
      //  Debug.Log("FIX SÅ DEN HAKKER SINGLE PACKS AF");
        if (PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) >= cost && PlayerPrefsBool.GetBool("Pack") == false || PlayerPrefsBool.GetBool("Pack (1)") == false || PlayerPrefsBool.GetBool("Pack (2)") == false || PlayerPrefsBool.GetBool("Pack (3)") == false || PlayerPrefsBool.GetBool("Pack (4)") == false || PlayerPrefsBool.GetBool("Pack (5)") == false)
        {
            PlayerPrefsBool.SetBool("Pack", true);
            PlayerPrefsBool.SetBool("Pack (1)", true);
            PlayerPrefsBool.SetBool("Pack (2)", true);
            PlayerPrefsBool.SetBool("Pack (3)", true);
            PlayerPrefsBool.SetBool("Pack (4)", true);
            PlayerPrefsBool.SetBool("Pack (5)", true);
            IsBought();
            PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) - cost);
        }
        else
        {
    //        Debug.Log("YOU DONT HAVE SO MUCH MONEY");
        }
    }

    void IsBought()
    {
        if (PlayerPrefsBool.GetBool("Pack") == true && PlayerPrefsBool.GetBool("Pack (1)") == true && PlayerPrefsBool.GetBool("Pack (2)") == true && PlayerPrefsBool.GetBool("Pack (3)") == true && PlayerPrefsBool.GetBool("Pack (4)") == true && PlayerPrefsBool.GetBool("Pack (5)") == true)
        {
            buyButton.sprite = purchased;
            APack(pack);
            APack(pack1);
            APack(pack2);
            APack(pack3);
            APack(pack4);
            APack(pack5);
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
