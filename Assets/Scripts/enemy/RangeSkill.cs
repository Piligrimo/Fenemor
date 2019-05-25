using UnityEngine;
using System.Collections;

public class RangeSkill : MonoBehaviour {
    public float castTime,maxtime,SkillRange;
    bool csted;
    
    public GameObject longAttack,Feny;
    Animator an;
    // Use this for initialization
    void Start () {
        an = GetComponent<Animator>();
        Feny = GameObject.Find("Фенемор");
    }

    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    // Update is called once per frame
    void Update () {
        if (castTime <= 0 && dist(Feny.transform.position, transform.position) < SkillRange)
        {
            castTime = maxtime;
            csted = false;
        }
        an.SetBool("cast", !csted && castTime > 0);
        if (castTime > 0)
        {

            if (castTime < maxtime-0.5 && !csted)
            {
                GameObject clone = Instantiate(longAttack, transform.localPosition + new Vector3(0, 0.5f, 0), transform.rotation) as GameObject;
                csted = true;
            }
            castTime -= Time.deltaTime;
        }
        
    }
}
