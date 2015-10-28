using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Characters.Player.Scripts
{
    public class DemoPlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private AiMovement _ai;

        // Use this for initialization
        void Start ()
        {
            _agent = GetComponent<NavMeshAgent>();
            _ai = GetComponent<AiMovement>();
        }
	
        // Update is called once per frame
        void Update () {
            if (!Input.GetMouseButtonDown(0)) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) return;
            var other = hit.transform.gameObject;

            IState state;

            if (other.GetComponent<IInteractable>() != null)
            {
                state = new InteractWithNearestState(_agent, hit.transform.gameObject);
                
            } else if (other.GetComponent<ICollectable>() != null)
            {
                state = new PickupItemState(_agent, hit.transform.gameObject);
            }
            else
            {
                state = new GoSomewhereAndWaitState(_agent, hit.point);
            }

            _ai.AssignNewState(state);
        }
    }
}
