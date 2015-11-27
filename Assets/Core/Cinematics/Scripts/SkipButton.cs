using UnityEngine;
using System.Collections;

public class SkipButton : MonoBehaviour
{
    public void Skip()
    {
        var currentScene = Application.loadedLevelName;
        Application.LoadLevel(currentScene == "Cinematic1" ? "Tutorial1Beta" : "Tutorial2Beta");
    }
}
