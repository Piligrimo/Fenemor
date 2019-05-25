using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
    public int ItemID, ItemType;
    GameObject f;
	// Use this for initialization
	void Start () {
        f = GameObject.Find("Фенемор");

    }
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    // Update is called once per frame
    void Update () {
        float y = transform.localPosition.y, z = transform.localPosition.z;
        z = z - (float)(y - 1);
        transform.localPosition -= new Vector3(0, 0, z);
        if (dist(transform.position, f.transform.position) < .3)
        {
            if (f.GetComponent<BriskScript>()!=null)
            {
                
                f.GetComponent<BriskScript>().itemtypes[f.GetComponent<BriskScript>().n] = ItemType;
                f.GetComponent<BriskScript>().inv[f.GetComponent<BriskScript>().n] = ItemID;
                f.GetComponent<BriskScript>().n++;
                Destroy(gameObject);
            }
        }
    }

}
