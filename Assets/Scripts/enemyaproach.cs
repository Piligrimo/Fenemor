using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enemyaproach : MonoBehaviour {
    public float existtime, dietime;
    public GameObject enemy;
    // Use this for initialization
    void Start()
    {
        existtime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        existtime += Time.deltaTime;
        if (existtime > dietime)
        {
            GameObject clone = Instantiate(enemy, transform.position,Quaternion.Euler(0,180,0)) as GameObject;
            clone.GetComponent<EnemyAdvanced>().hpbar = clone.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>();
            clone.GetComponent<EnemyAdvanced>().Feny = GameObject.Find("Фенемор");
            Destroy(gameObject);
        }
    }
}
