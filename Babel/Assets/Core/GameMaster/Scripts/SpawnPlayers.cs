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
        
        // Use this for initialization
        void Start ()
        {
            var sidekick = Instantiate(CharactorPrefab);
            var player = Instantiate(CharactorPrefab);

            sidekick.transform.position = SidekickSpawnPoint.position;
            player.transform.position = PlayerSpawnPoint.position;

            sidekick.tag = Constants.Tags.SideKick;
            sidekick.name = Constants.Tags.SideKick;
            sidekick.GetComponent<PlayerMovement>().enabled = false;

            player.GetComponent<SidekickControls>().enabled = false;
        }

        // Update is called once per frame
        void Update () {
	
        }
    }
}
