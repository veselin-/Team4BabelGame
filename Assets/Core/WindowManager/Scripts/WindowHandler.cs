using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//  This script will be updated in Part 2 of this 2 part series.
public class WindowHandler : MonoBehaviour
{

    public Text Content;
    public Text Header;
    public Button YesButton;
    public Button NoButton;
    public GameObject DialogWindow;

    private static WindowHandler _windowHandler;

    public static WindowHandler Instance()
    {
        if (!_windowHandler)
        {
            _windowHandler = FindObjectOfType(typeof(WindowHandler)) as WindowHandler;
            if (!_windowHandler)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return _windowHandler;
    }

    public void CreateConfirmDialog(string header, string content, string yesOptionText, string noOptionText, UnityAction yesEvent, UnityAction noEvent)
    {
        SetDefaultValues(header, content);

        YesButton.GetComponentInChildren<Text>().text = yesOptionText;
        YesButton.onClick.RemoveAllListeners();
        if(yesEvent != null)
             YesButton.onClick.AddListener(yesEvent);
        YesButton.onClick.AddListener(ClosePanel);

        NoButton.GetComponentInChildren<Text>().text = noOptionText;
        NoButton.onClick.RemoveAllListeners();
        if (noEvent != null)
            NoButton.onClick.AddListener(noEvent);
        NoButton.onClick.AddListener(ClosePanel);
    }

    public void CreateInfoDialog(string header, string content, string yesOptionText, UnityAction yesEvent)
    {
        SetDefaultValues(header, content);

        YesButton.GetComponentInChildren<Text>().text = yesOptionText;
        YesButton.onClick.RemoveAllListeners();
        if (yesEvent != null)
            YesButton.onClick.AddListener(yesEvent);
        YesButton.onClick.AddListener(ClosePanel);

        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(false);
    }

    public void CreateStaticDialog(string header, string content)
    {
        SetDefaultValues(header, content);

        YesButton.gameObject.SetActive(false);
        NoButton.gameObject.SetActive(false);
    }


    private void SetDefaultValues(string header, string content)
    {
        DialogWindow.SetActive(true);
        Content.text = content;
        Header.text = header;
        Time.timeScale = 0f;
    }


    public void ClosePanel()
    {
        DialogWindow.SetActive(false);
        Time.timeScale = 1f;
    }


}