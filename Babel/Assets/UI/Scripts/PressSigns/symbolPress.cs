using UnityEngine;
using System.Collections;

public class symbolPress : MonoBehaviour {

    public GameObject slot, sylPanel;
    public static int count = 0;
    //GameObject chosenSign;
    int childIndex;
    bool moved = false;
    public bool first = false, second = false, third = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SaveChildIndex()
    {
        //chosenSign = Instantiate(gameObject);
        childIndex = transform.GetSiblingIndex();
    }

    void MoveSignBackInBook()
    {
        transform.SetParent(sylPanel.transform);
        transform.SetSiblingIndex(childIndex);
        transform.localScale = Vector3.one;
        count -= 1;
    }

    public void PressedSymbol()
    {
        if (count > 0 && first || second || third && moved)
        {
            if (first)
            {
                first = false;
            }
            else if (second)
            {
                second = false;
            }
            else if(third)
            {
                third = false;
            }
            MoveSignBackInBook();
            moved = false;
        }
        else
        {
            moved = true;
            if (count == 2)
            {
                SaveChildIndex();
                third = true;
                transform.SetParent(slot.transform.GetChild(0).transform.GetChild(0).transform);
                count += 1;
            }
            else if (count == 1)
            {
                SaveChildIndex();
                second = true;
                transform.SetParent(slot.transform.GetChild(0).transform);
                count += 1;
            }
            else if (count == 0)
            {
                SaveChildIndex();
                first = true;
                transform.SetParent(slot.transform);
                count += 1;
            }
        }
        Debug.Log("COUNT ER: " + count);
        Debug.Log("moved ER: " + moved);
        Debug.Log("first ER: " + first);
        Debug.Log("second ER: " + second);
        Debug.Log("third ER: " + third);
    }
}
