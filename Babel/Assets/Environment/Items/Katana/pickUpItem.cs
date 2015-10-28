using UnityEngine;
using System.Collections;

public class pickUpItem : MonoBehaviour {

    GameObject katanaDrop, katana, player;
    bool katanaUse = false;
	// Use this for initialization
	void Start () {
        katanaDrop = GameObject.FindGameObjectWithTag("PUKatana");
        katana = GameObject.FindGameObjectWithTag("Katana");
        player = GameObject.FindGameObjectWithTag("Player");
        katana.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PUKatana" && Item.pressed == true)
        {
            KatanaUsed();
        }
    }

    void OnMouseDown()
    {
       if (katanaUse == true)
        {
            Item.pressed = false;
            KatanaDropped();
        }
    }

    void KatanaUsed()
    {
        katanaDrop.SetActive(false);
        katana.SetActive(true);
        katanaUse = true;
    }

    void KatanaDropped()
    {
        katana.SetActive(false);
        katanaDrop.transform.localPosition = player.transform.localPosition;
        katanaDrop.SetActive(true);
        katanaUse = false;
    }
}
