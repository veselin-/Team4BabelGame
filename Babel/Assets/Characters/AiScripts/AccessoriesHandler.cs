using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.AiScripts
{
    public class AccessoriesHandler : MonoBehaviour
    {
        public GameObject Hat;

        void Start()
        {
            Hat.SetActive(false);
            if (PlayerPrefsBool.GetBool(gameObject.tag + Constants.ShopItems.Hat))
            {
                Hat.SetActive(true);
                Hat.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}
