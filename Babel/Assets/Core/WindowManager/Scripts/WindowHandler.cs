using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Core.Configuration;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public GameObject DialogWindow;


    private void Awake()
    {
      //  DontDestroyOnLoad(this.gameObject);
    }


    public void  ActivateDialogWindow(string header, string content, bool canBeClosed)
    {
        
        Time.timeScale = 0f;
     
        Text[] allTextComponents = DialogWindow.GetComponentsInChildren<Text>(true);

        //0 is the button text, 1 is header and 2 is content
        allTextComponents[1].text = header;
        allTextComponents[2].text = content;

        DialogWindow.SetActive(true);

        //If it is an error we don't allow the user to close the window
        if (!canBeClosed)
        {
            Button[] but = DialogWindow.GetComponentsInChildren<Button>(true);
            but[0].gameObject.SetActive(false);
        }



    }

    public void DeactivateDialogWindow()
    {
        DialogWindow.SetActive(false);
        Time.timeScale = 1f;

    }




}
