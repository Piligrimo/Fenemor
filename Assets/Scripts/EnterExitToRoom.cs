using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitToRoom : MonoBehaviour {
    public int enterDir; //0-влево 1-вправо
    enum Direction : int {LEFT, RIGHT, UP, DOWN };
    public GameObject Room;
	// Use this for initialization
	void Start () {
        Room.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.name == "Фенемор")
        {
            if (other.GetComponent<Rigidbody2D>().velocity.x > 0 && enterDir == (int)Direction.RIGHT)
                Room.SetActive(true);
            if (other.GetComponent<Rigidbody2D>().velocity.x < 0 && enterDir == (int)Direction.LEFT)
                Room.SetActive(true);
            if (other.GetComponent<Rigidbody2D>().velocity.y > 0 && enterDir == (int)Direction.UP)
                Room.SetActive(true);
            if (other.GetComponent<Rigidbody2D>().velocity.y < 0 && enterDir == (int)Direction.DOWN)
                Room.SetActive(true);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Фенемор")
        {
            if (other.GetComponent<Rigidbody2D>().velocity.x < 0 && enterDir == (int)Direction.RIGHT)
                Room.SetActive(false);
            if (other.GetComponent<Rigidbody2D>().velocity.x > 0 && enterDir == (int)Direction.LEFT)
                Room.SetActive(false);
            if (other.GetComponent<Rigidbody2D>().velocity.y < 0 && enterDir == (int)Direction.UP)
                Room.SetActive(false);
            if (other.GetComponent<Rigidbody2D>().velocity.y > 0 && enterDir == (int)Direction.DOWN)
                Room.SetActive(false);
        }

    }
}
