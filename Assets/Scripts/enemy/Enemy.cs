using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float mhp,hp;
    public Image hpbar;
    public bool CanBeHit, InDamageArea;
    public GameObject dfx,Feny;
    // Use this for initialization
    void Start () {
        hp = mhp;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Feny.GetComponent<WeaponScript>().AOECastID == 1 && InDamageArea)
        {
            hp -= 0.3f;
            if (gameObject.name == "Мешок2")
                Feny.GetComponent<NewBehaviourScript>().hp -= 1.4f-(1.4f * Feny.GetComponent<NewBehaviourScript>().armor / 10);
            Feny.GetComponent<WeaponScript>().AOECastID =0;
        }
        hpbar.fillAmount = hp / mhp;
        if (hp < 0)
        {
              Destroy(hpbar);
              GameObject clone = Instantiate(dfx, transform.localPosition, transform.rotation) as GameObject;
              Destroy(gameObject);
        }    
	}
}
