using UnityEngine;
using System.Collections;
using System;
public class myClass
{
    public GameObject kletka;
    public string color_vladelca;
    public string name_vladelca;
    public bool kuplena;
    public bool zalogena;
    public bool uluchena; // куплен весь набор - это тру всем кто в наборе т.е. можно улучшать.
    public string nastupil;
    public bool v_torge;
    public bool k_uluchenie;

    public bool v_komplekte;

    public int cost; // цена карточки
    public int arenda; // Без акций
    public int cost_up; // цена акции
    public int[] arenda_up; // акций 1 2 3 4 5
    public myClass()
    {
        arenda_up = new int[5];
    }
    public void Arenda_up(int index,int value)
    {
        arenda_up[index] = value * 1000;
    }
}

public class Pole:MonoBehaviour
{
    public static myClass[] kletki;

    void Start()
    {
        kletki = new myClass[28];
        for (int i = 0; i < 28; i++)
        {
            kletki[i] = new myClass();
            kletki[i].kletka = GameObject.FindWithTag(i.ToString());
        }// почему не вызываем в скрипте Players  в Start - OnJoinedRoom() потому что не успевает тут все инициализироваться и поле.клетка[0] он не находит там
        kletki[1].cost = 80000;
        kletki[2].cost = 100000;
        kletki[4].cost = 120000;
        kletki[5].cost = 120000;
        kletki[6].cost = 150000;
        kletki[8].cost = 170000;
        kletki[9].cost = 180000;
        kletki[10].cost = 190000;
        kletki[11].cost = 200000;
        kletki[13].cost = 220000;
        kletki[15].cost = 250000;
        kletki[16].cost = 250000;
        kletki[18].cost = 270000;
        kletki[19].cost = 270000;
        kletki[20].cost = 300000;
        kletki[22].cost = 330000;
        kletki[23].cost = 350000;
        kletki[24].cost = 350000;
        kletki[26].cost = 400000;
        kletki[27].cost = 450000;

        kletki[1].arenda = 3000;
        kletki[2].arenda = 5000;
        kletki[4].arenda = 7000;
        kletki[5].arenda = 7000;
        kletki[6].arenda = 10000;
        kletki[8].arenda = 12000;
        kletki[9].arenda = 14000;
        kletki[10].arenda = 15000;
        kletki[11].arenda = 17000;
        kletki[13].arenda = 20000;
        kletki[15].arenda = 22000;
        kletki[16].arenda = 22000;
        kletki[18].arenda = 25000;
        kletki[19].arenda = 25000;
        kletki[20].arenda = 27000;
        kletki[22].arenda = 30000;
        kletki[23].arenda = 33000;
        kletki[24].arenda = 33000;
        kletki[26].arenda = 40000;
        kletki[27].arenda = 45000;

        kletki[1].cost_up = 50000;
        kletki[2].cost_up = 50000;
        kletki[4].cost_up = 50000;
        kletki[5].cost_up = 50000;
        kletki[6].cost_up = 50000;
        kletki[8].cost_up = 100000;
        kletki[9].cost_up = 100000;
        kletki[10].cost_up = 100000;
        kletki[11].cost_up = 100000;
        kletki[13].cost_up = 100000;
        kletki[15].cost_up = 150000;
        kletki[16].cost_up = 150000;
        kletki[18].cost_up = 150000;
        kletki[19].cost_up = 150000;
        kletki[20].cost_up = 150000;
        kletki[22].cost_up = 200000;
        kletki[23].cost_up = 200000;
        kletki[24].cost_up = 200000;
        kletki[26].cost_up = 200000;
        kletki[27].cost_up = 200000;

        kletki[1].Arenda_up(0,10);
        kletki[1].Arenda_up(1,25);
        kletki[1].Arenda_up(2, 50);
        kletki[1].Arenda_up(3, 100);
        kletki[1].Arenda_up(4, 250);
        kletki[2].Arenda_up(0, 15);
        kletki[2].Arenda_up(1, 40);
        kletki[2].Arenda_up(2, 80);
        kletki[2].Arenda_up(3, 150);
        kletki[2].Arenda_up(4, 300);
        kletki[4].Arenda_up(0, 21);
        kletki[4].Arenda_up(1, 52);
        kletki[4].Arenda_up(2, 105);
        kletki[4].Arenda_up(3, 200);
        kletki[4].Arenda_up(4, 500);
        kletki[5].Arenda_up(0, 21);
        kletki[5].Arenda_up(1, 52);
        kletki[5].Arenda_up(2, 105);
        kletki[5].Arenda_up(3, 200);
        kletki[5].Arenda_up(4, 500);
        kletki[6].Arenda_up(0, 30);
        kletki[6].Arenda_up(1, 75);
        kletki[6].Arenda_up(2, 150);
        kletki[6].Arenda_up(3, 300);
        kletki[6].Arenda_up(4, 550);
        kletki[8].Arenda_up(0, 37);
        kletki[8].Arenda_up(1, 92);
        kletki[8].Arenda_up(2, 195);
        kletki[8].Arenda_up(3, 400);
        kletki[8].Arenda_up(4, 700);
        kletki[9].Arenda_up(0, 42);
        kletki[9].Arenda_up(1, 105);
        kletki[9].Arenda_up(2, 210);
        kletki[9].Arenda_up(3, 425);
        kletki[9].Arenda_up(4, 750);
        kletki[10].Arenda_up(0, 45);
        kletki[10].Arenda_up(1, 112);
        kletki[10].Arenda_up(2, 225);
        kletki[10].Arenda_up(3, 450);
        kletki[10].Arenda_up(4, 800);
        kletki[11].Arenda_up(0, 55);
        kletki[11].Arenda_up(1, 140);
        kletki[11].Arenda_up(2, 300);
        kletki[11].Arenda_up(3, 600);
        kletki[11].Arenda_up(4, 900);
        kletki[13].Arenda_up(0, 65);
        kletki[13].Arenda_up(1, 170);
        kletki[13].Arenda_up(2, 350);
        kletki[13].Arenda_up(3, 700);
        kletki[13].Arenda_up(4, 1000);
        kletki[15].Arenda_up(0, 75);
        kletki[15].Arenda_up(1, 200);
        kletki[15].Arenda_up(2, 420);
        kletki[15].Arenda_up(3, 800);
        kletki[15].Arenda_up(4, 1100);
        kletki[16].Arenda_up(0, 75);
        kletki[16].Arenda_up(1, 200);
        kletki[16].Arenda_up(2, 420);
        kletki[16].Arenda_up(3, 800);
        kletki[16].Arenda_up(4, 1100);
        kletki[18].Arenda_up(0, 80);
        kletki[18].Arenda_up(1, 220);
        kletki[18].Arenda_up(2, 440);
        kletki[18].Arenda_up(3, 850);
        kletki[18].Arenda_up(4, 1200);
        kletki[19].Arenda_up(0, 80);
        kletki[19].Arenda_up(1, 220);
        kletki[19].Arenda_up(2, 440);
        kletki[19].Arenda_up(3, 850);
        kletki[19].Arenda_up(4, 1200);
        kletki[20].Arenda_up(0, 85);
        kletki[20].Arenda_up(1, 230);
        kletki[20].Arenda_up(2, 450);
        kletki[20].Arenda_up(3, 1000);
        kletki[20].Arenda_up(4, 1300);
        kletki[22].Arenda_up(0, 90);
        kletki[22].Arenda_up(1, 250);
        kletki[22].Arenda_up(2, 500);
        kletki[22].Arenda_up(3, 1200);
        kletki[22].Arenda_up(4, 1500);
        kletki[23].Arenda_up(0, 100);
        kletki[23].Arenda_up(1, 270);
        kletki[23].Arenda_up(2, 550);
        kletki[23].Arenda_up(3, 1500);
        kletki[23].Arenda_up(4, 1700);
        kletki[24].Arenda_up(0, 100);
        kletki[24].Arenda_up(1, 270);
        kletki[24].Arenda_up(2, 550);
        kletki[24].Arenda_up(3, 1500);
        kletki[24].Arenda_up(4, 1700);
        kletki[26].Arenda_up(0, 130);
        kletki[26].Arenda_up(1, 300);
        kletki[26].Arenda_up(2, 700);
        kletki[26].Arenda_up(3, 1700);
        kletki[26].Arenda_up(4, 2500);
        kletki[27].Arenda_up(0, 150);
        kletki[27].Arenda_up(1, 350);
        kletki[27].Arenda_up(2, 800);
        kletki[27].Arenda_up(3, 2000);
        kletki[27].Arenda_up(4, 3000);

        GameObject.Find("Canvas").GetComponent<PLayers>().OnJoinedRoom();
    }
}