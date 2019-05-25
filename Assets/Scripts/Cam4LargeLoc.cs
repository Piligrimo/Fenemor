using UnityEngine;
using System.Collections;

public class Cam4LargeLoc : MonoBehaviour {
    public GameObject player;
    Rigidbody2D rb;
    public float left, right, up, down;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update () {
        float s = 0.9f;
        if (transform.position.x - player.transform.position.x > 1 && transform.position.x > left)
            rb.velocity = new Vector2(-s, 0);
        else
            rb.velocity = new Vector2(0, 0);
        if (transform.position.x - player.transform.position.x < -1 && transform.position.x < right)
            rb.velocity = new Vector2(s, 0);
        if (transform.position.y - player.transform.position.y > 1 && transform.position.y > down)
            rb.velocity = new Vector2(rb.velocity.x,-s);
        else
            rb.velocity = new Vector2(rb.velocity.x, 0);
        if (transform.position.y - player.transform.position.y < -1 && transform.position.y < up)
            rb.velocity = new Vector2(rb.velocity.x, s);

    }
}
