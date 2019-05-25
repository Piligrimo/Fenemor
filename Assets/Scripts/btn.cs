using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
public class btn : MonoBehaviour
{
    public Text txt;

   int at = 0;
   public void OnClick()
    {
        txt.text = "Kjk";
    }



    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (at > 0)
            at--;
    }

}
