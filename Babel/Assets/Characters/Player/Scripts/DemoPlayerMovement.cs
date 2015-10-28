using System.Net;
using Assets.Characters.SideKick.Scripts;
using Assets.Characters.SideKick.Scripts.States;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Characters.Player.Scripts
{
    public class DemoPlayerMovement : MonoBehaviour
    {
        public GameObject SideKick;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit)) return;

                if (hit.transform.gameObject.tag == Constants.Tags.Lever &&
                    Vector3.Distance(transform.position, hit.transform.position) < 2)
                    hit.transform.gameObject.GetComponent<IInteractable>().Interact();
                else
                    GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
    }
}
