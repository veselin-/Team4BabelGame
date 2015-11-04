using System.Collections;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Core.GameMaster.Scripts
{
    public class EndPoints : MonoBehaviour
    {

        public GameObject[] Endpoints;
        public string NextLevelName;

        private bool isSidekickHere;
        private bool isPlayerHere;

        // Use this for initialization
        void Start ()
        {
            StartCoroutine(ShouldRoomChange());
        }

        IEnumerator ShouldRoomChange()
        {
            while (!(isPlayerHere && isSidekickHere))
            {
                yield return new WaitForSeconds(0.5f);
            }
            Application.LoadLevel(NextLevelName);
        }


        void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case Constants.Tags.Player:
                    var sk = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
                    sk.GetComponent<AiMovement>().AssignNewState(new EndGameState(sk.GetComponent<NavMeshAgent>()));
                    isPlayerHere = true;
                    break;
                case Constants.Tags.SideKick:
                    isSidekickHere = true;
                    break;
            }
        }

        void OnTriggerExit(Collider other)
        {
            isSidekickHere = other.tag != Constants.Tags.SideKick && isSidekickHere;
            isPlayerHere = other.tag != Constants.Tags.Player && isPlayerHere;
        }
    }
}
