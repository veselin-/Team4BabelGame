using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using Assets.Core.Configuration;
using Assets.Core.GameMaster.Scripts;

public class EndLevelScreen : MonoBehaviour {


	public Text AllOrbsText; 
	private int allOrbs, foundOrbs, shopOpen;
	public Text FoundOrbsText;
	public Text LevelCompleteText;
    public Text shopBut;
	public string NextLevel;
	private EndPoints ep;
    public GameObject blackness;
    private GameObject pokesprite, pokedex, closeuimask;
    bool waitPLEASE = false;

    // Use this for initialization
    void Awake () {

		AllOrbsText.text = "";
		FoundOrbsText.text = "";
		LevelCompleteText.text = "";
	}
	
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update () {

    }

	public void ShowEndLevelScreen()
	{
		gameObject.SetActive (true);
	}

    IEnumerator waitForAnim()
    {
        waitPLEASE = true;
        while (!pokedex.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PokeDexFullyExit"))
        {
            yield return new WaitForEndOfFrame();
        }
        shopBut.text = LanguageManager.Instance.Get("Phrases/Shop");
        blackness.SetActive(false);
        pokesprite.SetActive(false);
        shopOpen = 0;
        Debug.Log(shopOpen);
        waitPLEASE = false;
    }

    public void ShowShop()
    {
        if (shopOpen == 1)
        {
            if (waitPLEASE)
            {
                return;
            }
            pokedex.GetComponent<Animator>().SetTrigger("MenuExit");
            StartCoroutine(waitForAnim());
        }
        else if (shopOpen == 0)
        {
            blackness.SetActive(true);
            pokesprite.SetActive(true);
            pokedex.GetComponent<UiController>().OpenShop();
            shopBut.text = LanguageManager.Instance.Get("Phrases/BackText");
            shopOpen = 1;
            Debug.Log(shopOpen);
        }
    }

	public void MainMenuBtn()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void NextLevelBtn()
	{
	    if (Application.loadedLevelName == "Level3Beta")
	    {
	        GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().CreateInfoDialog("Phrases/LastLevelHeader", "Phrases/LastLevelInfo", "Phrases/LastLevelButton", MainMenuBtn);
            return;
	    }

        if(NextLevel == "Level3Beta" && !PlayerPrefsBool.GetBool("Level7")) {
            var bls = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).GetComponent<BuyLevelSeven>();
            bls.DoYouWannaBuy();
        }
        else
        {
            Application.LoadLevel(NextLevel);
        }

    }



	public void UpdateOrbsUI()
	{
		AllOrbsText.text = "0"; //"" + PlayerPrefs.GetInt ("CurrencyAmount").ToString();
		FoundOrbsText.text = "0"; // + ep.orbs.ToString();
		LevelCompleteText.text = GetLevelNumber() + "\n" + LanguageManager.Instance.Get("Phrases/Complete");;

		StartCoroutine (AnimateOrbs());
	}

	string GetLevelNumber()
	{
		string level = Application.loadedLevelName;
		if (level.Equals ("Tutorial1Beta")) {
			return LanguageManager.Instance.Get("Phrases/Level") + " 1";
		} else if (level.Equals ("Tutorial2Beta")) {
            return LanguageManager.Instance.Get("Phrases/Level") + " 2";
		} else if (level.Equals ("Tutorial3Beta")) {
            return LanguageManager.Instance.Get("Phrases/Level") + " 3";
		} else if (level.Equals ("Tutorial4Beta")) {
            return LanguageManager.Instance.Get("Phrases/Level") + " 6";
		} else if (level.Equals ("Level1Beta")) {
            return LanguageManager.Instance.Get("Phrases/Level") + " 4";
		} else if (level.Equals ("Level2Beta")) {
            return LanguageManager.Instance.Get("Phrases/Level") + " 5";
		} else if (level.Equals ("Level3Beta")) {
            return LanguageManager.Instance.Get("Phrases/Level") + " 7";
		} else {
            return LanguageManager.Instance.Get("Phrases/Level");
        }
    }
    
	IEnumerator AnimateOrbs()
	{
		yield return new WaitForSeconds(1f);

		int numbers = 0;
		while(numbers < foundOrbs)
		{
			yield return new WaitForSeconds(0.01f);
			numbers++;
			FoundOrbsText.text = "" + numbers.ToString();
		}

		yield return new WaitForSeconds(0.5f);

		numbers = 0;
		while(numbers < allOrbs)
		{
			yield return new WaitForSeconds(0.001f);
			numbers++;
			AllOrbsText.text = "" + numbers.ToString();
		}
	}

    void OnEnable()
    {
        pokesprite = GameObject.FindGameObjectWithTag("PokedexSprite");
        pokedex = GameObject.FindGameObjectWithTag("GameUI");
        ep = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<EndPoints>();	
		allOrbs = PlayerPrefs.GetInt ("CurrencyAmount");
        shopBut.text = LanguageManager.Instance.Get("Phrases/Shop");
        foundOrbs = ep.orbs;
        pokesprite.SetActive(false);
        UpdateOrbsUI();
    }

}
