using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.LevelSelector
{
    public class LevelSelector : MonoBehaviour
    {
        public GameObject[] LevelButtons;

        // Use this for initialization
        void Start () {
            Time.timeScale = 1f;

            var userLevels = Userlevels.GetInstance().GetUserLevels();
            if (userLevels.FirstOrDefault() == "all")
                return;

            foreach (var levelButton in LevelButtons)
                levelButton.GetComponent<Button>().interactable = userLevels.Contains(levelButton.name);
        }

        public void LoadLevel(string SceneName)
        {
            if(!string.IsNullOrEmpty(SceneName))
            {
                Application.LoadLevel (SceneName);
            }
        }

        public void BackToMainMenu()
        {
            Application.LoadLevel ("MainMenu");
        }
    }
}
