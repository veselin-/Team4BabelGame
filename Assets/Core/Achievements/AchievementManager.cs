using System;
using Assets.Core.Configuration;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

namespace Assets.Core.Achievements
{
    public class AchievementManager
    {

        private static AchievementManager _instance;
        public static AchievementManager Instance
        {
            get
            {
                _instance = _instance ?? new AchievementManager();
                return _instance;
            }
        }

        private AchievementManager()
        {
            var _windowHandler = GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>();
            try
            {
                PlayGamesPlatform.DebugLogEnabled = true;
                PlayGamesPlatform.Activate();

               // _windowHandler.CreateInfoDialog("Test", Social.localUser.ToString(), "OK", null);

                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("Sign in Successful");
                    }
                    else
                    {
                        Debug.LogError("Sign in Unsuccessful");
                   //     _windowHandler.CreateInfoDialog("Error", "Sign in Unsuccessful", "OK", null);
                    }
                });
            }
            catch (Exception e)
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
    }
} 
