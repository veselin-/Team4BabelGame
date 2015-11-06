using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.AiScripts.ScriptedBehaviours
{
    public class Toturial01Behaviour : MonoBehaviour
    {
        private State state;
        private bool waitingForPlayer;
        private float movementSpeed;
        private NavMeshAgent _agent;

        // Use this for initialization
        void Start ()
        {
            var colider = gameObject.AddComponent<SphereCollider>();
            colider.radius = 5;            colider.isTrigger = true;            _agent = GetComponent<NavMeshAgent>();            GetComponent<SidekickControls>().enabled = false;
            var aiMovement = GetComponent<AiMovement>();
            aiMovement.StrollSpeed = 0;            movementSpeed = aiMovement.MovementSpeed;
            _agent.destination = _agent.transform.position;
            waitingForPlayer = true;        }
	
        // Update is called once per frame
        void Update () {
            _agent.speed = movementSpeed;
                        if (state == State.First && Vector3.Distance(_agent.transform.position, new Vector3(17, 1, 17)) < 2)            {
                // TODO: waving animation
                // TODO: Say symbol: 8
                Debug.Log("Say symbol: 8");
                waitingForPlayer = true;
                state = State.Second;            } else if (state == State.Second && Vector3.Distance(_agent.transform.position, new Vector3(35, 1, -8)) < 2)            {                // TODO: waving animation
                // TODO: Say symbol: 8
                Debug.Log("Say symbol: 8");                state = State.Thrid;                waitingForPlayer = true;            }        }
                void OnTriggerStay(Collider other)        {
            if(other.tag != Constants.Tags.Player || !waitingForPlayer) return;            waitingForPlayer = false;            switch (state)            {
                case State.First:
                    // TODO: waving animation
                    CreateNewSymbol.SymbolID = 8;
                    GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UIControl>().SignCreationEnter();
                    _agent.destination = new Vector3(17,1,17);                    _agent.speed = movementSpeed;
                    break;
                case State.Second:
                    _agent.destination = new Vector3(35, 1, -8);                    _agent.speed = movementSpeed;
                    break;
                case State.Thrid:
                    if (Vector3.Distance(_agent.transform.position, new Vector3(35, 1, -8)) > 2) return;
                    Debug.Log("Say symbol: 8");
                    // TODO: waving animation
                    // TODO: Say symbol: 8
                    break;
            }
        }
                enum State        {
            First, Second, Thrid        }
    }
}

