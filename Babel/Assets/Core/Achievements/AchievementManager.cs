using System;
using Assets.Core.Configuration;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Achievements
{   
    public class AchievementManager// : MonoBehaviour
    {
        #region Instance
        private static AchievementManager _instance;
        public static AchievementManager Instance
        {
            get
            {
                _instance = _instance ?? new AchievementManager();
                return _instance;
            }
        }
        #endregion

        private bool _loggedIn;

        private AchievementManager()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            
            try
            {
                Social.Active.localUser.Authenticate((bool success) =>
                {
                    _loggedIn = success;
                });
            }
            catch (System.Exception e)
            {
            }
        }

        public void UnlockAchievement(string achievement)
        {
            try
            {
                if (Social.localUser.authenticated)
                {
                    Social.ReportProgress(achievement, 100.0f, (bool success) =>
                    {
                        //if (success)
                        //{
                        //    console.text = "Achievement Unlock Successful";
                        //}
                        //else
                        //{
                        //    console.text = "Achievement Unlock Unsuccessful";
                        //    Handheld.Vibrate();
                        //}
                    });
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        public void ShowAchievements()
        {
            Social.ShowAchievementsUI();
        }
    }
} 
