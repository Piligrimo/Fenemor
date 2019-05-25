using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class NewBehaviourScript : MonoBehaviour {
    
    public int dir,armor,locID,n;
    public float damage,hp, mhp,atimer = 0, ctimer = 0,speed=1;
    public bool at, go,cast,canEnter;
    public Animator anim;
    AudioSource zv;
    Rigidbody2D rb;
    public Image weapon, cloth, magic;
    public GameObject dfx,pig,inventory,hexpig;
    public Text dbg;
    public Image hpbar;
    public AudioClip footStep, attack;
    public int[] inv = new int[24];
    public int[] itemtypes = new int[24];
    string[] locs = { "Обучение", "Свинья" };
    //  int dir;
    // Use this for initialization
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    void Start()
    {
        if (Application.loadedLevel!=1)
        load("autosave");
        anim = GetComponent<Animator>();
        zv = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hp > mhp)
            hp = mhp;

        if (Input.GetKey(KeyCode.E) && canEnter)
        {
            locID = 1;
            save("autosave");
            Application.LoadLevel(locs[locID]);
        }
        if (GetComponent<WeaponScript>().cst)
            {
                GetComponent<WeaponScript>().cst = false;
                cast = true;
            }
           
        hpbar.fillAmount = hp / mhp;
        anim = GetComponent<Animator>();
        float mx = Input.GetAxis("Horizontal");
        float my = Input.GetAxis("Vertical");
        go = !(mx == 0 && my == 0);
        if (Mathf.Abs(mx) >= Mathf.Abs(my))
        {
            if (mx > 0)
                dir = 3;
            if (mx < 0)
                dir = 2;
        }
        else
        {
            if (my >= 0)
                dir = 0;
            if (my <= 0)
                dir = 1;
        }


        if (at)
        {
            atimer += Time.deltaTime;
            at = atimer < 0.8;
        }
        else
            {
                atimer = 0;
            if (cast)
                {
                    ctimer += Time.deltaTime;
                    cast = ctimer < 0.8;
                }
            else
                {
                    ctimer = 0;
                    Vector2 v = new Vector2(mx, my).normalized*speed;
                    v = new Vector2(v.x, v.y * 0.85f);
                    rb.velocity = v;
                   // rb.velocity = new Vector2(mx, my);
                    float y = transform.localPosition.y, z = transform.localPosition.z;
                    z = z - (float)(y - 1);
                    transform.localPosition -= new Vector3(0, 0, z);
                }
            }   


        if (!go)
        {
        if (!at)
        {
            if (!cast)
                dir = 5;
            else
                dir = 6;
        }
        else
            dir = 8;
        }

        zv.mute = (!go);

            
    if (hp < 0)
    {
        GameObject clone = Instantiate(dfx, transform.localPosition, transform.rotation) as GameObject;
        Destroy(gameObject);
        }

    anim.SetInteger("d", dir);
    dbg.text = Convert.ToString(dir);


    }
    public void GetDamage(float dam)
    {
        armor = 0;
        if (GetComponent<MagicScript>().BuffID == 1)
            armor += 5;
        hp -= dam - (dam * armor / 5+armor);
        GameObject.Find("Портрет").GetComponent<Portrait>().hurt = true;
        GameObject.Find("Портрет").GetComponent<Portrait>().t = 0.2f;
        if (GameObject.FindGameObjectWithTag("aura")!=null)
        {
            GameObject au = GameObject.FindGameObjectWithTag("aura");
            GameObject clone = Instantiate(au.GetComponent<ShieldDestroy>().wave, au.transform.localPosition, au.transform.rotation) as GameObject;
            clone.GetComponent<ShieldDestroy>().dietime = 2;
            clone.GetComponent<ShieldDestroy>().AuraHolder = au;
        }
    }

    public void FieldClick()
    {
        if (!at && !go && !cast && GetComponent<MagicScript>().ClickModificate==0)
            at = true;
        if (GetComponent<MagicScript>().ClickModificate == 1)
        {
            if (GetComponent<MagicScript>().Mana > 1)
                GetComponent<MagicScript>().Hex();
            else
            {
                Cursor.SetCursor(GameObject.Find("Cursor cntrl").GetComponent<CursorCntrl>().dflt, new Vector2(0.33f, 0.15f), CursorMode.Auto);
                GetComponent<MagicScript>().ClickModificate = 0;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "enemy")
        {
              other.GetComponent<EnemyAdvanced>().hp -= damage;
            float x = UnityEngine.Random.value*other.GetComponent<SpriteRenderer>().sprite.rect.size.x / 250 - other.GetComponent<SpriteRenderer>().sprite.rect.size.x / 500;
            float y= UnityEngine.Random.value * other.GetComponent<SpriteRenderer>().sprite.rect.size.y / 250;
            Vector3 v = new Vector3(x,y,-0.5f);
            GameObject hfx = Instantiate(other.GetComponent<EnemyAdvanced>().hitfx, other.transform.position+v, transform.rotation) as GameObject;
            if (other.GetComponent<EnemyAdvanced>().hurttime<=0)
            other.GetComponent<EnemyAdvanced>().hurttime = 0.2f;
        }
    }
  
    void save(string name)
    {
        FileStream fs = new FileStream("saves\\" + name + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(Convert.ToString(locID));
        sw.WriteLine(Convert.ToString(hp));
        sw.WriteLine(Convert.ToString(mhp));
        sw.WriteLine(Convert.ToString(GetComponent<WeaponScript>().Stamina));
        sw.WriteLine(Convert.ToString(GetComponent<WeaponScript>().MaxStamina));
        sw.WriteLine(Convert.ToString(GetComponent<MagicScript>().Mana));
        sw.WriteLine(Convert.ToString(GetComponent<MagicScript>().MaxMana));
        sw.WriteLine(Convert.ToString(n));
        for (int i = 0; i < n; i++)
        {
            sw.WriteLine(inv[i]);
            sw.WriteLine(itemtypes[i]);
        }
        sw.WriteLine(Convert.ToString(GetComponent<WeaponScript>().WeaponID));
        sw.WriteLine(Convert.ToString(GetComponent<MagicScript>().MagicID));
        sw.WriteLine(Convert.ToString(GetComponent<ArmorScript>().ArmorID));
        sw.Close();
    }
    void load(string name)
    {
        StreamReader sr = new StreamReader("saves\\" + name + ".txt");
        locID = Convert.ToInt32(sr.ReadLine());
        hp = (float)Convert.ToDouble(sr.ReadLine());
        mhp = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<WeaponScript>().Stamina = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<WeaponScript>().MaxStamina = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<MagicScript>().Mana = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<MagicScript>().MaxMana = (float)Convert.ToDouble(sr.ReadLine());
        n = Convert.ToInt32(sr.ReadLine());

        for (int i = 0; i < n; i++)
        {
            inv[i] = Convert.ToInt32(sr.ReadLine());
            itemtypes[i] = Convert.ToInt32(sr.ReadLine());
        }
        GetComponent<WeaponScript>().WeaponID = Convert.ToInt32(sr.ReadLine());
        GetComponent<MagicScript>().MagicID = Convert.ToInt32(sr.ReadLine());
        GetComponent<ArmorScript>().ArmorID = Convert.ToInt32(sr.ReadLine());

        weapon.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<WeaponScript>().WeaponID];
        magic.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<MagicScript>().MagicID];
        cloth.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<ArmorScript>().ArmorID];



    }
    void loadgame(string name)
    {
        StreamReader sr = new StreamReader("saves\\" + name + ".txt");
        locID = Convert.ToInt32(sr.ReadLine());
        Application.LoadLevel(locs[locID]);
        hp = (float)Convert.ToDouble(sr.ReadLine());
        mhp = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<WeaponScript>().Stamina = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<WeaponScript>().MaxStamina = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<MagicScript>().Mana = (float)Convert.ToDouble(sr.ReadLine());
        GetComponent<MagicScript>().MaxMana = (float)Convert.ToDouble(sr.ReadLine());
        n = Convert.ToInt32(sr.ReadLine());

        for (int i = 0; i < n; i++)
        {
            inv[i] = Convert.ToInt32(sr.ReadLine());
            itemtypes[i] = Convert.ToInt32(sr.ReadLine());
        }
        GetComponent<WeaponScript>().WeaponID = Convert.ToInt32(sr.ReadLine());
        GetComponent<MagicScript>().MagicID = Convert.ToInt32(sr.ReadLine());
        GetComponent<ArmorScript>().ArmorID = Convert.ToInt32(sr.ReadLine());
        weapon.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<WeaponScript>().WeaponID];
        magic.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<MagicScript>().MagicID];
        cloth.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<ArmorScript>().ArmorID];


    }
}
