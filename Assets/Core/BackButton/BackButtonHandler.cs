using UnityEngine;
using System.Collections;
using System;
using Assets.Core.Configuration;

public class BackButtonHandler : MonoBehaviour {

    private String currentScene;
    public String mainScene;
    public String levelSelectScene;
    public String LoadingScreen1;
    public String LoadingScreen2;
    public String LoadingScreen3;


    // Use this for initialization
    void Start () {
        currentScene = Application.loadedLevelName;
        Debug.Log(currentScene);
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentScene.Equals(LoadingScreen1) || currentScene.Equals(LoadingScreen2) || currentScene.Equals(LoadingScreen3)) ;
            else if (currentScene.Equals(mainScene)) mainMenuActions();
            else if (currentScene.Equals("BackButtonTest")) Application.Quit();
            else if (currentScene.Equals(levelSelectScene)) Application.LoadLevel(mainScene);
            else defaultAction();
        }
    }

    private void defaultAction()
    {
        //Application.LoadLevel(mainScene);
        if (Time.timeScale == 1)
        {
            GameObject.FindObjectOfType<PauseScreen>().PausePanelBtn();
        }
        else
        {
            GameObject.FindObjectOfType<PauseScreen>().PausePanelBackBtn();
        }
        
    }

    private void mainMenuActions()
    {

       // GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().ActivateDialogWindow("Exit Game", "Do you want to exit the game?", true);
        Application.Quit();
    }
}
