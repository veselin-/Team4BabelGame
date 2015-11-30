using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class UiSnapScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
	private AudioManager _audioManager; 

    //private bool lerpToNearestPoint = false;

    private ScrollRect scrollRect;

    private float[] steps;
    public int menuCount;

    public GameObject[] Pips;

    // Use this for initialization
    void Start ()
    {
		_audioManager = GameObject.FindObjectOfType<AudioManager> ().GetComponent<AudioManager> ();
      // menuCount = transform.GetChild(0).transform.childCount;

        scrollRect = GetComponent<ScrollRect>();

        steps = new float[menuCount];

        int count = 0;

        for (int i = 0; i < menuCount; i++)
        {
            steps[i] = (1f/ (menuCount-1)) * count;
         //   Debug.Log(steps[i]);
            count++;
        }

    }
	
	// Update is called once per frame
	void Update () {

	    if (scrollRect.horizontalNormalizedPosition < 0.16f)
	    {
            Pips[0].GetComponent<Image>().color = new Color(133, 162, 192);
            Pips[1].GetComponent<Image>().color = Color.black;
            //Pips[2].GetComponent<Image>().color = Color.black;
        }
	    else if (scrollRect.horizontalNormalizedPosition > 0.16f && scrollRect.horizontalNormalizedPosition < 0.56f)
	    {
            Pips[0].GetComponent<Image>().color = Color.black;
            Pips[1].GetComponent<Image>().color = new Color(133, 162, 192);
            //Pips[2].GetComponent<Image>().color = Color.black;
        }


	}

    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log(scrollRect.horizontalNormalizedPosition);


        StartCoroutine(LerpToPoint());
    }

    IEnumerator LerpToPoint()
    {
        float timer = 0f;
        float distanceNew = 1f;
        float distanceShortest = 1f;
        float startPos = scrollRect.horizontalNormalizedPosition;
        float endPos = 0f;

        foreach (float s in steps)
        {

            distanceNew = Mathf.Abs(startPos - s);
          //  Debug.Log(distanceNew);
            if (distanceNew < distanceShortest)
            {
                distanceShortest = distanceNew;
                endPos = s;
            //    Debug.Log(endPos);
            }

        }



        while (timer < 1f)
        {

            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPos, endPos, timer);

            timer += Time.unscaledDeltaTime * 5f;

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
		_audioManager.SwipeBtnPlay ();
        StopAllCoroutines();
    }
}
