using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Kletka : Photon.MonoBehaviour
{
    public GameObject fishka; // фишка
    public GameObject find_kletka; // клетка
    public Kletka activ_find_kletka;
    static public int i = 0;
    public bool zaglushka_logi_my_karta; // чтобы логи моя карта не выводилась второй раз по клику на карту

    public virtual void Povedenie(myClass Moskow_)
    {
    }
    public  void na_rastoyanii()
    { // поведение клетки по нажатию курсора игрока 
        if (GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == true && GameObject.Find("Timer").GetComponent<Timer_>().enabled == true) 
        {
            zaglushka_logi_my_karta = true;
            int i = int.Parse(gameObject.tag); // т.к. метод na_rastoyanii вызывается не через класс клетка, а через класс Moskow то в этом методе все поля клетки равны Null!!!!!
            activ_find_kletka = Pole.kletki[i].kletka.GetComponentInChildren<Kletka>();
            if (GameObject.Find("Canvas").GetComponent<Kletka>().fishka != null) // это должно быть для того чтобы когда игрок проигрывал он не мог управлять недвижимостью
                activ_find_kletka.Povedenie(Pole.kletki[i]); //GetComponent<Kletka>().fishka юнити не находит! пишет нулл!
        }
        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == false) // второй if для быстрого отключения скрипта таймера для тестов монополии
        {
            zaglushka_logi_my_karta = true;
            int i = int.Parse(gameObject.tag);
            activ_find_kletka = Pole.kletki[i].kletka.GetComponentInChildren<Kletka>();// ПОЛИМОРФИЗМ МОЖЕТ БАГАНУТЬ! НЕ НАХОДИТЬ GetComponentInChildren<Kletka>(); если теги клеток совпадают с тегами других объектов на сцене
            if (GameObject.Find("Canvas").GetComponent<Kletka>().fishka != null) 
                activ_find_kletka.Povedenie(Pole.kletki[i]); 
        }
    }
    [PunRPC]
    public void Peredvigenie_fishka_rpc(PhotonMessageInfo info)
    {
        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
        {
            GameObject.Find("Timer").GetComponent<Timer_>().kletka_open = false;
            GameObject.Find("Timer").GetComponent<Timer_>().kubik.GetComponent<Button>().interactable = false;
            GameObject.Find("Timer").GetComponent<Timer_>().torg[PhotonNetwork.player.ID - 1].GetComponent<Button>().interactable = false;
        }
        zaglushka_logi_my_karta = false;
        if (info.sender.isLocal)// sender игрок отправляет другим игрокам только СВОИ действия. sender игрок не может задавать(изменять) действия ЧУЖИХ игроков в islocal(sender их изменения увидит, а вот сами игроки не увидят как их изменил sender)
        { // поведение клетки когда на нее ступила фишка //отслеживание по файнд клетке
            GetComponent<Hod>().Peredvisenie();
            i = GetComponent<Hod>().hod;
            fishka.transform.position = Pole.kletki[i].kletka.transform.position;// передвижение фишки
            activ_find_kletka = Pole.kletki[i].kletka.GetComponentInChildren<Kletka>(); // ПОЛИМОРФИЗМ МОЖЕТ БАГАНУТЬ! НЕ НАХОДИТЬ GetComponentInChildren<Kletka>(); если теги клеток совпадают с тегами других объектов на сцене
            Pole.kletki[i].nastupil = PhotonNetwork.player.name;
            activ_find_kletka.Povedenie(Pole.kletki[i]);
            photonView.RPC("set", PhotonTargets.All, i); // т.к. некоторое поведение проверяет расположение клетки равно расположению фишки
        }
    }
    public void Peredvigenie_fishka()
    {
        photonView.RPC("Peredvigenie_fishka_rpc", PhotonTargets.All);

        //Pole.kletki[i].nastupil = "";
    }
}