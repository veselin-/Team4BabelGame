using UnityEngine;
using System.Collections;

public class NormalMapAnim : MonoBehaviour {

	public Material environmentMat;
	private float _bumpiness = 0f;

	// Use this for initialization
	void Start () {
		//_bumpiness = environmentMat.
		environmentMat.EnableKeyword("_NORMALMAP");
	}
	
	// Update is called once per frame
	void Update () {
	
		_bumpiness = Mathf.PingPong (Time.time, 0.5f);
		environmentMat.SetFloat ("_BumpScale", _bumpiness);
	}



}
