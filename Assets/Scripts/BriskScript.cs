using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class BriskScript : MonoBehaviour {
    public int dir,armor,locID;
    public Camera cam;
    public float damage,hp,mhp,slow=1,slowtime,speed=1;
    public bool at, go,cast;
    public int n;
    public float atimer = 0,ctimer=0;
    public Image weapon, cloth, magic;
    public Animator anim;
    AudioSource zv;
    Rigidbody2D rb;
    public GameObject target,Void,dfx, ball,inventory,hexpig;
    public Text dbg;
    public AudioClip ballrelize;
    public Image hpbar,port;
    int dir0;
    Vector3 pnt,PrevPos;
    public int[] inv = new int[24];
    public int[] itemtypes = new int[24];//1- оружие 2-магия 3-одежда
    string[] locs = { "Обучение", "Свинья" };
    
    // Use this for initialization
   
   
    float dist(Vector3 v1, Vector3 v2)
    {
        float x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y, r = Mathf.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
        return r;
    }
    void Start()
    {
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

        if (GetComponent<WeaponScript>().cst)
            {
                GetComponent<WeaponScript>().cst = false;
                cast = true;
            }
        armor = 0;
        if (slowtime <= 0)
            slow = 1;
        else
            slowtime -= Time.deltaTime;
        if (GetComponent<MagicScript>().BuffID == 1)
            armor += 5;
        hpbar.fillAmount = hp / mhp;
        anim = GetComponent<Animator>();
        float mx = Input.GetAxis("Horizontal")/slow;
        float my = Input.GetAxis("Vertical")/slow;
        go = !(mx == 0 && my == 0);
        
        if (dir != 7)
            dir0 = dir;
        if (atimer> 1)
        {
            zv.clip = ballrelize;
            zv.Play();
            at = false;
            GameObject b = Instantiate(ball, transform.position+new Vector3(0,0.83f,0), transform.rotation) as GameObject;
            b.GetComponent<BallScript>().target = pnt;

        }
        if (at)
            {
            rb.velocity = new Vector2(0, 0);
            atimer += Time.deltaTime;
            dir = 7;
             }
            else
                {
                    dir = dir0;
                    atimer = 0;
                if (cast)
                    {
                        ctimer += Time.deltaTime;
                        cast = ctimer < 1.2;
                        dir = 6;
                    }
                else
                    {
                        if (dir==6)
                            dir = 1;
                        ctimer = 0;
                        Vector2 v = new Vector2(mx, my).normalized * speed;
                        v = new Vector2(v.x, v.y * 0.85f);
                        rb.velocity = v;
                        rb.velocity = new Vector2(mx, my);
                        float y = transform.localPosition.y, z = transform.localPosition.z;
                        z = z - (float)(y - 1);
                        transform.localPosition -= new Vector3(0, 0, z);
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
            }
           }
        

        //            zv.mute = (!go);



        if (hp < 0)
        {
           GameObject death = Instantiate(dfx, transform.localPosition, transform.rotation) as GameObject;
            Destroy(gameObject);
         }

        anim.SetInteger("d", dir);
        dbg.text = Convert.ToString(dir);
        PrevPos = transform.position;

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            target = other.gameObject;
        }
    }

    public void FieldClick()
    {
        if (GetComponent<MagicScript>().ClickModificate == 0)
        {
            pnt = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(transform.position.x - pnt.x) > Mathf.Abs(transform.position.y - pnt.y))
            {
                if (transform.position.x - pnt.x > 0)
                    dir = 2;
                else
                    dir = 3;

            }
            else
                if (transform.position.y - pnt.y > 0)
                dir = 1;
            else
                dir = 0;
            anim.SetInteger("d", dir);
            if (!at)
                at = true;
        }
        if (GetComponent<MagicScript>().ClickModificate == 1 )
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

    public void GetDamage(float dam)
    {
        armor = 0;
        if (GetComponent<MagicScript>().BuffID == 1)
            armor += 5;
        hp -= dam - (dam * armor / (5 + armor));
        GameObject.Find("Портрет").GetComponent<Portrait>().hurt = true;
        GameObject.Find("Портрет").GetComponent<Portrait>().t = 0.2f;
        if (GameObject.FindGameObjectWithTag("aura") != null)
        {
            GameObject au = GameObject.FindGameObjectWithTag("aura");
            GameObject clone = Instantiate(au.GetComponent<ShieldDestroy>().wave, au.transform.localPosition, au.transform.rotation) as GameObject;
            clone.GetComponent<ShieldDestroy>().dietime = 2;
            clone.GetComponent<ShieldDestroy>().AuraHolder = au;
        }
    }
   public void save(string name)
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
            inv[i]=Convert.ToInt32(sr.ReadLine());
            itemtypes[i] = Convert.ToInt32(sr.ReadLine());
        }
        GetComponent<WeaponScript>().WeaponID=Convert.ToInt32(sr.ReadLine());
        GetComponent<MagicScript>().MagicID= Convert.ToInt32(sr.ReadLine());
        GetComponent<ArmorScript>().ArmorID= Convert.ToInt32(sr.ReadLine());
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
