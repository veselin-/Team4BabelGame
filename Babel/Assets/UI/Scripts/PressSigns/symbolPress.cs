using UnityEngine;
using System.Collections;

public class symbolPress : MonoBehaviour {

    public GameObject slot1, sylPanel;
    static bool isInSlut = false;
    static int count = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PressedSymbol()
    {
        if (isInSlut && count == 2)
        {
            transform.SetParent(slot1.transform.GetChild(0).transform.GetChild(0).transform);
            count += 1;
            isInSlut = true;
            Debug.Log("COUNT ER: " + count);
            Debug.Log("isInSlut is: " + isInSlut);
        }
        else if (isInSlut && count == 1)
        {
            transform.SetParent(slot1.transform.GetChild(0).transform);
            count += 1;
            isInSlut = true;
            Debug.Log("COUNT ER: "+ count);
            Debug.Log("isInSlut is: " + isInSlut);
        }
        else if (isInSlut == false)
        {
            transform.SetParent(slot1.transform);
            count += 1;
            isInSlut = true;
            Debug.Log("COUNT ER: " + count);
            Debug.Log("isInSlut is: " + isInSlut);
        }
        else 
        {
            transform.SetParent(sylPanel.transform);
            count -= 1;
            isInSlut = false;
            Debug.Log("COUNT ER: " + count);
            Debug.Log("isInSlut is: " + isInSlut);
        }
        // NÅR MAN HAR 3 SYMBOLER, OG TAGER ET FRA BLIVER BOOL SAT TIL FALSE, DERFOR NÅR DU TYKKER PÅ NÆSTE, VIL COUNT STIGE MED 1, DA BOOL ER FALSE, FIX DET!
    }
}
