using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour{

    public GameObject packOfObjects;
    public Image buyButton;
    //itemImg;
    public Sprite purchased;
    public Text costText;
    public int cost;
    AudioManager am;

	void Start () {
        costText.text = "" + cost;
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        IsBought();
    }

    void Update()
    {
        if (PlayerPrefsBool.GetBool(packOfObjects.name) == true)
        {
            buyButton.sprite = purchased;
        }
    }

    public void BuyThisItem()
    {
        if(PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) >= cost && PlayerPrefsBool.GetBool(packOfObjects.name) == false)
        {
            PlayerPrefsBool.SetBool(packOfObjects.name, true);
            am.ClickBtnPlay();
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
        if (PlayerPrefsBool.GetBool(packOfObjects.name) == true)
        {
            buyButton.sprite = purchased;
            APack(packOfObjects);
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
