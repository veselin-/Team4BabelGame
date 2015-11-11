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
			GetComponent<AudioSource> ().Play();
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
