using UnityEngine;
using System.Collections;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Characters.Player.Scripts;
using Assets.Core.Configuration;
using Assets.Core.NavMesh;
using Assets.Core.GameMaster.Scripts;

public class hitBoxNewSign : MonoBehaviour
{

    public int ID;
    private NavMeshAgent _sideKickAgent;
    public PlayerInteract _playerInteract;
    //private AiMovement _playerMovement;
    NavMeshAgent _player;
    private UiController _uiControl;
    //public InteractableSpeechBubble speech;

    // Use this for initialization
    void Start()
    {
        _uiControl = GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UiController>();
        _sideKickAgent = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick).GetComponent<NavMeshAgent>();
        //_playerMovement = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<AiMovement>();
        _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerInteract._targetIsNotHit = true;

            _player.Stop();
            _uiControl.NewSignCreation(ID);
            _playerInteract.ChangePlayerInteractState(true);
            _sideKickAgent.Resume();
        //    Debug.Log("IM IN HERE");
            //_playerMovement.MovementSpeed = 0;
        }
    }
}
