using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Core.Configuration;
using System.Linq;
using System.Collections.Generic;

public class TestingDBManager : MonoBehaviour {


    private DatabaseManager db;

	// Use this for initialization
	void Start () {
        db = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
        GameObject.Find("Image0").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(0).ImageName);
        GameObject.Find("Image1").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(1).ImageName);
        GameObject.Find("Image2").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(2).ImageName);
        GameObject.Find("Image3").GetComponent<Image>().sprite = db.GetImage(db.GetSyllable(3).ImageName);


        List<int> list1 = new List<int>();
        list1.Add(1);
        list1.Add(2);
        list1.Add(3);

        List<int> list2 = new List<int>();
        list2.Add(3);
        list2.Add(1);
        list2.Add(2);


        List<int> list3 = new List<int>();
        list3.Add(3);
        list3.Add(2);
        list3.Add(1);


        //var t1 = db.GetSentenceBySeq(list1);
        //var t2 = db.GetSentenceBySeq(list2);
        //var t3 = db.GetSentenceBySeq(list3);

        //if(t1 == -1 && t2 == -1 && t3 == -1)
        //{

        //}


        var uu = db.GetSign(0);

        if(uu == null)
        {

        }


    }

}
