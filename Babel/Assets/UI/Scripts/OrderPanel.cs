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
            _sidekickControls.ExecuteAction(i);
        }
    }
}
