using UnityEngine;
using System.Collections;

public class MindEnter : MonoBehaviour {
    public GameObject Feny;
    // Use this for initialization
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Feny.GetComponent<NewBehaviourScript>().canEnter = dist(Feny.transform.position, transform.position) < 1;
	}
}
