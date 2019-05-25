using UnityEngine;
using System.Collections;

public class FXplay : MonoBehaviour {
    Animator an;
    float time;
	// Use this for initialization
	void Start () {
        an = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	if (time>5)
        {
            if (Random.value > 0.5)
                an.SetBool("play", true);
            else
            {
                an.SetBool("play", false);
                time = 0;
            }
        }
	}
}
