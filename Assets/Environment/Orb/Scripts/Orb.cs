using UnityEngine;
using System.Collections;
using Assets.Core.GameMaster.Scripts;

public class Orb : MonoBehaviour {

    public int orbValue;
	private EndPoints ep;
	// Use this for initialization
	void Start () {
		ep = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<EndPoints>();	
	}
	
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
			if(!GetComponent<AudioSource> ().isPlaying)
			{
				GetComponent<AudioSource> ().Play();
				ep.orbs += 1;
				PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) + orbValue);
			}

            
            Debug.Log("WTF YOU ARE HITTING ME!?");
			StartCoroutine(DestroyOrb());
        }
    }

	IEnumerator DestroyOrb()
	{
		transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<ParticleSystem>().Stop();
		yield return new WaitForSeconds(GetComponent<AudioSource> ().clip.length);
		Destroy(this.gameObject);
	}
}
