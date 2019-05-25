using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public Vector3 target;
    Rigidbody2D rb;
    public GameObject d;
    // Use this for initialization
    float damage;
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    void Start () {
        damage = GameObject.Find("Фенемор").GetComponent<BriskScript>().damage;
        float speed = 3f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * (target.x - transform.position.x) / dist(target, transform.position), speed * (target.y - transform.position.y) / dist(target, transform.position));rb.velocity = new Vector2(speed * (target.x - transform.position.x) / dist(target, transform.position), speed * (target.y - transform.position.y) / dist(target, transform.position));
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(target.x - transform.position.x) + Mathf.Abs(target.y - transform.position.y) < 0.04 || Mathf.Abs(target.x - transform.position.x) + Mathf.Abs(target.y - transform.position.y) > 40)
        {
            GameObject b = Instantiate(d, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "enemy")
        {
            if (other.GetComponent<EnemyAdvanced>() != null)
            {
                if (other.GetComponent<EnemyAdvanced>().CanBeHit)
                    other.GetComponent<EnemyAdvanced>().hp -= damage;
                other.GetComponent<EnemyAdvanced>().lastAtackGot=5;
            }
            else
                other.GetComponent<HexPigScript>().hp -= damage;
            GameObject b = Instantiate(d, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }

    }
}
