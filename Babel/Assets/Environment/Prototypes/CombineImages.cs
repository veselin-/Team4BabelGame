using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombineImages : MonoBehaviour {

	public Texture2D tex1;
	public Texture2D tex2;
	public Image img;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	void Combine()
	{
		var cols1 = tex1.GetPixels();
		var cols2 = tex2.GetPixels();
		for(var i = 0; i < cols1.Length; ++i)
		{
			cols1[i] += cols2[i];
		}
		tex1.SetPixels(cols1);
		tex1.Apply();
		img.sprite = tex1;
	}
	*/
}
