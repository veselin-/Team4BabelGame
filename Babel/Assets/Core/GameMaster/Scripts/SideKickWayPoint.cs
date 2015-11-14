using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Characters.Player.Scripts;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using Assets.Core.NavMesh;

public class SideKickWayPoint : MonoBehaviour
{
    [Header("Attributes with a NI at the end is not implemented yet.")]
    public bool IUnderstandMaster = false;

    [Header("Animation")]
    public bool Animate = false;
    public string Animation;

    [Header("New Sign Creation")]
    public bool AddNewSign;
    public int SignId;

    // [Header("Animation")]
    //public bool LookAtPlayer = false;

    [Header("Wait Settings")]
    public bool WaitForPlayer = false;
    public float WaitForPlayerDistance;
    public bool WaitForSeconds = false;
    public float WaitTime;
    public bool WaitForInteraction = false;
    public GameObject Interactable;

    [Header("Player Immobilization Settings")]
    public bool ImmobilizePlayerForSecondsNI = false;
    public float ImmobilizeTimeNI;
    public bool ImmobilizePlayerUntilNextWaypoint = false;

    [Header("Speech Text")]
    public bool UseText = false;

    public int AdvanceSpeechByNSteps;
    public float TimePerStep;

    [Header("Speech Sign")]
    public bool UseSignBubble = false;

    public List<int> DisplaySignId;

    [Header("Interact With World Objects")]
    public bool InteractWithLever = false;


    //[TextArea]
    //public string TextNI;

    private GameObject sidekick;

    private GameObject player;

    private InteractableSpeechBubble speech;

    // Use this for initialization
    void Start () {

        sidekick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
	
        player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);


        speech = GameObject.FindGameObjectWithTag(Constants.Tags.SpeechCanvas).GetComponent<InteractableSpeechBubble>();
        //sidekick.GetComponent<SidekickControls>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
        

	}

   public void EngageWaypoint()
    {
        if(player != null)
        player.GetComponent<PlayerMovement>().enabled = true;

        //if (Animate)
        //{
        //    sidekick.GetComponent<Animator>().SetTrigger(Animation);
        //}

        //if (AddNewSign)
        //{
        //    CreateNewSymbol.SymbolID = SignId;
        //}

        //if (LookAtPlayer)
        //{
        //   sidekick.GetComponent<SidekickControls>().ExecuteAction(19);
        //}

       if (UseText)
       {
           
         //  speech.ActivateSidekickSignBubble(new List<int>(new[] { 8 }));
        }

        if (ImmobilizePlayerUntilNextWaypoint)
       {
           player.GetComponent<PlayerMovement>().enabled = false;
       }

       if (sidekick != null)
       {
           sidekick.GetComponent<AiMovement>()
               .AssignNewState(new GoSomewhereAndWaitState(sidekick.GetComponent<NavMeshAgent>(), transform.position));
            Debug.Log("State assigned");
       }
       StartCoroutine(ExecuteWaypoint());

    }

    IEnumerator ExecuteWaypoint()
    {
        float endTime = 0f;

        yield return new WaitForSeconds(0.5f);

        while (sidekick.GetComponent<NavMeshAgent>().HasReachedTarget() == false)
        {
            Debug.Log("Has reached target: " + sidekick.GetComponent<NavMeshAgent>().HasReachedTarget());
            yield return new WaitForSeconds(0.1f);
        }

        if (InteractWithLever)
        {
            sidekick.GetComponent<SidekickControls>().ExecuteAction(6);
        }

        if (Animate)
        {
            sidekick.GetComponent<Animator>().SetTrigger(Animation);
            yield return new WaitForFixedUpdate();
            //endTime = sidekick.GetComponent<Animator>().get
        }

        if (UseSignBubble)
        {
            speech.ActivateSidekickSignBubble(DisplaySignId);
        }

        if (WaitForInteraction)
        {
            while(!Interactable.GetComponent<IInteractable>().HasBeenActivated())
            {
                yield return new WaitForSeconds(0.2f);
            }
        }



        while (UseText && AdvanceSpeechByNSteps > 0)
        {
            speech.GetNextSpeech();
            AdvanceSpeechByNSteps--;
            yield return new WaitForSeconds(TimePerStep);
        }

        if (AddNewSign)
        {
            GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UiController>().NewSignCreation(SignId);
        }

        if (WaitForPlayer)
        {
            while (Vector3.Distance(player.transform.position, sidekick.transform.position) > WaitForPlayerDistance)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }


        yield return !sidekick.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base." + Animation);
        if (WaitForSeconds)
        {
            yield return new WaitForSeconds(WaitTime);
        }

        sidekick.GetComponent<WaypointSystem>().NextWaypoint();
    }

}
