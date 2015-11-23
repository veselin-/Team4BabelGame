using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.AiScripts
{
    public class AccessoriesHandler : MonoBehaviour
    {
        public GameObject Hat;
        //GameObject hat;

        void Start()
        {
            Hat.SetActive(false);
            if (PlayerPrefsBool.GetBool(gameObject.tag + Constants.ShopItems.Hat))
            {
                Hat.SetActive(true);
                Hat.GetComponent<Renderer>().material.color = Color.blue;
            }
        }

        //public void PlayerHat(GameObject check)
        //{
        //    hat = GameObject.FindGameObjectWithTag("PlayerHat");
        //    if (PlayerPrefsBool.GetBool("PlayerHat") == true)
        //    {
        //        if (check.activeSelf == false)
        //        {
        //            check.SetActive(true);
        //            hat.SetActive(true);
        //            Debug.Log("NU ER DEN PÅ");
        //        }
        //        else
        //        {
        //            check.SetActive(false);
        //            hat.SetActive(false);
        //            Debug.Log("NU ER DEN AF DIT LORTE HVOEDE");
        //        }
        //    }
        //}
    }
}
