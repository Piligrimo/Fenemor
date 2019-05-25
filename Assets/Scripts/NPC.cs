using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt(0.7225f*(y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    public Vector3[] targets = new Vector3[10];
    public int tarcount;
    public bool go,right;
    Rigidbody2D rb;
    public float speed;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tarcount < 0)
            go = false;
        if (go)
            rb.velocity = new Vector2(speed * (targets[tarcount].x - transform.position.x) / dist(targets[tarcount], transform.position), 0.85f*speed * (targets[tarcount].y - transform.position.y) / dist(targets[tarcount], transform.position));
        else
            rb.velocity = Vector2.zero;
        if(tarcount >= 0)
        if (dist(targets[tarcount], transform.position) < 0.15 )
            tarcount--;
        if ((right && rb.velocity.x < 0) || (!right && rb.velocity.x > 0))
        {
            transform.Rotate(0, 180, 0);
            right = !right;
        }
        float y = transform.localPosition.y, z = transform.localPosition.z;
        z = z - (float)(y - 1);
        transform.localPosition -= new Vector3(0, 0, z);
    }
}
