using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class WeaponScript : MonoBehaviour {
    public int WeaponID, AOECastID;
    public Image Dis,StaminaScale;
    public bool cst;
    public float TimeEl, CooldownTime,Stamina,MaxStamina;
    public GameObject swFX;
	// Use this for initialization
	void Start () {
        Dis.enabled = false;
    }
    public void WeapClick()
    {
        Cast();
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cast();
        }
        if (TimeEl > 0)
        {
            TimeEl -= Time.deltaTime;
            Dis.fillAmount = TimeEl / CooldownTime;
        }
        else
            Dis.enabled = false;
        float bonus;
        if (GetComponent<NewBehaviourScript>() == null)
            bonus = 1;
        else
            bonus = 1.01f;
        if (Stamina < MaxStamina)
            Stamina += bonus*0.008f;
        StaminaScale.fillAmount = Stamina / MaxStamina;
     }
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    void Cast()
    {
        if (WeaponID == 1 && !Dis.enabled && Stamina > 5)
        {
            GameObject[] ens = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < ens.Length-1; i++)
            {
               
                
                if (dist(ens[i].transform.position, transform.position) < 1.5)
                {
                    Instantiate(swFX,ens[i].transform.position,Quaternion.Euler(0,0,Random.value*360));
                    ens[i].GetComponent<EnemyAdvanced>().hp -= 0.7f;
                    if (ens[i].GetComponent<EnemyAdvanced>().enemyID == 1)
                    {
                        GetComponent<NewBehaviourScript>().GetDamage(2f);
                    }
                }

            }
            TimeEl = CooldownTime;
            Dis.enabled = true;
            Stamina -= 5;

            cst = true;
        }
    }  
}
