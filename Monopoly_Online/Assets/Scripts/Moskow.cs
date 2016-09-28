using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Moskow : Kletka
{
    public GameObject Moskow_;
    public GameObject okno_moskow;
    public GameObject okno_auk;
    public GameObject prodat;
    public GameObject zalog;
    public GameObject kupit;
    public GameObject aukcion;
    public GameObject oplatit;
    public GameObject vikup;
    public GameObject cost_up;
    public GameObject sdatsya;

    public GameObject okno_torga;
    public GameObject[] kletki_torga;
    public GameObject[] kletki_torga_2;
    public string color_kletki_torga_2;
    public int s = 0;
    public int f = 0;


    public GameObject name_left;
    public GameObject valuta_left;
    public GameObject name_right;
    public GameObject valuta_right;

    public GameObject prinat;
    public GameObject otpravit;
    public string color_activ;
    public InputField va_torg_left;
    public InputField va_torg_right;
    public int dve_sdelki;

    public Text name_firm;
    public Image firm;
    public Text prise;
    public Text zalog_firm;
    public Text dohod;

    public Sprite firm_1;
    public Sprite firm_2;
    public Sprite firm_3;
    public Sprite firm_4;
    public Sprite firm_5;
    public Sprite firm_6;
    public Sprite firm_7;
    public Sprite firm_8;
    public Sprite firm_9;
    public Sprite firm_10;
    public Sprite firm_11;
    public Sprite firm_12;
    public Sprite firm_13;
    public Sprite firm_14;
    public Sprite firm_15;
    public Sprite firm_16;
    public Sprite firm_17;
    public Sprite firm_18;
    public Sprite firm_19;
    public Sprite firm_20;
    Dictionary<string, Sprite> openfirm = new Dictionary<string, Sprite>();

    public GameObject proigrish;

    void Start()
    {
        dve_sdelki = 0;
        cost_up = GameObject.Find("Multi").GetComponent<Moskow>().cost_up;
        kupit = GameObject.Find("Multi").GetComponent<Moskow>().kupit;
        prodat = GameObject.Find("Multi").GetComponent<Moskow>().prodat;
        zalog = GameObject.Find("Multi").GetComponent<Moskow>().zalog;
        aukcion = GameObject.Find("Multi").GetComponent<Moskow>().aukcion;
        oplatit = GameObject.Find("Multi").GetComponent<Moskow>().oplatit;
        vikup = GameObject.Find("Multi").GetComponent<Moskow>().vikup;
        name_firm = GameObject.Find("Multi").GetComponent<Moskow>().name_firm;
        firm = GameObject.Find("Multi").GetComponent<Moskow>().firm;
        prise = GameObject.Find("Multi").GetComponent<Moskow>().prise;
        zalog_firm = GameObject.Find("Multi").GetComponent<Moskow>().zalog_firm;
        dohod = GameObject.Find("Multi").GetComponent<Moskow>().dohod;
        sdatsya = GameObject.Find("Multi").GetComponent<Moskow>().sdatsya;

        firm_1 = GameObject.Find("Multi").GetComponent<Moskow>().firm_1;
        firm_2 = GameObject.Find("Multi").GetComponent<Moskow>().firm_2;
        firm_3 = GameObject.Find("Multi").GetComponent<Moskow>().firm_3;
        firm_4 = GameObject.Find("Multi").GetComponent<Moskow>().firm_4;
        firm_5 = GameObject.Find("Multi").GetComponent<Moskow>().firm_5;
        firm_6 = GameObject.Find("Multi").GetComponent<Moskow>().firm_6;
        firm_7 = GameObject.Find("Multi").GetComponent<Moskow>().firm_7;
        firm_8 = GameObject.Find("Multi").GetComponent<Moskow>().firm_8;
        firm_9 = GameObject.Find("Multi").GetComponent<Moskow>().firm_9;
        firm_10 = GameObject.Find("Multi").GetComponent<Moskow>().firm_10;
        firm_11 = GameObject.Find("Multi").GetComponent<Moskow>().firm_11;
        firm_12 = GameObject.Find("Multi").GetComponent<Moskow>().firm_12;
        firm_13 = GameObject.Find("Multi").GetComponent<Moskow>().firm_13;
        firm_14 = GameObject.Find("Multi").GetComponent<Moskow>().firm_14;
        firm_15 = GameObject.Find("Multi").GetComponent<Moskow>().firm_15;
        firm_16 = GameObject.Find("Multi").GetComponent<Moskow>().firm_16;
        firm_17 = GameObject.Find("Multi").GetComponent<Moskow>().firm_17;
        firm_18 = GameObject.Find("Multi").GetComponent<Moskow>().firm_18;
        firm_19 = GameObject.Find("Multi").GetComponent<Moskow>().firm_19;
        firm_20 = GameObject.Find("Multi").GetComponent<Moskow>().firm_20;

        openfirm.Add("Jim_Beam", firm_1);
        openfirm.Add("Jack_Daniels", firm_2);
        openfirm.Add("NTV", firm_3);
        openfirm.Add("STS", firm_4);
        openfirm.Add("TNT", firm_5);
        openfirm.Add("D&G", firm_6);
        openfirm.Add("Nike", firm_7);
        openfirm.Add("Adidas", firm_8);
        openfirm.Add("Lukoyl", firm_9);
        openfirm.Add("Gazprom", firm_10);
        openfirm.Add("LG", firm_11);
        openfirm.Add("Samsung", firm_12);
        openfirm.Add("VK", firm_13);
        openfirm.Add("Odnoklasniki", firm_14);
        openfirm.Add("Facebook", firm_15);
        openfirm.Add("Nissan", firm_16);
        openfirm.Add("BMW", firm_17);
        openfirm.Add("Mercedes", firm_18);
        openfirm.Add("Apple", firm_19);
        openfirm.Add("Microsoft", firm_20);
        proigrish = GameObject.Find("Multi").GetComponent<Moskow>().proigrish;
    }
    public void peredacha_auka()
    {
        GameObject.Find("Timer").GetComponent<Timer_>().kubik.GetComponent<Button>().interactable = false;
        dve_sdelki++;
        if (dve_sdelki == 2)
        {
            dve_sdelki = 0;
            GameObject.Find("Timer").GetComponent<Timer_>().torg[PhotonNetwork.player.ID - 1].GetComponent<Button>().interactable = false;
        }

        string name_ = name_right.GetComponent<Text>().text;

        GameObject.Find("Multi").GetComponent<PhotonView>().RPC("peredacha_auka_", PhotonTargets.All, -1, name_, va_torg_left.text, va_torg_right.text);
    }
    [PunRPC]
    public void peredacha_auka_(int i,string name_,string valut_torg_left,string valut_torg_right,PhotonMessageInfo info)
    {
        if(name_!="")
        {
            //"[Имя игрока] предложил сделку [имя игрока2]."
            string logi_stroka = info.sender.name + ":" + " предложил сделку "+name_;
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
        }
        okno_torga.GetComponent<Moskow>().color_activ = info.sender.customProperties["color"].ToString();
        string color = info.sender.customProperties["color"].ToString();

        if (i != -1)
        {
            if (color == Pole.kletki[i].color_vladelca && Pole.kletki[i].v_torge == false)
            {
                name_left.GetComponent<Text>().text = info.sender.name;
                valuta_left.GetComponent<Text>().text = info.sender.customProperties["valuta"].ToString();
                Pole.kletki[i].v_torge = true;
                s = okno_torga.GetComponent<Moskow>().s;
                okno_torga.GetComponent<Moskow>().kletki_torga[s].GetComponent<Image>().sprite = Pole.kletki[i].kletka.GetComponent<Image>().sprite;
                okno_torga.GetComponent<Moskow>().kletki_torga[s].name = Pole.kletki[i].kletka.name;// для вывода клеток когда сделка прошла успешна

                GameObject copy = Instantiate(Pole.kletki[i].kletka.transform.GetChild(0).gameObject, okno_torga.GetComponent<Moskow>().kletki_torga[s].transform.position, Quaternion.identity) as GameObject;
                copy.transform.SetParent(okno_torga.GetComponent<Moskow>().kletki_torga[s].transform);
                copy.GetComponent<RectTransform>().offsetMax = new Vector2(okno_torga.GetComponent<Moskow>().kletki_torga[s].GetComponent<RectTransform>().rect.width, okno_torga.GetComponent<Moskow>().kletki_torga[s].GetComponent<RectTransform>().rect.height);
                copy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                copy.transform.position = okno_torga.GetComponent<Moskow>().kletki_torga[s].transform.position;  


                okno_torga.GetComponent<Moskow>().kletki_torga[s++].SetActive(true);
                okno_torga.GetComponent<Moskow>().s = s;
            }
            else if (Pole.kletki[i].color_vladelca != null && Pole.kletki[i].v_torge == false)
            {
                if (okno_torga.GetComponent<Moskow>().color_kletki_torga_2 == "")
                    okno_torga.GetComponent<Moskow>().color_kletki_torga_2 = Pole.kletki[i].color_vladelca;

                if (Pole.kletki[i].color_vladelca == okno_torga.GetComponent<Moskow>().color_kletki_torga_2)
                {
                    name_right.GetComponent<Text>().text = Pole.kletki[i].name_vladelca;
                    valuta_right.GetComponent<Text>().text = GameObject.Find("Valuta_" + Pole.kletki[i].color_vladelca).GetComponent<Text>().text;
                    Pole.kletki[i].v_torge = true;
                    f = okno_torga.GetComponent<Moskow>().f;
                    okno_torga.GetComponent<Moskow>().kletki_torga_2[f].GetComponent<Image>().sprite = Pole.kletki[i].kletka.GetComponent<Image>().sprite;
                    okno_torga.GetComponent<Moskow>().kletki_torga_2[f].name = Pole.kletki[i].kletka.name;// для вывода клеток когда сделка прошла успешна

                    GameObject copy = Instantiate(Pole.kletki[i].kletka.transform.GetChild(0).gameObject, okno_torga.GetComponent<Moskow>().kletki_torga_2[f].transform.position, Quaternion.identity) as GameObject;
                    copy.transform.SetParent(okno_torga.GetComponent<Moskow>().kletki_torga_2[f].transform);
                    copy.GetComponent<RectTransform>().offsetMax = new Vector2(okno_torga.GetComponent<Moskow>().kletki_torga_2[f].GetComponent<RectTransform>().rect.width, okno_torga.GetComponent<Moskow>().kletki_torga_2[f].GetComponent<RectTransform>().rect.height);
                    copy.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    copy.transform.position = okno_torga.GetComponent<Moskow>().kletki_torga_2[f].transform.position;  

                    okno_torga.GetComponent<Moskow>().kletki_torga_2[f++].SetActive(true);
                    okno_torga.GetComponent<Moskow>().f = f;
                }
            }
        }
        else
        {
            if (PhotonNetwork.player.name == name_)
            {
                info.sender.SetCustomProperties(new Hashtable() { { "hodit", "false" } }); // тот кто отправил окно сделки
                PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "hodit", "true" } }); // тот кто получил окно сделки

                okno_torga.SetActive(true);

                otpravit.SetActive(false);
                prinat.SetActive(true);
                GameObject.Find("val_torg_left").GetComponent<InputField>().text = valut_torg_left;
                GameObject.Find("val_torg_right").GetComponent<InputField>().text = valut_torg_right;
                GameObject.Find("val_torg_left").GetComponent<InputField>().enabled = false;
                GameObject.Find("val_torg_right").GetComponent<InputField>().enabled = false;
            }
        }
    }
    public void activ_sdelka()
    {
        prinat.SetActive(false);
        //PhotonNetwork.player.customProperties["color"].ToString() = okno_torga.GetComponent<Moskow>().color_kletki_torga_2; // цвета одинаковые!! должны быть разные!НЕ ЗАБЫВАТЬ!!!
        GameObject.Find("Multi").GetComponent<PhotonView>().RPC("torg_zamena", PhotonTargets.All, color_activ, okno_torga.GetComponent<Moskow>().color_kletki_torga_2, name_left.GetComponent<Text>().text, va_torg_left.text, va_torg_right.text);
        
        //если сделка принимается то меняем местами у игроков свойство hodit(чтобы удалили того кого надо) и обновляем таймер
        GameObject.Find("Multi").GetComponent<PhotonView>().RPC("otchistka_torga", PhotonTargets.All);

    }
    [PunRPC]
    public void otchistka_torga(PhotonMessageInfo info)
    {
        GameObject.Find("Timer").GetComponent<Timer_>().timer_ = GameObject.Find("Timer").GetComponent<Timer_>().timer_value; // обновляем таймер
        if (PhotonNetwork.player.name == name_left.GetComponent<Text>().text)
        {
            GameObject.Find("Timer").GetComponent<Timer_>().kubik.GetComponent<Button>().interactable = true;
            PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "hodit", "true" } });
        }
        info.sender.SetCustomProperties(new Hashtable() { { "hodit", "false" } }); 

        //"[Имя игрока] заключает торговую сделку с [Имя игрока2]. Условия: [список игрока 1] за [список игрока 2]."
        string logi_stroka = name_left.GetComponent<Text>().text + " заключает торговую сделку с " + info.sender.name+".Условия: "+"";
        foreach (GameObject klet in okno_torga.GetComponent<Moskow>().kletki_torga)
        {
            if (klet.name != "Imag")
            {
                print(klet.name);
                logi_stroka += klet.name;
            }
        }
        logi_stroka += " за ";
        foreach (GameObject klet in okno_torga.GetComponent<Moskow>().kletki_torga_2)
        {
            if (klet.name != "Imag")
            {
                print(klet.name);
                logi_stroka += klet.name;
            }
        }
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

        okno_torga.GetComponent<Moskow>().vihod_torg();
    }
    [PunRPC]
    public void logi_otkaz_torg(PhotonMessageInfo info)
    {
        GameObject.Find("Timer").GetComponent<Timer_>().timer_ = GameObject.Find("Timer").GetComponent<Timer_>().timer_value; // обновляем таймер
        if (PhotonNetwork.player.name == name_left.GetComponent<Text>().text)
        {
            GameObject.Find("Timer").GetComponent<Timer_>().kubik.GetComponent<Button>().interactable = true;
            PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "hodit", "true" } });
        }
        info.sender.SetCustomProperties(new Hashtable() { { "hodit", "false" } }); 

        //"[Имя игрока2] отказался от сделки предложенной [имя игрока]."
        string logi_stroka = info.sender.name + " отказался от сделки предложенной " + name_left.GetComponent<Text>().text;
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
    }
    public void vihod_torg()
    {
        
        if(prinat.activeInHierarchy==true)
        {
            GameObject.Find("Multi").GetComponent<PhotonView>().RPC("logi_otkaz_torg", PhotonTargets.All);
        }
        
        s = 0;
        f = 0;
        for (int i = 0; i < 28; i++)
        {
            if (Pole.kletki[i].v_torge == true)
            {
                Pole.kletki[i].v_torge = false;
                int deti = okno_torga.GetComponent<Moskow>().kletki_torga[s].transform.childCount;
                for (int de = 0; de < deti; de++)
                {
                    if (okno_torga.GetComponent<Moskow>().kletki_torga[s].activeInHierarchy == true)
                    {
                        print(okno_torga.GetComponent<Moskow>().kletki_torga[s].transform.GetChild(de).gameObject.name);
                        Destroy(okno_torga.GetComponent<Moskow>().kletki_torga[s].transform.GetChild(de).gameObject);
                    }
                }

                deti = okno_torga.GetComponent<Moskow>().kletki_torga_2[f].transform.childCount;
                for (int de = 0; de < deti; de++)
                {
                    if (okno_torga.GetComponent<Moskow>().kletki_torga_2[f].activeInHierarchy == true)
                    {
                        print(okno_torga.GetComponent<Moskow>().kletki_torga_2[f].transform.GetChild(de).gameObject.name);
                        Destroy(okno_torga.GetComponent<Moskow>().kletki_torga_2[f].transform.GetChild(de).gameObject);
                    }
                }

                okno_torga.GetComponent<Moskow>().kletki_torga[s++].SetActive(false);
                okno_torga.GetComponent<Moskow>().kletki_torga_2[f++].SetActive(false);
            }            
        }
        okno_torga.GetComponent<Moskow>().s = 0;// для того чтобы заполнять от начала новый тор клетками
        okno_torga.GetComponent<Moskow>().f = 0;
        okno_torga.GetComponent<Moskow>().color_kletki_torga_2 = "";

        name_right.GetComponent<Text>().text = "";
        valuta_right.GetComponent<Text>().text = "";
        name_left.GetComponent<Text>().text = "";
        valuta_left.GetComponent<Text>().text = "";
        va_torg_left.text = "";
        va_torg_right.text = "";

        if (prinat.activeInHierarchy == true)
        {
            otpravit.SetActive(true);
            prinat.SetActive(false);
        }
    }
    public void obnov_okno_moskow(int i)
    {
        name_firm.text = Pole.kletki[i].kletka.name;
        firm.GetComponent<Image>().sprite = openfirm[Pole.kletki[i].kletka.name];
        prise.text = "Стоимость: " + Pole.kletki[i].cost.ToString();
        zalog_firm.text = "Залог: " + (Pole.kletki[i].cost / 2).ToString();
        if (Pole.kletki[i].kuplena == true && Pole.kletki[i].k_uluchenie == false)
            dohod.text = "Доход: " + Pole.kletki[i].arenda.ToString();
        else if (Pole.kletki[i].k_uluchenie == true)
        {
            dohod.text = "Доход: " + (Pole.kletki[i].arenda_up[int.Parse(Pole.kletki[i].kletka.transform.GetChild(1).name[0].ToString()) - 1]).ToString();
        }
        else dohod.text = "Доход: ";


    }
    public override void Povedenie(myClass Moskow_)
    {
        GameObject.Find("Multi").GetComponent<Multi>().ne_oplatil.SetActive(false);

        string color = GameObject.Find("Canvas").GetComponent<Kletka>().fishka.GetComponent<Image>().sprite.name.ToString();
        kletki_torga = new GameObject[8];
        kletki_torga_2 = new GameObject[8];
        if (GameObject.Find("Okno_torg") != null)
        {
            va_torg_left = GameObject.Find("Multi").GetComponent<Moskow>().va_torg_left; //иначе он не видит va_torg_left на клетке на которой скрипт Moskow
            va_torg_right = GameObject.Find("Multi").GetComponent<Moskow>().va_torg_right;
            GameObject.Find("Multi").GetComponent<PhotonView>().RPC("peredacha_auka_", PhotonTargets.All, int.Parse(Moskow_.kletka.tag), "", va_torg_left.text, va_torg_right.text);
        }

        int valuta_igroka = Convert.ToInt32(PhotonNetwork.player.customProperties["valuta"]);// для того чтобы нельзя было покупать когда закончилась валюта

        if (GameObject.Find("Okno_kazino") != null) GameObject.Find("Okno_kazino").SetActive(false);

    
        okno_moskow.SetActive(true);
        obnov_okno_moskow(int.Parse(Moskow_.kletka.tag));

        GameObject.Find("Okno_moskow").GetComponent<Moskow>().Moskow_ = Moskow_.kletka;

        if (Moskow_.kuplena == true && color == Moskow_.color_vladelca)
        {//своя клетка
            if (Moskow_.uluchena == true)
            {
                cost_up.SetActive(true);
            }
            else
                cost_up.SetActive(false);

            if (Moskow_.k_uluchenie == false && valuta_igroka < Moskow_.cost_up)
            {
                cost_up.SetActive(false);
            }

            if (Moskow_.kletka.transform.childCount > 1)
            {
                if (Moskow_.kletka.transform.GetChild(1).name[0] == '5')
                    cost_up.SetActive(false);
                else if (Moskow_.k_uluchenie == true && valuta_igroka < Moskow_.cost_up)
                {
                    cost_up.SetActive(false);
                }
            }
            if (Moskow_.nastupil == PhotonNetwork.player.name)
            {
                cost_up.SetActive(false);
            }
            kupit.SetActive(false);
            if (Moskow_.nastupil != PhotonNetwork.player.name)
                prodat.SetActive(true);
            else
                prodat.SetActive(false);
            if (Moskow_.k_uluchenie == false && Moskow_.nastupil != PhotonNetwork.player.name)
                zalog.SetActive(true);
            else
                zalog.SetActive(false);
            aukcion.SetActive(false);
            oplatit.SetActive(false);
            vikup.SetActive(false);
            sdatsya.SetActive(false);
            if (Moskow_.nastupil == PhotonNetwork.player.name && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
            {
                Moskow_.kletka.GetComponent<Multi>().my_kletka();// вывод логи то что попал на свою купленную клетку
                if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
                    hod_next_igrok();
            }
        }
        else if (Moskow_.zalogena == true && color == Moskow_.color_vladelca)
        {//своя заложенная клетка
            cost_up.SetActive(false);
            kupit.SetActive(false);
            prodat.SetActive(false);
            zalog.SetActive(false);
            aukcion.SetActive(false);
            oplatit.SetActive(false);
            sdatsya.SetActive(false);
            if (valuta_igroka >= Moskow_.cost && Moskow_.nastupil != PhotonNetwork.player.name)
                vikup.SetActive(true);
            else
            { // если ступили на заложенную свою клетку то пропускаем ход
                vikup.SetActive(false);
            }

            if (Moskow_.nastupil == PhotonNetwork.player.name && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
            {
                Moskow_.kletka.GetComponent<Multi>().my_kletka();// вывод логи то что попал на свою купленную клетку
                if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
                    hod_next_igrok();
            }
        }
        else if (Moskow_.nastupil == PhotonNetwork.player.name &&
            (Moskow_.kuplena == false && Moskow_.zalogena == false) &&
            valuta_igroka >= Moskow_.cost)// для того чтобы нельзя было покупать когда закончилась валюта
        {// наступили на пустаю клетку и можем купить
            cost_up.SetActive(false);
            kupit.SetActive(true);
            prodat.SetActive(false);
            aukcion.SetActive(true);
            oplatit.SetActive(false);
            vikup.SetActive(false);
            sdatsya.SetActive(false);
        }
        else if (Moskow_.color_vladelca != null && color != Moskow_.color_vladelca)
        {//чужая клетка
            cost_up.SetActive(false);
            kupit.SetActive(false);
            prodat.SetActive(false);
            zalog.SetActive(false);
            aukcion.SetActive(false);
            vikup.SetActive(false);
            if (Moskow_.nastupil == PhotonNetwork.player.name && Moskow_.zalogena == false && GameObject.Find("Canvas").GetComponent<Kletka>().zaglushka_logi_my_karta == false)
            {
                GameObject.Find("Timer").GetComponent<Timer_>().kletka_open = true; // может не хватать валюты для оплаты и поэтому управляем своими клетками на расстоянии и продаем закладываем.
                oplatit.SetActive(true);


                if (Moskow_.k_uluchenie == false && valuta_igroka < Moskow_.arenda)
                {
                    sdatsya.SetActive(true);
                    PhotonNetwork.player.SetCustomProperties(new Hashtable { { "flag_sdat", Moskow_.kletka.tag.ToString() } });
                }
                else if (Moskow_.kletka.transform.childCount > 1)
                {
                    if (Moskow_.k_uluchenie == true && valuta_igroka < Moskow_.arenda_up[int.Parse(Moskow_.kletka.transform.GetChild(1).name[0].ToString()) - 1])
                    {
                        PhotonNetwork.player.SetCustomProperties(new Hashtable { { "flag_sdat", Moskow_.kletka.tag.ToString() } });
                        sdatsya.SetActive(true);
                    }
                }
                else sdatsya.SetActive(false);
            }
            else
            {
                sdatsya.SetActive(false);
                oplatit.SetActive(false);
            }
            if (Moskow_.zalogena == true)
            {
                if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
                    hod_next_igrok();
            }
            if (GameObject.Find("Multi").GetComponent<Moskow>().sdatsya.activeInHierarchy == true)
                GameObject.Find("Knopka_Exit").GetComponent<Button>().interactable = false;
        }
        else
        {// пустая клетка и не можем купить
            cost_up.SetActive(false);
            kupit.SetActive(false);
            prodat.SetActive(false);
            zalog.SetActive(false);
            if (Moskow_.nastupil == PhotonNetwork.player.name)
                aukcion.SetActive(true);
            else
                aukcion.SetActive(false);
            oplatit.SetActive(false);
            vikup.SetActive(false);
            sdatsya.SetActive(false);
        }
    }
    public void Auk(string v_auk)
    {
        if (Moskow_ == null) GameObject.Find("Multi").GetComponent<Multi>().multi_auk(v_auk);
        else Moskow_.GetComponent<Multi>().multi_auk(v_auk);
        okno_moskow.SetActive(true);
        //cost_up.SetActive(false);// !!!!null вылезает почему то....
        kupit.SetActive(false);
        prodat.SetActive(false);
        zalog.SetActive(false);
        aukcion.SetActive(false);
        oplatit.SetActive(false);
        vikup.SetActive(false);
        //sdatsya.SetActive(false);// !!!!null вылезает почему то....
        okno_moskow.SetActive(false);// для того чтобы при отключенном таймере поведение кнопок москвы не сбоило 
        //ибо тот кто вызывал аук(сидит на чужой клетке и после его розыгрыша у него не те кнопки москвы)
    }
    public void Sdatsya()
    {
        //выход из комнаты и отдаем остаток другому игроку.
    }
    public void Kupit()
    {
        Moskow_.GetComponent<Multi>().multi_kupit();
        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
            hod_next_igrok();
    }
    public void Cost_up()
    {
        Moskow_.GetComponent<Multi>().multi_cost_up();
        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
            hod_next_igrok();
    }
    public void Prodat()
    {
        Moskow_.GetComponent<Multi>().multi_prodat();

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
            hod_next_igrok();
    }
    public void Zalog()
    {
        Moskow_.GetComponent<Multi>().multi_zalog();

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
            hod_next_igrok();
    }
    public void Arenda()
    {
        Moskow_.GetComponent<Multi>().multi_arenda();
        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true && GameObject.Find("Multi").GetComponent<Multi>().ne_oplatil.activeInHierarchy==false)
            hod_next_igrok();
    }
    public void Vikup()
    {
        Moskow_.GetComponent<Multi>().multi_vikup();

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true && GameObject.Find("Timer").GetComponent<Timer_>().kletka_open == false)
            hod_next_igrok();
    }
    public void hod_next_igrok()
    {
        PhotonNetwork.player.SetCustomProperties(new Hashtable { { "flag_sdat", "" } });
        GameObject.Find("Knopka_Exit").GetComponent<Button>().interactable = true;
        Pole.kletki[GameObject.Find("Canvas").GetComponent<Hod>().hod].nastupil = "";
        GameObject.Find("Timer").GetComponent<PhotonView>().RPC("Hod_rpc", PhotonTargets.All);
        cost_up.SetActive(false);
        kupit.SetActive(false);
        prodat.SetActive(false);
        zalog.SetActive(false);
        aukcion.SetActive(false);
        oplatit.SetActive(false);
        vikup.SetActive(false);
        sdatsya.SetActive(false);
        okno_moskow.SetActive(false);
    }
}
