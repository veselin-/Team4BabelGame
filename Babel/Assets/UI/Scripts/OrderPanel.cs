using Assets.Characters.AiScripts;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.UI.Scripts
{
    public class OrderPanel : MonoBehaviour {

        private SidekickControls _sidekickControls;

        // Use this for initialization
        void Start ()
        {
            _sidekickControls = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick).GetComponent<SidekickControls>();
        }
	
        public void DoOrder(int i)
        {
            if (i == -2)
            {
                var sk = _sidekickControls.gameObject.GetComponent<AiMovement>();
                sk.Happines -= 0.2f;
                if (sk.Happines < 0)
                    sk.Happines = 0;
            } else if (i == -3)
            {
                var sk = _sidekickControls.gameObject.GetComponent<AiMovement>();
                sk.Happines += 0.2f;
                if (sk.Happines > 1)
                    sk.Happines = 1;
            } else if (i == -4)
            {
                GameObject.FindGameObjectWithTag(Constants.Tags.DatabaseManager).GetComponent<DatabaseManager>().ResetUserData();
            }

            _sidekickControls.ExecuteAction(i);
        }
    }
}
