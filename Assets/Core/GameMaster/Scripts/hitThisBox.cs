using UnityEngine;
using System.Collections;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Characters.Player.Scripts;
using Assets.Core.Configuration;
using Assets.Core.NavMesh;
using Assets.Core.GameMaster.Scripts;

public class hitThisBox : MonoBehaviour {

    private NavMeshAgent _sideKickAgent;
    public PlayerInteract _playerInteract;
    //private AiMovement _playerMovement;
    NavMeshAgent _player;
    //public InteractableSpeechBubble speech;

    // Use this for initialization
    void Start () {
        _sideKickAgent = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick).GetComponent<NavMeshAgent>();
        //_playerMovement = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<AiMovement>();
        _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            PlayerInteract._targetIsNotHit = true;
            _sideKickAgent.Resume();
            _player.Stop();
            _playerInteract.ChangePlayerInteractState(false);
            Debug.Log("IM IN HERE");
            //_playerMovement.MovementSpeed = 0;
        }
    }
}
