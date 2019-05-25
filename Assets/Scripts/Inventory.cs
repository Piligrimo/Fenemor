using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
  
    public int[] ID;
    public int[] type;
    public int num;

    // Use this for initialization
    void Start () {

        num = 0;
        ID = new int[24];
        type = new int[24];
    }
	
	// Update is called once per frame
	void Update () {

    }
}
