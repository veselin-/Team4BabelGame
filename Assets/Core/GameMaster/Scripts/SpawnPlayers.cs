using System.Collections;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.ScriptedBehaviours;
using Assets.Characters.AiScripts.States;
using Assets.Characters.Player.Scripts;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Core.GameMaster.Scripts
{
    public class SpawnPlayers : MonoBehaviour
    {
        public GameObject CharactorPrefab, SideKickPrefab;

        public Transform PlayerSpawnPoint;
        public Transform SidekickSpawnPoint;

        public Transform goal;

        private GameObject sidekick;
        private GameObject player;
        private int i = 0;

        /// <summary>
        /// This one should only be used, if the level recuires a scripted behaviour
        /// </summary
        public ScriptedBehaviour Behaviour;

        // Use this for initialization
        void Awake ()
        {
            sidekick = (GameObject) Instantiate(SideKickPrefab, SidekickSpawnPoint.position, SidekickSpawnPoint.rotation);
            player = (GameObject) Instantiate(CharactorPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);

            sidekick.tag = Constants.Tags.SideKick;
            sidekick.name = Constants.Tags.SideKick;
            sidekick.GetComponent<PlayerMovement>().enabled = false;

            sidekick.GetComponent<AiMovement>().StrollSpeed = 0f;
            sidekick.GetComponent<AiMovement>().TimeBeforeStolling = 0;
            sidekick.GetComponent<NavMeshAgent>().avoidancePriority = 1;

            player.GetComponent<PlayerMovement>().enabled = false;
            player.name = Constants.Tags.Player;
            player.GetComponent<NavMeshAgent>().avoidancePriority = 2;

            StartCoroutine(test());
        }

        void Update()
        {
            //Application.CaptureScreenshot("./pics/" + i++ + ".png", 2);
        }


        IEnumerator test()
        {

            yield return new WaitForSeconds(1);

            if (goal != null)
            {
                var s1 = new GoSomewhereAndWaitState(sidekick.GetComponent<NavMeshAgent>(), goal.position);
                var s2 = new GoSomewhereAndWaitState(player.GetComponent<NavMeshAgent>(), goal.position);

                sidekick.GetComponent<AiMovement>().AssignNewState(s1);
                player.GetComponent<AiMovement>().AssignNewState(s2);
            }

            if (Application.loadedLevelName == "Level1Beta")
            {
                yield return new WaitForSeconds(8);
                Camera.main.GetComponent<Animator>().SetTrigger("What");
                yield return new WaitForSeconds(6);
                Application.LoadLevel("Tutorial1Beta");
                yield break;
            }


            if (Application.loadedLevelName == "Tutorial1Beta")
            {
                yield return new WaitForSeconds(8);
                Application.LoadLevel("Level2Beta");
                yield break;
            }

            if (Application.loadedLevelName == "Level2Beta")
            {
                sidekick.GetComponent<AiMovement>().MovementSpeed = 0.7f;
                player.GetComponent<AiMovement>().MovementSpeed = 0.7f;
                player.GetComponentInChildren<Light>().enabled = false;
                sidekick.GetComponentInChildren<Light>().enabled = false;
                yield return new WaitForSeconds(10);
                Application.LoadLevel("Tutorial2Beta");
                yield break;
            }


            if (Application.loadedLevelName == "Tutorial2Beta")
            {
                yield return new WaitForSeconds(8);
                Application.LoadLevel("Tutorial3Beta");
                yield break;
            }

            if (Application.loadedLevelName == "Tutorial3Beta")
            {
                sidekick.GetComponent<AiMovement>().MovementSpeed = 0.7f;
                player.GetComponent<AiMovement>().MovementSpeed = 0.7f;

                yield return new WaitForSeconds(8);
                Application.Quit();

            }




        }

        public enum ScriptedBehaviour
        {
            None, Toturial1, Toturial2, WaypointSystem
        }
    }
}
