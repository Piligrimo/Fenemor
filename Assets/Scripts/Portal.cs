using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
    public int loc;
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float y = transform.localPosition.y, z = transform.localPosition.z;
        z = z - (float)(y - 1);
        transform.localPosition -= new Vector3(0, 0, z);
        Debug.Log(dist(GameObject.Find("Фенемор").transform.position, transform.position));
        if (dist(GameObject.Find("Фенемор").transform.position, transform.position) < 0.5)
        {
            GameObject.Find("Фенемор").GetComponent<BriskScript>().save("autosave");
            Application.LoadLevel(loc);
        }
	}
}
