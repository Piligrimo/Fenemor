using UnityEngine;
using System.Collections;

public class ShieldDestroy : MonoBehaviour {
    public GameObject AuraHolder,wave;
    public float existtime, dietime;
    // Use this for initialization
    void Start()
    {
        existtime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        existtime += Time.deltaTime;
        if (existtime > dietime || AuraHolder == null)
            Destroy(gameObject);
        if (AuraHolder!=null)
        transform.position = AuraHolder.transform.position+new Vector3(0,0,-0.00001f);

    }
}
