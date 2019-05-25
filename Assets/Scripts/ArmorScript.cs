using UnityEngine;
using System.Collections;

public class ArmorScript : MonoBehaviour {
    public int ArmorID;
  
    // Use this for initialization
    void Start () {
	
	}
	 
	// Update is called once per frame
	void Update () {
        if (GetComponent<NewBehaviourScript>() != null)
            if (ArmorID == 3 && (GetComponent<NewBehaviourScript>().hp< GetComponent<NewBehaviourScript>().mhp ))
                GetComponent<NewBehaviourScript>().hp +=Time.deltaTime*0.1f;

        if (GetComponent<BriskScript>() != null)
            if (ArmorID == 3 && ( GetComponent<BriskScript>().hp < GetComponent<BriskScript>().mhp))
                GetComponent<BriskScript>().hp += Time.deltaTime * 0.1f;
    }
}
