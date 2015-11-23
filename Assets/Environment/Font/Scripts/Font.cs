using System.Linq;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Font.Scripts
{
    public class Font : MonoBehaviour, IInteractable
    {

        public GameObject[] InteractPoints;
        public GameObject Water;
        public int WaterLevel;

        void Start()
        {
            Water.GetComponent<Renderer>().material.color = Color.blue;
            FixWater();
        }

        public bool HasBeenActivated()
        {
            return WaterLevel == 2;
        }

        public GameObject Interact(GameObject pickup)
        {
            if (!CanThisBeInteractedWith(pickup)) return null;
            pickup.GetComponent<Bucket.Scripts.Bucket>().HasWaterInIt = false;
            RaiseWater();
            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return (pickup != null && pickup.GetComponent<Bucket.Scripts.Bucket>() != null && pickup.GetComponent<Bucket.Scripts.Bucket>().HasWaterInIt);
        }

        public Vector3 InteractPosition(Vector3 ai)
        {
            return InteractPoints.OrderBy(g => Vector3.Distance(g.transform.position, ai)).
                ToArray()[0].transform.position;
        }

        void RaiseWater()
        {
            WaterLevel += WaterLevel > 1 ? 0 : 1;
            FixWater();
        }

        void FixWater()
        {
            switch (WaterLevel)
            {
                case (0):
                    Water.SetActive(false);
                    break;
                case (1):
                    Water.SetActive(true);
                    var vec = Water.transform.localPosition;
                    vec.y = 84;
                    Water.transform.localPosition = vec;
                    vec = Water.transform.localScale;
                    vec.x = 28.2f;
                    vec.z = 28.2f;
                    Water.transform.localScale = vec;
                    break;
                case (2):
                    Water.SetActive(true);
                    vec = Water.transform.localPosition;
                    vec.y = 95;
                    Water.transform.localPosition = vec;
                    vec = Water.transform.localScale;
                    vec.x = 33f;
                    vec.z = 33f;
                    Water.transform.localScale = vec;
                    break;
            }
        }
    }
}
