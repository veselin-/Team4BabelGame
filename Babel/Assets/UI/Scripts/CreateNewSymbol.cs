using UnityEngine;
using System.Collections;

public class CreateNewSymbol : MonoBehaviour
{

    public static int SymbolID = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void NextSymbol()
    {
        SymbolID++;
    }
}
