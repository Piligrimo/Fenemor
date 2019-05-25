using UnityEngine;
using System.Collections;

public class SlowFX : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = GameObject.Find("Фенемор").transform.position + new Vector3(-0f,0,-0.1f);
        if (GameObject.Find("Фенемор").GetComponent<BriskScript>().hp<0 || GameObject.Find("Фенемор").GetComponent<BriskScript>().slowtime<=0)
            Destroy(gameObject);
    }
}
