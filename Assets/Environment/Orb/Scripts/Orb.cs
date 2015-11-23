using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

    public int orbValue;

	// Use this for initialization
	void Start () {

	}
	
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
			if(!GetComponent<AudioSource> ().isPlaying)
			{
				GetComponent<AudioSource> ().Play();
			}

            PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) + orbValue);
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
