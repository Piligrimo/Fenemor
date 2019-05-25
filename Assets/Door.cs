using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    GameObject Fen;
    public bool unlocked = true;
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt(0.7225f * (y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    // Use this for initialization
    void Start () {
        Fen = GameObject.Find("Фенемор");
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Animator>().SetBool("opn",dist(Fen.transform.position,transform.position)<2 && unlocked);
	}
}
