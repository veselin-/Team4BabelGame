using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Core.GameMaster.Scripts;

public class EndLevelScreen : MonoBehaviour {


	public Text AllOrbsText; 
	private int allOrbs, foundOrbs, shopOpen;
	public Text FoundOrbsText;
	public Text LevelCompleteText;
	public string NextLevel;
	private EndPoints ep;
    public GameObject blackness;
    private GameObject pokesprite, pokedex;
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

    IEnumerator CloseShop()
    {
        Debug.Log("RUNNING ANIMATION");
        yield return new WaitForEndOfFrame();
        yield return !pokedex.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PokeDexFullyExit");
        Debug.Log("DONE WITH RUNNING ANIMATION");
        pokesprite.SetActive(false);
        blackness.SetActive(false);
        shopOpen = 0;
    }

    public void ShowShop()
    {
        if (shopOpen == 1)
        {
            pokedex.GetComponent<Animator>().SetTrigger("MenuExit");
            StartCoroutine(CloseShop());
        }
        else
        {
            blackness.SetActive(true);
            pokesprite.SetActive(true);
            pokedex.GetComponent<UiController>().OpenShop();
            shopOpen = 1;
        }
    }

	public void MainMenuBtn()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void NextLevelBtn()
	{
		Application.LoadLevel (NextLevel);
	}

	public void UpdateOrbsUI()
	{
		AllOrbsText.text = "0"; //"" + PlayerPrefs.GetInt ("CurrencyAmount").ToString();
		FoundOrbsText.text = "0"; // + ep.orbs.ToString();
		LevelCompleteText.text = GetLevelNumber() + "\n Complete";

		StartCoroutine (AnimateOrbs());
	}

	string GetLevelNumber()
	{
		string level = Application.loadedLevelName;
		if (level.Equals ("Tutorial1Beta")) {
			return "Level 1";
		} else if (level.Equals ("Tutorial2Beta")) {
			return "Level 2";
		} else if (level.Equals ("Tutorial3Beta")) {
			return "Level 3";
		} else if (level.Equals ("Tutorial4Beta")) {
			return "Level 6";
		} else if (level.Equals ("Level1Beta")) {
			return "Level 4";
		} else if (level.Equals ("Level2Beta")) {
			return "Level 5";
		} else if (level.Equals ("Level3Beta")) {
			return "Level 7";
		} else {
			return "Level";
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
		foundOrbs = ep.orbs;
        pokesprite.SetActive(false);
        UpdateOrbsUI();
    }

}
