using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestingDBManager : MonoBehaviour {


    private DatabaseManager db;

	// Use this for initialization
	void Start () {
        db = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
        db.LoadData();

        StartCoroutine(WaitAndPrint(0.5F));
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
       while(!db.DatabasesLoaded())
        {
            Debug.Log("not loaded");
            yield return new WaitForSeconds(waitTime);
        }

        Debug.Log("LOADED");

        GameObject.Find("Image0").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(0).ImageName);
        GameObject.Find("Image1").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(1).ImageName);
        GameObject.Find("Image2").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(2).ImageName);
        GameObject.Find("Image3").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(3).ImageName);
    }
}
