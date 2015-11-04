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
            PlayerPrefs.SetInt("CurrencyAmount", CurrencyControl.currencyAmount + orbValue);
            Destroy(this.gameObject);
            Debug.Log("WTF YOU ARE HITTING ME!?");
        }
    }
}
