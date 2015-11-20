using UnityEngine;
using System.Collections;
using System;

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
        Application.LoadLevel(mainScene);
    }

    private void mainMenuActions()
    {
        Application.Quit();
    }
}
