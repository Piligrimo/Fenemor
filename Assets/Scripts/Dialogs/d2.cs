﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class d2 : MonoBehaviour {
    public Text txt;
    int i=0;
    
    bool tarap = false;
    public GameObject tarasque,portal;
    string[] dia ={"Межпространственный голос Юстуса: Я доволен тобой. Оглядись вокруг, происходящее здесь непривычно для тебя, но не стоит бояться.",
    "Юстус: В этом измерении и в этом облике твоя сила обеспечена магическими способностями, твоя мана восстанавливается быстрее.",
    "Юстус: А выносливость медленнее. Так же у тебя появилась возможность дальнего боя. Опробуй ее на тех слизнях.",
    "Подсказка: Чтобы использовать дальнюю атаку, кликните на место, куда собираетесь ее использовать.",
    "Юстус: У тебя здорово выходит! Кстати, не нужно переживать за жизни существ, что ты будешь встречать в таких мирах.",
    "Юстус: Нельзя даже сказать, что они в полной мере живые. Это материальные проекции мыслей и чувств хозяина сознания. И они не слишком хороши в большинстве случаев.",
    "Юстус: Убивая их, ты только очищаешь сознание в котором находишься. Продолжай в том же духе.",
    "Юстус: Остался последний слизень, самый крупный. Обрати внимание, внутри него что-то есть.",
    "Юстус: Как видишь, внутри большого слизня был посох. Не знаю, как он туда попал. Разберись с оставшимися слизнями и подбери посох.",
    "Юстус: Очень хорошо! Осмотри посох.",
    "Подсказка: Нажмите на кнопку, появившуюся в правом нижнем углу или клавишу \"Tab\" на клавиатуре, чтобы открыть инвентарь.",
    "Бриск: Учитель, что-то огромное вылезло из водопада!",
    "Юстус: Видимо это тараск. Он выглядит внушительно, но если знать подход , то победить его не составит труда.",
    "Юстус: Панцири делают тарасков неуязвимыми к любому урону. Однако под своей броней они довольно хилые. Ты обратил внимание, что тараск пришел только когда исчезли все слизни?",
    "Юстус: Дело в том, что эти существа знают про уязвимость тарасков и питаются ими, заползая внутрь панциря.",
    "Бриск: А как мне быть?",
    "Твой посох превращает врагов в хрюшек, которые не имеют тех способностей, что имел до превращения",
    "Юстус: Используй посох, чтобы превратить его в хрюшку, а затем наноси ему урон.",
    "Юстус: У тебя здорово выходит сражаться. Думаю, это все что ты должен был сделать здесь. Когда ты выполняешь задание внутри сознания, твой собственный разум поймет, что твое дело сделано и откроет портал в реальный мир. Заходи туда."
};
    public Button op;
    public void OnCl()
    {
        if (i != 3 && i != 6 && i != 7 && i != 8 && i != 10 && i < 17 )
        {
            i++;
            txt.text = dia[i];
        }
    }
    // Use this for initialization
	void Start () {
        txt.text = dia[0];
        op.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 4 && i < 4)
        {
            i = 4;
            txt.text = dia[i];
        }
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 1 && i < 7 && GameObject.FindGameObjectsWithTag("enemy")[0]== GameObject.Find("Ст.Эссенция"))
        {
            i = 7;
            txt.text = dia[i];
        }
        if (GameObject.Find("Ст.Эссенция") == null && i < 8)
        {
            i = 8;
            txt.text = dia[i];
        }
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 && i < 9 && GameObject.Find("Фенемор").GetComponent<BriskScript>().n==4)
        {
            i = 9;
            txt.text = dia[i];
            op.interactable = true;
        }
       
        if (GameObject.Find("Фенемор").GetComponent<MagicScript>().MagicID == 4 && i<11)
        {
                i = 11;
                GameObject clone = Instantiate(tarasque) as GameObject;
                txt.text = dia[i];
                tarap = true;
        }
        if (tarap && GameObject.FindGameObjectsWithTag("enemy").Length <= 0 && i < 18)
        {

            i = 18;
            txt.text = dia[i];
            GameObject prt = Instantiate(portal, GameObject.Find("Main Camera").transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            prt.GetComponent<Portal>().loc = 3;
        }
    }
}
