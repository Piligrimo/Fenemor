using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MagicScript : MonoBehaviour
{
    public Texture2D pigcurs,dflt;
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    public int MagicID, BuffID,ClickModificate;
    public Image Dis, ManaScale;
    public float TimeEl, CooldownTime, Mana, MaxMana;
    public GameObject Aura,Animal,HexFx;
     // Use this for initialization
    void Start()
    {
        Dis.enabled = false;
    }
    public void MagClick()
    {
        Cast();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Cast();
        }
        if (BuffID == 1 && TimeEl < 5)
            BuffID = 0;
        if (TimeEl > 0)
        {
            TimeEl -= Time.deltaTime;
            Dis.fillAmount = TimeEl / CooldownTime;
        }
        else
            Dis.enabled = false;
        float bonus;
        if (GetComponent<NewBehaviourScript>() != null)
            bonus = 1;
        else
            bonus = 1.01f;
        if (Mana < MaxMana)
            Mana += bonus*0.0008f;
        ManaScale.fillAmount = Mana / MaxMana;
    }
    void Cast()
    {
        if (MagicID == 2 && !Dis.enabled && Mana > 5)
        {
           GameObject clone = Instantiate(Aura,transform.localPosition, transform.rotation) as GameObject;
            clone.GetComponent<ShieldDestroy>().AuraHolder = gameObject;
            Mana -= 5;
            Dis.enabled = true;
            TimeEl = CooldownTime;
            BuffID = 1;
        }
        if (MagicID == 4 && !Dis.enabled)
        {
            ClickModificate = 1;
            Cursor.SetCursor(pigcurs, new Vector2(0.33f, 0.15f), CursorMode.Auto);
        }
    }
    GameObject Choose()
    {
        
        GameObject chosen;
        if (GameObject.FindGameObjectsWithTag("enemy").Length > 0)
        {
            chosen = GameObject.FindGameObjectsWithTag("enemy")[0];
            for (int i = 1; i < GameObject.FindGameObjectsWithTag("enemy").Length; i++)
            {
                if (dist(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), chosen.transform.position) > dist(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), GameObject.FindGameObjectsWithTag("enemy")[i].transform.position))
                    chosen = GameObject.FindGameObjectsWithTag("enemy")[i];
            }
            return chosen;
        }
        else
            return null;
       
    }
    public void Hex()
    {
        Dis.enabled = true;
        CooldownTime = 6;
        TimeEl = 6;
        Cursor.SetCursor(dflt, new Vector2(0.33f, 0.15f), CursorMode.Auto);
        Mana -= 1;
        ClickModificate = 0;
        GameObject Hexed = Choose();
        Instantiate(HexFx, Hexed.transform.position, Quaternion.Euler(0, 0, 0));
        GameObject clone = Instantiate(Animal, Hexed.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
        clone.GetComponent<HexPigScript>().dietime = 5;
        clone.GetComponent<HexPigScript>().hexedOne = Hexed;
        clone.GetComponent<HexPigScript>().hp = Hexed.GetComponent<EnemyAdvanced>().hp;
        clone.GetComponent<HexPigScript>().mhp = Hexed.GetComponent<EnemyAdvanced>().mhp;
        clone.GetComponent<HexPigScript>().hpbar = clone.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>();

        Hexed.active = false;
    }
}