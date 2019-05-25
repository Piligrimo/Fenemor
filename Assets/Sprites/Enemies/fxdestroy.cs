using UnityEngine;
using System.Collections;

public class fxdestroy : MonoBehaviour {
  public  float existtime,dietime;
	// Use this for initialization
	void Start () {
        existtime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        existtime += Time.deltaTime;
        if (existtime > dietime)
            Destroy(gameObject);
    }
}
