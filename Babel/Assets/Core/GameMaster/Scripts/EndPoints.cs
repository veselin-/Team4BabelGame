using UnityEngine;

namespace Assets.Core.GameMaster.Scripts
{
    public class EndPoints : MonoBehaviour
    {

        public GameObject[] Endpoints;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {


        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);    
        }
    }
}
