using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class WaypointAction : MonoBehaviour
{

    public InteractableSpeechBubble speech;
    public PlayerInteract _playerInteract;
    public int ActionToPerform;

    // Use this for initialization
    void Start ()
	{
	    //_playerInteract = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).GetComponent<PlayerInteract>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SideKickPerformAction()
    {

        if (_playerInteract != null)
        {
            switch (ActionToPerform)
            {
                case 0:
                    _playerInteract.ChangePlayerInteractState(true);
                    speech.GetNextSpeech();
                    break;
                default:
                    Debug.Log("None");
                    break;
            }
        }

    }

}
