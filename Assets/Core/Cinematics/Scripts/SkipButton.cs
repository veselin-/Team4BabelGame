using UnityEngine;
using System.Collections;

public class SkipButton : MonoBehaviour
{

    public string levelName;
    AsyncOperation async;
    private string currentScene;


    void Start()
    {
        currentScene = Application.loadedLevelName;
     //   StartLoading();
    }

    public void StartLoading()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        async = Application.LoadLevelAsync(currentScene == "Cinematic1" ? "Tutorial1Beta" : "Tutorial2Beta");
        async.allowSceneActivation = false;
      //  Debug.Log("Begun Loading");
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }


    public void Skip()
    {
          var currentScene = Application.loadedLevelName;
           Application.LoadLevel(currentScene == "Cinematic1" ? "Tutorial1Beta" : "Tutorial2Beta");
      // async.allowSceneActivation = true;
    }
}
