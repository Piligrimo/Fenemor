using UnityEngine;
using System.Collections;

public class GreatEssLongAttack : MonoBehaviour {
    public Vector3 target;
    Rigidbody2D rb;
    public GameObject fx,d;
   
    // Use this for initialization
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    // Use this for initialization
    void Start () {
        target = GameObject.Find("Фенемор").transform.position+new Vector3(0,0.1f,0);
        float speed = 3f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * (target.x - transform.position.x) / dist(target, transform.position), speed * (target.y - transform.position.y) / dist(target, transform.position)); rb.velocity = new Vector2(speed * (target.x - transform.position.x) / dist(target, transform.position), speed * (target.y - transform.position.y) / dist(target, transform.position));
    }
	
	// Update is called once per frame
	void Update () {
        float y = transform.localPosition.y, z = transform.localPosition.z;
        z = z - (float)(y - 1);
        transform.localPosition -= new Vector3(0, 0, z);
        if (Mathf.Abs(target.x - transform.position.x) + Mathf.Abs(target.y - transform.position.y) < 0.04 || Mathf.Abs(target.x - transform.position.x) + Mathf.Abs(target.y - transform.position.y) > 40)
        {
           GameObject b = Instantiate(d, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Фенемор")
        {
            GameObject b = Instantiate(fx, transform.position, transform.rotation) as GameObject;
            if (other.GetComponent<BriskScript>().slow > 1)
                Destroy(b);
            other.GetComponent<BriskScript>().slow += 2f;
            other.GetComponent<BriskScript>().slowtime = 6f;
                 
            Destroy(gameObject);
        }
    }
}
