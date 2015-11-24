using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;  
using UnityEngine.UI;

public class ButtonFuncs : MonoBehaviour {

    public string achievement = "CgkI0JqQkYAGEAIQAQ";
    public Text console;

    // Use this for initialization
    void Start ()
    {
        console.text = "Activation started";
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        console.text = "Activation done";


    }

    // Update is called once per frame
    void Update () {
	
	}

    public void signIn()
    {
        try
        {
            console.text = "Sign in started...";
            // Social.localUser.Authenticate((bool success) =>
            Social.Active.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    console.text = "Sign in Successful";
                }
                else
                {
                    console.text = "Sign in Unsuccessful";
                    Handheld.Vibrate();
                }
            });
        }
        catch (System.Exception e)
        {
            console.text = e.Message + "\n\n" + e.StackTrace;
        }
        
    }

    public void unlockAchievement()
    {
        try
        {
            if (Social.localUser.authenticated)
            {
                Social.ReportProgress(achievement, 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        console.text = "Achievement Unlock Successful";
                    }
                    else
                    {
                        console.text = "Achievement Unlock Unsuccessful";
                        Handheld.Vibrate();
                    }
                });
            }
            else
            {
                console.text = "user not Authenticated";
            }
        }
        catch (System.Exception e)
        {
            console.text = e.Message + "\n\n" + e.StackTrace;
        }

    }

    public void showAchievements()
    {
        try
        {
            if (Social.localUser.authenticated)
            {
                Social.ShowAchievementsUI();
                console.text = "Achievements shown";
            }
            else
            {
                console.text = "user not Authenticated";
            }
        }
        catch (System.Exception e)
        {
            console.text = e.Message + "\n\n" + e.StackTrace;
        }
    }

    public void signOut()
    {
        try
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            console.text = "Sign out successful";
        }
        catch (System.Exception e)
        {
            console.text = e.Message + "\n\n" + e.StackTrace;
        }
    }
}
