using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class DialogBtn : MonoBehaviour {
    public Text txt;
    string[] dia = {"Посказка: Используй клавиши WASD или стрелочки для ходьбы.",
        "Юстус: Покажи, как ты обращаешься с мечом. Подойди к манекену с с демоном и ударь по нему несколько раз.",
        "Подсказка: Подойди к манекену и кликни в его направлении несколько раз. Пронаблюдай, как убавляется шкала здоровья манекена.",
        "Юстус: Ха! А ты не так уж плох. У тебя выходит пользоваться оружием. Я хочу, чтобы ты уничтожил его.",
        "Юстус: Отлично! Я думаю, ты готов перейти к более сложным техникам боя.",
 /*5*/   "Подсказка: Взгляни на свой инвентарь. Как видишь, там находятся предметы, которые могут сильно помочь тебе в бою.",
        "Подсказка: Оружие необходимо, чтобы использовать физические навыки, при этом будет уменьшаться количество твоих очков  выносливости.",
        "Подсказка: Магический артефакт нужен для использования заклинаний. На них тратится мана. Очки маны и выносливости будут восстанавливаться со временем.",
        "Подсказка: Одежда отвечает за твою пассивную способность. Для того, чтобы одежда выполняла свою функцию достаточно просто носить ее.",
        "Юстус: Посмотри на второй манекен. Я наложил на него ауру, которая будет блокировать урон от ударов и отражать урон от навыков.",
/*10*/  "Юстус: Подойди к нему и используй на манекен свой меч. Подсказка: Кликни на иконку меча или нажми клавишу \"1\" находясь около манекена.",
        "Юстус: Вижу, что аура, действительно, тебя поранила. Обрати внимание, что благодаря рубашке тебе становится легче.",
        "Юстус: Используй свой амулет, чтобы включить защиту. Энергетический щит заблокирует часть урона от ауры манекена.",
        "Подсказка: Кликни по иконке амулета или нажми клавишу \"2\", чтобы активировать амулет.",
        "Юстус: Ты быстро учишься, я думаю, пора перейти к действительно важным вещам. Продолжим.",
/*15*/  "Юстус: Это существо нам понадобится  для следующего испытания. \n Фенемор: Я отказываюсь убивать хрюшку.\n Юстус: Не бойся, тебе и не придется.",
        "Юстус: Я хочу, чтобы ты проник в сознание свиньи. \n Фенемор: Я не уверен, что смогу, я делал это только один раз, и то - случайно...",
        "Подсказка: подойди к поросенку и нажми клавишу \"E\"."};
    public GameObject Fenemor, bag,bag2,Ustus,Shield,pig;
    public Image weapon,armor,magic;
    public Sprite sword, amulet, shirt;
    public int i = 0;
    Animator an;
    float x, y;
    float time=0;
    bool cst=false;

    public void OnCl()

    {
       
        if (i == 14)
        {
            an.SetBool("Cast", true);
            GameObject Piggy = Instantiate(pig, new Vector3(1.864f, 0.365f, -0.6198258f), transform.rotation) as GameObject;
           
            pig = Piggy;
            pig.GetComponent<MindEnter>().Feny = Fenemor;
        }
        if (i == 4)
        {
            
            cst = true;
            an.SetBool("Cast", true);
            weapon.sprite = sword;
            armor.sprite = shirt;
            magic.sprite = amulet;
            Fenemor.GetComponent<WeaponScript>().WeaponID = 1;
            Fenemor.GetComponent<MagicScript>().MagicID = 2;
            Fenemor.GetComponent<ArmorScript>().ArmorID = 3;
            Fenemor.GetComponent<NewBehaviourScript>().n = 3;
            Fenemor.GetComponent<NewBehaviourScript>().inv[0] = 1;
            Fenemor.GetComponent<NewBehaviourScript>().inv[1] = 2;
            Fenemor.GetComponent<NewBehaviourScript>().inv[2] = 3;
            Fenemor.GetComponent<NewBehaviourScript>().itemtypes[0] = 1;
            Fenemor.GetComponent<NewBehaviourScript>().itemtypes[1] = 2;
            Fenemor.GetComponent<NewBehaviourScript>().itemtypes[2] = 3;
        }
        if (i==8)
        {
            cst = true;
            an.SetBool("Cast", true);
            GameObject clone = Instantiate(Shield, bag2.transform.localPosition-new Vector3(0.25f,0,0.00001f), bag2.transform.rotation) as GameObject;
            clone.GetComponent<ShieldDestroy>().AuraHolder = bag2;
        }
        if (i == 1 || (i>3 && i != 10 && i != 13 && i<17))
            i++;
        txt.text = dia[i];
    }
	// Use this for initialization
	void Start () {
        an = Ustus.GetComponent<Animator>();
        x = Fenemor.transform.localPosition.x;
        y = Fenemor.transform.localPosition.y;
        txt.text = "Юстус: Приветствую тебя на твоей первой тренировке, Фенемор.Тебе, должно быть, раньше не приходилось драться.Ничего, я обучу тебя азам.";
    }
	
	// Update is called once per frame
	void Update ()
    {
   
        if (Fenemor==null)
            txt.text= "Юстус: Ты самый глупый из всех, кого я когда либо учил.";
        if (i == 10 && Fenemor.GetComponent<NewBehaviourScript>().hp < Fenemor.GetComponent<NewBehaviourScript>().mhp)
        {
            i++;
            txt.text = dia[i];
        }
        if (cst)
            time += Time.deltaTime;
        if (time>0.6)
        {
            time = 0;
            cst = false;
        }
    an.SetBool("Cast", cst);
        if (i == 0 && Mathf.Abs(Fenemor.transform.localPosition.x - x) + Mathf.Abs(Fenemor.transform.localPosition.y - y) > 1.5)
        {
            i++;
            txt.text = dia[i];
        }
        if (bag2==null && i< 14)
        {
            i = 14;
            txt.text = dia[i];
        }
        if (bag != null)
        {
            if ((i == 1 || i == 2) && (bag.GetComponent<EnemyAdvanced>().hp < 0.5* bag.GetComponent<EnemyAdvanced>().hp))
            {

                i = 3;
                txt.text = dia[i];
            }
        }
        else
         if (i==3)
          {
            i++;
            txt.text = dia[i];
        }
    }
}
