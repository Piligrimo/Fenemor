using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Фенемор")
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Фенемор")
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
