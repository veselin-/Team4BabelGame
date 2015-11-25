using System.Collections;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using Assets.Core.LevelSelector;
using UnityEngine;

namespace Assets.Core.GameMaster.Scripts
{
    public class EndPoints : MonoBehaviour
    {

        public GameObject[] Endpoints;
        public string NextLevelName;
        public string GrandAccesToLevelId;
		public GameObject endLevel;
        private bool isSidekickHere;
        private bool isPlayerHere;

		[HideInInspector]
		public int orbs = 0;
        // Use this for initialization
        void Start ()
        {
			orbs = 0;
			Time.timeScale = 1f;
            StartCoroutine(ShouldRoomChange());
        }

        IEnumerator ShouldRoomChange()
        {
            //while (!(isPlayerHere && isSidekickHere))
            while (!(isPlayerHere && isSidekickHere))
            {
                yield return new WaitForSeconds(0.5f);
              
            }
            Debug.Log("room change");
            Userlevels.GetInstance().AddUserLevel(GrandAccesToLevelId);
            endLevel.SetActive(true);
			endLevel.GetComponent<EndLevelScreen> ().NextLevel = NextLevelName;
			if(GetComponent<AudioSource> () != null)
			
				GetComponent<AudioSource> ().Play();
            
        }


        void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {

                case Constants.Tags.Player:
                    if(isPlayerHere)
                        return;
                    isPlayerHere = true;
                    var sk = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
                    sk.GetComponent<AiMovement>().AssignNewState(new EndGameState(sk.GetComponent<NavMeshAgent>()));
                    isPlayerHere = true;
//                    Debug.Log("Player is in");

                    break;
                case Constants.Tags.SideKick:
                    if(isSidekickHere)
                        return;
                    isSidekickHere = true;
//                    Debug.Log("sidekick is in");
                    break;
            }
        }

        //void OnTriggerExit(Collider other)
        //{
        //    isSidekickHere = other.tag != Constants.Tags.SideKick && isSidekickHere;
        //    isPlayerHere = other.tag != Constants.Tags.Player && isPlayerHere;
        //    Debug.Log("someone left");
        //}
    }
} 
