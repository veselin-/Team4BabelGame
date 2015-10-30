using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Characters.Player.Scripts
{
    public class PlayerMovement : MonoBehaviour
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
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);

            IState state = null;

            foreach (var hit in hits)
            {
                state = GameObjectToState(hit.transform.gameObject, hit);
                if(state != null) break;
            }

            if (state != null)
                _ai.AssignNewState(state);
        }

        IState GameObjectToState(GameObject other, RaycastHit hit)
        {
            Debug.Log(other.tag);
            if (other.GetComponent<IInteractable>() != null)
            {
               return new InteractWithNearestState(_agent, hit.transform.gameObject);

            }
            if (other.GetComponent<ICollectable>() != null)
            {
                return new PickupItemState(_agent, hit.transform.gameObject);
            }
            if(other.tag == Constants.Tags.Floor)
            {
                return new GoSomewhereAndWaitState(_agent, hit.point);
            }
            return null;
        }
    }
}
