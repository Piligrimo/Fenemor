using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAdvanced : MonoBehaviour {
    public float mhp, hp;
    public Image hpbar;
    public bool CanBeHit, InDamageArea,right = true;
    public GameObject dfx, Feny, ess,loot, hitfx;
    Vector3 target;
    Animator an;
    Rigidbody2D rb;
    public float speed;
    public int enemyID;
    public float TimeForAttack=1, x,damage,hurttime,AtkRange=0.4f,lastAtackGot;
    float atTime;
    bool fight=false,atkd,tookdmg;
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt(0.7225f * (y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    // Use this for initialization
    void Start() {
        hp = mhp;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        target = transform.position;
        
    }

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update() {
        InDamageArea = dist(Feny.transform.position, transform.position) < 1;
        an.SetBool("go", rb.velocity.x != 0 && rb.velocity.y != 0);
        an.SetBool("atk", atTime > 0);
        if (hurttime > 0)
            hurttime -= Time.deltaTime;
        if (atTime <= 0 && dist(Feny.transform.position, transform.position) < AtkRange)
        {
            atTime = TimeForAttack;
            atkd = false;
        }

        if (atTime > 0)
        {
            if (atTime < 0.5*TimeForAttack && !atkd && damage>0)
            {
                if (Feny.GetComponent<NewBehaviourScript>() == null)
                    Feny.GetComponent<BriskScript>().GetDamage(damage);
                else
                    Feny.GetComponent<NewBehaviourScript>().GetDamage(damage);
                atkd = true;
            }
            atTime -= Time.deltaTime;
        }
       
        if (dist(target, transform.position) > 0.3)
        {
            rb.velocity = new Vector2(speed * (target.x - transform.position.x) / dist(target, transform.position), 0.85f*speed * (target.y - transform.position.y) / dist(target, transform.position));
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
           
        }
        if (fight || lastAtackGot>0)
            {
                target = Feny.transform.position;
            }
            else
            {
                if (Random.value  > 0.993)
                    target = transform.position + new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, 0);
            }
            fight = dist(transform.position, Feny.transform.position )<3;
        float y = transform.localPosition.y, z = transform.localPosition.z;
        z = z - (float)(y - 1);
        transform.localPosition -= new Vector3(0, 0, z);
        x = dist(Feny.transform.position, transform.position);
        if ((right && rb.velocity.x < 0) || (!right && rb.velocity.x > 0))
        {
            flip();
            right = !right;
        }
        if (lastAtackGot>0)
        {
            lastAtackGot -= Time.deltaTime;
        }
        hpbar.fillAmount = hp / mhp;
        if (hp < 0)
        {
            Destroy(hpbar);
            if (enemyID==3)
            {
                spawn(transform.position + new Vector3(0.3f, 0, 0));
                spawn(transform.position - new Vector3(0.3f, 0, 0));
                spawn(transform.position + new Vector3(0, 0.3f, 0));
                spawn(transform.position - new Vector3(0, 0.3f, 0));
                GameObject lootItem = Instantiate(loot, transform.localPosition, transform.rotation) as GameObject;
            }
            GameObject clone = Instantiate(dfx, transform.localPosition, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
        if (enemyID == 2 )
        {
            hpbar.transform.localPosition = new Vector3(0, GetComponent<SpriteRenderer>().sprite.rect.size.y / 80-.5f, 0);
        }
 
    }
    void flip()
    {
        transform.Rotate(0, 180, 0);
    }
    void spawn(Vector3 place)
    {
        GameObject unit = Instantiate(ess,place, Feny.transform.rotation) as GameObject;
        unit.GetComponent<EnemyAdvanced>().hpbar= unit.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>();
        unit.GetComponent<EnemyAdvanced>().Feny = Feny;

    }
}
