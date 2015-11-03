using UnityEngine;


namespace Assets.Standard_Assets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
