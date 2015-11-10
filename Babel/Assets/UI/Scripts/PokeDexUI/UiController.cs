using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    private Animator anim;

    public ScrollRect scrollRect;

    // Use this for initialization
    void Start () {



        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewSignCreation(int id)
    {
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        anim.SetTrigger("CreationToggle");

    }
    public void SignCreationDone()
    {
        anim.SetTrigger("CreationToggle");

    }

}
