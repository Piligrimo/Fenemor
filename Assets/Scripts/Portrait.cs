using UnityEngine;
using System.Collections;

public class Portrait : MonoBehaviour {
    Animator an;
    public float t;
    public bool hurt;
	// Use this for initialization
	void Start () {
        an = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float hpperc=1;
        if (GameObject.Find("Фенемор").GetComponent<NewBehaviourScript>()!=null)
         hpperc = GameObject.Find("Фенемор").GetComponent<NewBehaviourScript>().hp / GameObject.Find("Фенемор").GetComponent<NewBehaviourScript>().mhp;
        else
            hpperc = GameObject.Find("Фенемор").GetComponent<BriskScript>().hp / GameObject.Find("Фенемор").GetComponent<BriskScript>().mhp;

        an.SetBool("lowhp", hpperc < 0.3);
        an.SetBool("hurt", hurt);
        if (t < 0)
            hurt = false;
        else
            t -= Time.deltaTime;

	}
}
