using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;



public class Comixscr : MonoBehaviour {

    // Use this for initialization
    public Button Prev,Next;
    public Image Image;
    public Sprite s1,s2,s3;
    public int str;
    public Canvas canva;
    public int a;
    void Start () {
        transform.localPosition = new Vector3(0, -a, 0);

    }
	
	// Update is called once per frame
	void Update () {
        if (canva.enabled)
        {
            float s = Input.GetAxis("Mouse ScrollWheel") * 100;
            float y = transform.localPosition.y;
            if (!((y < -a && s > 0) || (y > a && s < 0)))
                transform.localPosition -= new Vector3(0, s, 0);
            Prev.enabled = !(Image.sprite == s1);
        }
    }
    public void NClick()
    {
        if (Image.sprite == s1 || Image.sprite == s2)
            transform.localPosition = new Vector3(0, -a, 0);
        if (Image.sprite == s3)
        {
            Application.LoadLevel("Обучение");
            //canva.enabled = false;

        }
        if (Image.sprite == s2)
            Image.sprite = s3;
        if (Image.sprite == s1)
            Image.sprite = s2;
    }
    public void PClick()
    {
        if (Image.sprite == s3 || Image.sprite == s2)
            transform.localPosition = new Vector3(0, -a, 0);

        if (Image.sprite == s2)
            Image.sprite = s1;
        if (Image.sprite == s3)
            Image.sprite = s2;
    }
}
