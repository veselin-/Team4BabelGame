using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.ScriptedBehaviours;
using Assets.Characters.Player.Scripts;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Core.GameMaster.Scripts
{
    public class SpawnPlayers : MonoBehaviour
    {
        public GameObject CharactorPrefab;

        public Transform PlayerSpawnPoint;
        public Transform SidekickSpawnPoint;


        /// <summary>
        /// This one should only be used, if the level recuires a scripted behaviour
        /// </summary
        public ScriptedBehaviour Behaviour;

        // Use this for initialization
        void Awake ()
        {
            var sidekick = (GameObject) Instantiate(CharactorPrefab, SidekickSpawnPoint.position, Quaternion.identity);
            var player = (GameObject) Instantiate(CharactorPrefab, PlayerSpawnPoint.position, Quaternion.identity);

            sidekick.tag = Constants.Tags.SideKick;
            sidekick.name = Constants.Tags.SideKick;
            sidekick.GetComponent<PlayerMovement>().enabled = false;

            sidekick.GetComponent<AiMovement>().StrollSpeed = 1.5f;
            sidekick.GetComponent<AiMovement>().TimeBeforeStolling = 5;
            
            player.GetComponent<SidekickControls>().enabled = false;
            player.name = Constants.Tags.Player;
            player.GetComponent<Renderer>().material.color = Color.green;

            switch (Behaviour)
            {
                case ScriptedBehaviour.Toturial1:
                    sidekick.AddComponent<Toturial01Behaviour>();
                    break;
                case ScriptedBehaviour.Toturial2:
                    sidekick.AddComponent<Toturial02Behaviour>();
                    break;
            }
        }

        public enum ScriptedBehaviour
        {
            None, Toturial1, Toturial2
        }
    }
}
