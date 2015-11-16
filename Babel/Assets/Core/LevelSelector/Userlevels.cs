using System.Collections.Generic;
using System.Linq;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Core.LevelSelector
{
    public class Userlevels
    {
        #region Singleton
        private static Userlevels _instance;
        public static Userlevels GetInstance()
        {
            return _instance ?? (_instance = new Userlevels());
        }
        #endregion

        private HashSet<string> _userLevels; 

        private Userlevels()
        {
            LoadUserLevels();
        }

        public string[] GetUserLevels()
        {
            LoadUserLevels();
            return _userLevels.ToArray();
        }

        public void AddUserLevel(string id)
        {
            _userLevels.Add(id);
            SaveUserLevels();
        }

        public void ClearUserLevels()
        {
            _userLevels.Clear();
            SaveUserLevels();
        }

        private void LoadUserLevels()
        {
            var playerprefString = PlayerPrefs.GetString(Constants.PlayerPrefs.UserLevels);
            var userLevelsStrings = (string.IsNullOrEmpty(playerprefString) ? "1" : playerprefString).Split(';');
            _userLevels = new HashSet<string>(userLevelsStrings);
        }

        private void SaveUserLevels()
        {
            var userLevelsString = string.Join(";", _userLevels.Select(x => x.ToString()).ToArray());
            PlayerPrefs.SetString(Constants.PlayerPrefs.UserLevels, userLevelsString);
        }
    }
}
