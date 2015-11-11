using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

    public int orbValue;
	private AudioSource _audioSource;
	
	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
		Debug.Log (_audioSource.enabled);
	}
	
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
			_audioSource.Play();
            PlayerPrefs.SetInt("CurrencyAmount", PlayerPrefs.GetInt("CurrencyAmount", CurrencyControl.currencyAmount) + orbValue);
            //Debug.Log("WTF YOU ARE HITTING ME!?");
			StartCoroutine(DestroyOrb());
        }
    }

	IEnumerator DestroyOrb()
	{
		yield return new WaitForSeconds(0.1f);
		Destroy(this.gameObject);
	}
}
