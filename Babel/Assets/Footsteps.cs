using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	public AudioClip[] footsteps;
	private AudioSource footsound;
	private int previousFootstep;

	// Use this for initialization
	void Start () {
		footsound = GetComponent<AudioSource> ();
		previousFootstep = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void RandomFootsteps()
	{
		int randomFootstep = Random.Range (0, footsteps.Length);
		while(randomFootstep == previousFootstep)
		{
			randomFootstep = Random.Range (0, footsteps.Length);
		}
		previousFootstep = randomFootstep;

//		Debug.Log (transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString());
		footsound.clip = footsteps[randomFootstep];
		if(!footsound.isPlaying)
			footsound.Play ();
	}
}
