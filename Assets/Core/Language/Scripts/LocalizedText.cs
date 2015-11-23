using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
	public string localizedID = string.Empty;
	void Start()
	{
		LocalizeText();
	}
	
	public void LocalizeText()
	{
		Text text = GetComponent<Text>();
		if (text != null)
		{
			text.text = LanguageManager.Instance.Get(localizedID);
		}
	}
}
