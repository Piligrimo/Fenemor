using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour
{
    public GameObject player,panel;
    Rigidbody2D rb;
    public float left, right;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target= player.transform.position;
        if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
            target = player.transform.position + new Vector3(5, 0, 0);
        if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
            target = player.transform.position - new Vector3(5, 0, 0);
        float s = 0.9f;
        if (transform.position.x - target.x > 1 && transform.position.x > left)
            rb.velocity = new Vector2(-s, 0);
        else
            rb.velocity = new Vector2(0, 0);
        if (transform.position.x - target.x < -1 && transform.position.x<right)
            rb.velocity = new Vector2(s, 0);
        
    }
}
