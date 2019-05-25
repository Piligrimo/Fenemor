using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HexPigScript : MonoBehaviour {
    public float hp,mhp,existtime,dietime=3;
    public GameObject hexedOne;
    public Image hpbar;
    bool right = false;
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
        hpbar.fillAmount = hp / mhp;
        if (existtime > dietime)
        {
            hexedOne.active = true;  
            hexedOne.GetComponent<EnemyAdvanced>().hp = hp;
            hexedOne.transform.position = transform.position;
            Destroy(hpbar);
            Destroy(gameObject);
        }
        existtime += Time.deltaTime;
        if (hp<0)
        {
            Destroy(hpbar);
            Destroy(hexedOne);
            Destroy(gameObject);
        }
        Vector3 target = GameObject.Find("Фенемор").transform.position;
    
        GetComponent<Rigidbody2D>().velocity = -0.3f * (new Vector2((target.x - transform.position.x) / dist(target, transform.position), (target.y - transform.position.y) / dist(target, transform.position)));
        if ((right && GetComponent<Rigidbody2D>().velocity.x < 0) || (!right && GetComponent<Rigidbody2D>().velocity.x > 0))
        {
            right = !right;
            transform.Rotate(0, 180, 0);
        }

        float y = transform.localPosition.y, z = transform.localPosition.z;
        z = z - (float)(y - 1);
        transform.localPosition -= new Vector3(0, 0, z);
    }
}
