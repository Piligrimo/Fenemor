using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;
using System.Collections;


public class InvUiScript : MonoBehaviour {
    public Button[] invicon = new Button[24];
    public Image weapon, cloth, magic, UInv, inw, inm, ina, inx;
    public GameObject inventory;
    public Text[] stats = new Text[4];
    public Canvas canv;
    public Button opn;
    // Use this for initialization
    void Start () {
        CloseInventory();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab) && opn.IsInteractable())
        {
            if (canv.enabled)
                CloseInventory();
            else
                OpenInventory();
        }
    }
    public void PutItemToMain(int id)
    {
        if (GetComponent<BriskScript>() != null)
        {
            if (GetComponent<BriskScript>().itemtypes[id] == 1)
                GetComponent<WeaponScript>().WeaponID = GetComponent<BriskScript>().inv[id];
            if (GetComponent<BriskScript>().itemtypes[id] == 2)
                GetComponent<MagicScript>().MagicID = GetComponent<BriskScript>().inv[id];
            if (GetComponent<BriskScript>().itemtypes[id] == 3)
                GetComponent<ArmorScript>().ArmorID = GetComponent<BriskScript>().inv[id];
        }
        else
        {
            if (GetComponent<NewBehaviourScript>().itemtypes[id] == 1)
                GetComponent<WeaponScript>().WeaponID = GetComponent<NewBehaviourScript>().inv[id];
            if (GetComponent<NewBehaviourScript>().itemtypes[id] == 2)
                GetComponent<MagicScript>().MagicID = GetComponent<NewBehaviourScript>().inv[id];
            if (GetComponent<NewBehaviourScript>().itemtypes[id] == 3)
                GetComponent<ArmorScript>().ArmorID = GetComponent<NewBehaviourScript>().inv[id];
        }
        weapon.sprite = inw.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<WeaponScript>().WeaponID];
        magic.sprite = inm.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<MagicScript>().MagicID];
        cloth.sprite = ina.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<ArmorScript>().ArmorID];
    }
    public void OpenInventory()
    { 

        Time.timeScale = 0;
        canv.enabled = true;
        inw.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<WeaponScript>().WeaponID];
        inm.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<MagicScript>().MagicID];
        ina.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<ArmorScript>().ArmorID];
        inx.sprite = inventory.GetComponent<iconbase>().icons[0];
        for (int i=0;i<24;i++)
        {
            if (GetComponent<BriskScript>()!=null)
                invicon[i].image.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<BriskScript>().inv[i]];
            else
                invicon[i].image.sprite = inventory.GetComponent<iconbase>().icons[GetComponent<NewBehaviourScript>().inv[i]];
        }
        if (GetComponent<BriskScript>() != null)
            stats[0].text = Convert.ToString((int)GetComponent<BriskScript>().hp) + "/" + Convert.ToString((int)GetComponent<BriskScript>().mhp);
        else
            stats[0].text = Convert.ToString((int)GetComponent<NewBehaviourScript>().hp) + "/" + Convert.ToString((int)GetComponent<NewBehaviourScript>().mhp);

        stats[1].text = Convert.ToString((int)GetComponent<WeaponScript>().Stamina) + "/" + Convert.ToString((int)GetComponent<WeaponScript>().MaxStamina);
        stats[2].text = Convert.ToString((int)GetComponent<MagicScript>().Mana) + "/" + Convert.ToString((int)GetComponent<MagicScript>().MaxMana);
        if (GetComponent<BriskScript>() != null)
            stats[3].text = Convert.ToString(GetComponent<BriskScript>().damage);
        else
            stats[3].text = Convert.ToString(GetComponent<NewBehaviourScript>().damage);

    }
    public void CloseInventory()
    {
        canv.enabled = false;
        Time.timeScale = 1;
    }
}

