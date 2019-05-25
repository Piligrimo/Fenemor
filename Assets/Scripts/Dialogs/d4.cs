using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class d4 : MonoBehaviour {

    // Use this for initialization
    string[] dia = {
        "Юстус: Когда ты приехал, у тебя даже не было возможности осмотреть замок. Думаю, сейчас самое время это сделать сейчас.",
        "Юстус: Две двери за моей спиной ведут в тронный зал. Там Его Величество проводит большую часть своего времени, принимая важных гостей.",
        "Юстус: Если пройти по вправо от меня, можно прийти в кабинет начальника стражи и две маленьких комнаты: одна из них твоя, во второй живет прислуга.",
        "Юстус: На другом конце коридора - мой кабинет. Рядом библиотека, а напротив нее - кухня.",
        "Юстус: Через тронный зал можно пройти на второй этаж. Там большая часть спален и различные комнаты, которые король счел необходимыми в своем замке.",
        "Юстус: Советую посмотреть все комнаты. Наш замок - довольно интересное место."
    };
    GameObject ustuce;
    int i = 0;
    public  Text txt;
    public void OnCl()
    {
        if (i < dia.Length - 1)
        {
            i++;
            txt.text = dia[i];
        }
        if (i==5)
        {
            ustuce.GetComponent<NPC>().targets[1] = new Vector3(4.141f, 1.971f, 0);
            ustuce.GetComponent<NPC>().targets[1] = new Vector3(7.88f, 6.56f, 0);
            ustuce.GetComponent<NPC>().tarcount = 1;
            ustuce.GetComponent<NPC>().go = true;
        }
    }
        void Start () {
        txt.text = dia[i];
        ustuce = GameObject.Find("Юстус");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
