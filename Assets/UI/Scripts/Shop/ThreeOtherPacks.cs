using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThreeOtherPacks : MonoBehaviour
{

    public GameObject pack3, pack4, pack5;
    public Image buyButton;
    public Sprite purchased;
    public Text costText;
    public int cost;
    AudioManager am;

    void Start()
    {
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        costText.text = "" + cost;
        IsBought();
    }

    void Update()
    {
        if (PlayerPrefsBool.GetBool("Pack (3)") == true && PlayerPrefsBool.GetBool("Pack (4)") == true && PlayerPrefsBool.GetBool("Pack (5)") == true)
        {
            buyButton.sprite = purchased;
        }
    }

    public void BuyThisItem()
    {
        if (PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) >= cost && (PlayerPrefsBool.GetBool("Pack (3)") == false || PlayerPrefsBool.GetBool("Pack (4)") == false || PlayerPrefsBool.GetBool("Pack (5)") == false))
        {
            PlayerPrefsBool.SetBool("Pack (3)", true);
            PlayerPrefsBool.SetBool("Pack (4)", true);
            PlayerPrefsBool.SetBool("Pack (5)", true);
            PlayerPrefsBool.SetBool("Pack456", true);
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
        if (PlayerPrefsBool.GetBool("Pack (3)") == true && PlayerPrefsBool.GetBool("Pack (4)") == true && PlayerPrefsBool.GetBool("Pack (5)") == true)
        {
            buyButton.sprite = purchased;
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
