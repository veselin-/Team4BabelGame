using UnityEngine;
using System.Collections;
//using UnityEditor.AnimatedValues;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private Animator anim;
    public ScrollRect scrollRect;
    public Animator bookAnim;
    NavMeshAgent navMeshP, navMeshS;
	private AudioManager _audioManager;

    // Use this for initialization
    void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager> ().GetComponent<AudioManager> ();
        navMeshP = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        navMeshS = GameObject.FindGameObjectWithTag("SideKick").GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewSignCreation(int id)
    {
        CreateNewSymbol.SymbolID = id;
        scrollRect.horizontalNormalizedPosition = 0f;
        anim.SetTrigger("MenuToggle");
        anim.SetBool("CreatingSign", true);
        bookAnim.SetBool("CreatingSign", true);
        bookAnim.SetTrigger("CreationToggle");
        navMeshP.Stop();
        navMeshS.Stop();
    }

    public void SignCreationDone()
    {
        anim.SetBool("CreatingSign", false);
        bookAnim.SetBool("CreatingSign", false);
        bookAnim.SetTrigger("CreationToggle");
        anim.SetTrigger("MenuToggle");
        navMeshP.ResetPath();
        navMeshS.ResetPath();
    }

    public void PokedexOpen()
    {
		_audioManager.PokedexBtnPlay ();
        navMeshP.Stop();
        navMeshS.Stop();
    }

    public void PokedexClose()
    {
		_audioManager.PokedexBtnPlay ();
        navMeshP.ResetPath();
        navMeshS.ResetPath();
    }
}
