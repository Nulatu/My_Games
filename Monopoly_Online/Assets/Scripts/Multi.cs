using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Multi : Photon.MonoBehaviour
{
    public Image Green;
    public Image Feol;
    public Image Blue;
    public Image Vinous;
    public Image Yellow;
    public Image Zalog;
    public Image Cost_up_1;
    public Image Cost_up_2;
    public Image Cost_up_3;
    public Image Cost_up_4;
    public Image Cost_up_5;

    private PhotonPlayer[] Players { get { return PhotonNetwork.playerList; } }
    Dictionary<string, Image> openWith = new Dictionary<string, Image>();
    public Text Stavka_Tek_prise;
    public Slider sly;
    public int[,] nab;
    public bool polon;
    static public List<int> player_auk = new List<int>();
    public GameObject proigrish;
    public GameObject ne_oplatil;
    public Button yes_auk;

    void Start()
    {
        nab = GameObject.Find("Multi").GetComponent<Multi>().nab=new int[,] { { 1, 2, 0 }, { 4, 5, 6 }, { 8, 9, 10 }, { 12, 13, 0 }, { 15, 16, 0 }, { 18, 19, 20 }, { 22, 23, 24 }, { 26, 27,0 } };
        polon = GameObject.Find("Multi").GetComponent<Multi>().polon;

        Green = GameObject.Find("Multi").GetComponent<Multi>().Green;
        Feol = GameObject.Find("Multi").GetComponent<Multi>().Feol;
        Blue = GameObject.Find("Multi").GetComponent<Multi>().Blue;
        Vinous = GameObject.Find("Multi").GetComponent<Multi>().Vinous;
        Yellow = GameObject.Find("Multi").GetComponent<Multi>().Yellow;
        Zalog = GameObject.Find("Multi").GetComponent<Multi>().Zalog;
        Cost_up_1 = GameObject.Find("Multi").GetComponent<Multi>().Cost_up_1;
        Cost_up_2 = GameObject.Find("Multi").GetComponent<Multi>().Cost_up_2;
        Cost_up_3 = GameObject.Find("Multi").GetComponent<Multi>().Cost_up_3;
        Cost_up_4 = GameObject.Find("Multi").GetComponent<Multi>().Cost_up_4;
        Cost_up_5 = GameObject.Find("Multi").GetComponent<Multi>().Cost_up_5;

        //словарь цветов совпадает со спрайтами на фишке
        openWith.Add("green", Green);
        openWith.Add("feol", Feol);
        openWith.Add("blue", Blue);
        openWith.Add("vinous", Vinous);
        openWith.Add("yellow", Yellow);
        openWith.Add("1", Cost_up_1);
        openWith.Add("2", Cost_up_2);
        openWith.Add("3", Cost_up_3);
        openWith.Add("4", Cost_up_4);
        openWith.Add("5", Cost_up_5);

        proigrish = GameObject.Find("Multi").GetComponent<Multi>().proigrish;
        ne_oplatil = GameObject.Find("Multi").GetComponent<Multi>().ne_oplatil;
        yes_auk = GameObject.Find("Multi").GetComponent<Multi>().yes_auk;
    }
    [PunRPC]
    public void Cost_up_rpc(PhotonMessageInfo info)
    {
        int i = int.Parse(gameObject.tag);
        Pole.kletki[i].k_uluchenie = true;
        Image cost_up;
        if (Pole.kletki[i].kletka.transform.childCount == 1)
            cost_up = Instantiate(openWith["1"], Pole.kletki[i].kletka.transform.position, Quaternion.identity) as Image;
        else
        {
            cost_up = Instantiate(openWith[(int.Parse(Pole.kletki[i].kletka.transform.GetChild(1).name[0].ToString()) + 1).ToString()], Pole.kletki[i].kletka.transform.position, Quaternion.identity) as Image;
            Destroy(Pole.kletki[i].kletka.transform.GetChild(1).gameObject);
        }
        cost_up.transform.SetParent(Pole.kletki[i].kletka.GetComponent<Transform>().transform);
        cost_up.GetComponent<RectTransform>().offsetMax = new Vector2(74, 19);
        cost_up.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        cost_up.transform.position = Pole.kletki[i].kletka.transform.position;
        cost_up.transform.position = new Vector3(Pole.kletki[i].kletka.transform.position.x, Pole.kletki[i].kletka.transform.position.y-30, 0);

        int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
        valuta_igroka = valuta_igroka - Pole.kletki[i].cost_up;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

        //"[Имя игрока] улучшил [название клетки] до [название уровня]. Арендная плата возросла!"
        string logi_stroka = info.sender.name + ":" + " улучшил " + Pole.kletki[i].kletka.name + " до " + cost_up.name[0] + ". Арендная плата возросла!";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

        if (info.sender.isLocal)
        {
            GetComponent<Moskow>().vikup.SetActive(false);

            if (cost_up.name[0] == '5')
            {
                GetComponent<Moskow>().cost_up.SetActive(false);
            }
            else
                GetComponent<Moskow>().cost_up.SetActive(true);

            if (valuta_igroka < Pole.kletki[i].cost_up)
            {
                GetComponent<Moskow>().cost_up.SetActive(false);
            }

            GetComponent<Moskow>().kupit.SetActive(false);
            GetComponent<Moskow>().prodat.SetActive(true);
            GetComponent<Moskow>().zalog.SetActive(false);
            GetComponent<Moskow>().aukcion.SetActive(false);
        }
        ne_uspet(); // Иначе не успевает вывести новую наклейку улучшения(которая в процессе присваивания)
    }
    public IEnumerator Levels_()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Moskow>().obnov_okno_moskow(int.Parse(gameObject.tag));
    }
    public void ne_uspet()
    {
        StartCoroutine(Levels_());
    }
    [PunRPC]
    public void My_kletka_rpc(PhotonMessageInfo info)
    {
        if (GameObject.Find("Canvas").GetComponent<Kletka>().zaglushka_logi_my_karta == false)
        {
            //"[Имя игрока] попал на территорию своего города [название клетки].".
            string logi_stroka = info.sender.name + ":" + " попал на территорию своего города " + gameObject.name;
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

            GameObject.Find("Canvas").GetComponent<Kletka>().zaglushka_logi_my_karta = true;
        }
    }
    public void my_kletka()
    {
        photonView.RPC("My_kletka_rpc", PhotonTargets.All);
    }
    public void multi_arenda()
    {
        photonView.RPC("Arenda_rpc", PhotonTargets.All);
    }
    public void multi_cost_up()
    {
        photonView.RPC("Cost_up_rpc", PhotonTargets.All);
    }
    public void multi_kupit()
    {
        photonView.RPC("Kupit_rpc", PhotonTargets.All,"no",int.Parse(gameObject.tag));
    }
    public void multi_prodat()
    {
        photonView.RPC("Prodat_rpc", PhotonTargets.All);
    }
    public void multi_zalog()
    {
        photonView.RPC("Zalog_rpc", PhotonTargets.All);
    }
    public void multi_vikup()
    {
        photonView.RPC("Vikup_rpc", PhotonTargets.All);
    }
    public void multi_auk(string v_auk)
    {
        if (PhotonNetwork.room.customProperties["index_auk"]==null)
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", PhotonNetwork.player.ID+1 } }); 
        else PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", PhotonNetwork.player.ID + 1 } }); // чтобы второй аук и т.д. отправил тот кто должен

        if (photonView == null) GameObject.Find("Multi").GetComponent<PhotonView>().RPC("Auk_rpc", PhotonTargets.All, v_auk);
        else photonView.RPC("Auk_rpc", PhotonTargets.All, v_auk);
    }
    [PunRPC]
    public void Auk_rpc(string v_auk,PhotonMessageInfo info)
    {
        int i = Convert.ToInt32(PhotonNetwork.room.customProperties["index_auk"]);
        string logi_stroka;


        int nn = 0;
        foreach (PhotonPlayer ple in Players)
            if ("no" == ple.customProperties["v_auk"].ToString()) nn++;
        if (nn == PhotonNetwork.playerList.Length)
        {
            logi_stroka = "Аукцион не состоялся.";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;


            if (info.sender.isLocal) // чтобвы вызывал один а не все.Иначе никто не походит =))
            {
                foreach (PhotonPlayer ple in Players)
                    ple.SetCustomProperties(new Hashtable() { { "v_auk", "yes" } }); // чтобы второй аук и т.д. отправили всем с обновленными свойствами участия в аке yes
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_kletka", null } }); // чтобы второй аук и т.д. разыгрывал новую клетку
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_Tek_prise", null } }); 

                if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
                    GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
            }

            return;
        }



        if (PhotonNetwork.room.customProperties["Stavka_plus"] == null)
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_plus", "false" } });

        info.sender.SetCustomProperties(new Hashtable() { { "v_auk", v_auk } });

        if (PhotonNetwork.room.customProperties["Stavka_Tek_prise"] != null)
        {
            if (PhotonNetwork.room.customProperties["Stavka_plus"].ToString() == "true")
            {
                //"[Имя игрока] увеличил ставку до $[сумма ставки]K за [название клетки]."
                logi_stroka = info.sender.name + ":" + " увеличил ставку до " + PhotonNetwork.room.customProperties["Stavka_Tek_prise"].ToString() + " за " + PhotonNetwork.room.customProperties["name_kletka"].ToString();
                logi_stroka = logi_stroka.Replace("\n", "");
                logi_stroka += "\n";
                GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
            }
            else
            {
                if (PhotonNetwork.player.ID == i && PhotonNetwork.player.customProperties["v_auk"].ToString() == "yes")
                {

                    int sta = int.Parse(PhotonNetwork.room.customProperties["Stavka_Tek_prise"].ToString());
                    sta += 10000;
                    PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_Tek_prise", sta.ToString() } });

                }
            }
        }


        for (int s = 1; s <= 5;s++ )
            if (i == s && GameObject.Find("Canvas").GetComponent<PLayers>().player[i - 1] == false)
            {
                i++;
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", i } });
            }

        if (i == 6)
        {
            i = 1;//Обязательно!
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", 1 } }); //чтобы последний игрок вызвавший аук передал аук первому игроку и тот его открыл
        }

        if (PhotonNetwork.player.ID == i && PhotonNetwork.player.customProperties["v_auk"].ToString() == "yes")
        {
            if (PhotonNetwork.room.customProperties["index_kletka"]!= null) // это для того чтобы мы автоматом не выигрывали клетку когда разыгрывали аук между двумя игроками // для того чтобы появлялась окно аука
            {
                nn = 0;
                foreach (PhotonPlayer ple in Players)
                    if ("yes" == ple.customProperties["v_auk"].ToString()) nn++;
                if (nn == 1)
                {
                    v_auk = "yes";
                    photonView.RPC("Kupit_rpc", PhotonTargets.All, v_auk, int.Parse(PhotonNetwork.room.customProperties["index_kletka"].ToString()));
                    return;
                }
            }
            
            if (PhotonNetwork.room.customProperties["index_kletka"] == null)
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_kletka", gameObject.tag.ToString() } });

            GameObject.Find("Multi").GetComponent<Moskow>().okno_auk.SetActive(true);

            if (PhotonNetwork.room.customProperties["name_kletka"] == null)
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "name_kletka", gameObject.name } });
            GameObject.Find("kletka").GetComponent<Text>().text = PhotonNetwork.room.customProperties["name_kletka"].ToString();

            if (PhotonNetwork.room.customProperties["Stavka_Tek_prise"] == null)
            {
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_Tek_prise", Pole.kletki[Convert.ToInt32(gameObject.tag)].cost.ToString() } });//GameObject.Find("Stavka_Tek_prise").GetComponent<Text>().text } });
                GameObject.Find("stavka").GetComponent<Text>().text = (Pole.kletki[Convert.ToInt32(gameObject.tag)].cost).ToString();
            }
            else GameObject.Find("Stavka_Tek_prise").GetComponent<Text>().text = PhotonNetwork.room.customProperties["Stavka_Tek_prise"].ToString();

            if (Convert.ToInt32(PhotonNetwork.player.customProperties["valuta"]) >= Convert.ToInt32(GameObject.Find("stavka").GetComponent<Text>().text))
            GameObject.Find("Stavka_auk").GetComponent<Stavka>().sly.minValue = Convert.ToInt32(PhotonNetwork.room.customProperties["Stavka_Tek_prise"]);

            if (Convert.ToInt32(PhotonNetwork.player.customProperties["valuta"]) < Convert.ToInt32(GameObject.Find("stavka").GetComponent<Text>().text))
            {
                yes_auk.interactable = false;
                GameObject.Find("Stavka_auk").GetComponent<Stavka>().sly.minValue = Convert.ToInt32(PhotonNetwork.player.customProperties["valuta"]);
            }
            else yes_auk.interactable = true;

            print(GameObject.Find("Stavka_Tek_prise").GetComponent<Text>().text);
            i++;
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", i } });

            return;
        }
        else if (PhotonNetwork.player.customProperties["v_auk"].ToString() == "no" && info.sender.isLocal == false && PhotonNetwork.player.ID == i)
        {
            i++;
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", i } });
            v_auk = "no"; // иначе игрок со свойством no отправляет рпц со свойством yes(т.е. изменяет свое свойство v_auk а нам это нельзя допустить.
            photonView.RPC("Auk_rpc", PhotonTargets.All,v_auk); /// вызов должен быть только у того кто пропускает вызов аука , тот кто от него отказался, нажал на нет
        }


        for (int s = 1; s <= 5; s++)
            if (i == s && GameObject.Find("Canvas").GetComponent<PLayers>().player[i - 1] == false)
            {
                i++;
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", i } });
            }

        if(i==6)
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_auk", 1 } }); // чтобы запускать цикл сначала после нажатия кнопки да или нет
    }
    [PunRPC]
    public void Arenda_rpc(PhotonMessageInfo info)
    {
        int i = int.Parse(gameObject.tag);
        if (GameObject.Find("Canvas").GetComponent<Kletka>().zaglushka_logi_my_karta == false) // чтобы логи "моя карта" не выводилось больше одного раз по клику
        {
            string color = info.sender.customProperties["color"].ToString();
            string color_vladelca = Pole.kletki[i].color_vladelca;

            if (color != color_vladelca && (Pole.kletki[i].kuplena == true || Pole.kletki[i].zalogena == true))
            {
                //print("chugak_kletka");
            }
            else return; // выйти из функции если не ступил на чужаю клетку


            int oplata = Pole.kletki[i].arenda; // без акций // заложенная клетка не попадает сюда
            if (Pole.kletki[i].kletka.transform.childCount > 1) // если есть акции
                oplata = Pole.kletki[i].arenda_up[int.Parse(Pole.kletki[i].kletka.transform.GetChild(1).name[0].ToString()) - 1];
                   

            int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
            valuta_igroka = valuta_igroka - oplata;

            if (valuta_igroka < 0) // пишем что не хватает оплатить аренду
            {
                ne_oplatil.SetActive(true);// пишем не хватает оплаты.
                return;
            }
            else
            {
                info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
                GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();
            }

            valuta_igroka = int.Parse(GameObject.Find("Valuta_" + color_vladelca).GetComponent<Text>().text);
            valuta_igroka = valuta_igroka + oplata;
            if (PhotonNetwork.player.customProperties["color"].ToString() == color_vladelca)
                PhotonNetwork.player.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
            GameObject.Find("Valuta_" + color_vladelca).GetComponent<Text>().text = valuta_igroka.ToString();


            //"[Имя игрока] попал на территорию города [название клетки]. Владелец: [имя игрока-владельца]. [Имя игрока] обязан выплатить арендную плату в сумме $[стоимость арендной платы]K."
            //"[Имя игрока] выплатил [имя игрока-владельца] $[стоимость арендой платы]K арендной платы."
            string name_vladelca = Pole.kletki[i].name_vladelca;
            string logi_stroka = info.sender.name + ":" + " попал на территорию города " + gameObject.name + ".Владелец:" + name_vladelca + "." + info.sender.name + " обязан выплатить арендную плату в сумме " + oplata.ToString() + ".";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

            logi_stroka = info.sender.name + ":" + " выплатил " + name_vladelca + " " + oplata.ToString() + " арендной платы.";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

            GameObject.Find("Canvas").GetComponent<Kletka>().zaglushka_logi_my_karta = true;


            if (info.sender.isLocal)
            {
                GetComponent<Moskow>().oplatit.SetActive(false);
            }
        }
    }

    [PunRPC]
    public void torg_zamena(string color1, string color2,string name2,string prise1,string prise2, PhotonMessageInfo info)
    {
        if (prise1 == "") prise1 = "0";
        if (prise2 == "") prise2 = "0";
        int max=Math.Max(int.Parse(prise1), int.Parse(prise2));
        int min = Math.Min(int.Parse(prise1), int.Parse(prise2));
        int val = max - min;
        int valuta_igroka = int.Parse(GameObject.Find("Valuta_" + color1).GetComponent<Text>().text);
        valuta_igroka = valuta_igroka + val;
        GameObject.Find("Valuta_" + color1).GetComponent<Text>().text = valuta_igroka.ToString();
        if (color1 == PhotonNetwork.player.customProperties["color"].ToString()) PhotonNetwork.player.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });

        valuta_igroka = int.Parse(GameObject.Find("Valuta_" + color2).GetComponent<Text>().text);
        valuta_igroka = valuta_igroka - val;
        GameObject.Find("Valuta_" + color2).GetComponent<Text>().text = valuta_igroka.ToString();
        if (color2 == PhotonNetwork.player.customProperties["color"].ToString()) PhotonNetwork.player.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });

        
        for (int r = 0; r < 28; r++)
        {
            if (Pole.kletki[r].v_torge == true)
            {
                if (Pole.kletki[r].color_vladelca == color1)
                {
                    Destroy(Pole.kletki[r].kletka.transform.GetChild(0).gameObject);

                    Image pokraska = Instantiate(openWith[color2], Pole.kletki[r].kletka.transform.position, Quaternion.identity) as Image;
                    pokraska.transform.SetParent(Pole.kletki[r].kletka.GetComponent<Transform>().transform);
                    pokraska.GetComponent<RectTransform>().offsetMax = new Vector2(Pole.kletki[r].kletka.GetComponent<RectTransform>().rect.width, Pole.kletki[r].kletka.GetComponent<RectTransform>().rect.height);
                    pokraska.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    pokraska.transform.position = Pole.kletki[r].kletka.transform.position;

                    Pole.kletki[r].color_vladelca = color2;

                    Pole.kletki[r].name_vladelca = info.sender.name;

                }

                else if (Pole.kletki[r].color_vladelca == color2)
                {
                    Destroy(Pole.kletki[r].kletka.transform.GetChild(0).gameObject);

                    Image pokraska = Instantiate(openWith[color1], Pole.kletki[r].kletka.transform.position, Quaternion.identity) as Image;
                    pokraska.transform.SetParent(Pole.kletki[r].kletka.GetComponent<Transform>().transform);
                    pokraska.GetComponent<RectTransform>().offsetMax = new Vector2(Pole.kletki[r].kletka.GetComponent<RectTransform>().rect.width, Pole.kletki[r].kletka.GetComponent<RectTransform>().rect.height);
                    pokraska.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    pokraska.transform.position = Pole.kletki[r].kletka.transform.position;

                    Pole.kletki[r].color_vladelca = color1;

                    Pole.kletki[r].name_vladelca = name2;
                }
            }
            
        }
        
    }

    [PunRPC]
    public void Kupit_rpc(string v_auk,int i,PhotonMessageInfo info)
    {


        Pole.kletki[i].kuplena = true;
        string color = info.sender.customProperties["color"].ToString();
        Pole.kletki[i].color_vladelca = color;
        Pole.kletki[i].name_vladelca = info.sender.name;


        int komplekt = -1;
        Pole.kletki[i].v_komplekte = true;
        for (int n = 0; n <= 7; n++)
            for (int nn = 0; nn <= 2; nn++)
                if (nab[n, nn] == i)// перебираем и находим в каком комплекте находится клетка
                    komplekt = n;

        polon = true;
        for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // перебираем все индексы клетки этого комплекта кроме пустого(нулевого)
        {
            if (Pole.kletki[nab[komplekt, 0]].color_vladelca != Pole.kletki[nab[komplekt, nn]].color_vladelca)
            {
                polon = false;
            }
            if (Pole.kletki[nab[komplekt, nn]].v_komplekte == false)
                polon = false;
        }

        if (polon == true)
        {
            for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // это чтобы включать кнопку улучшения на расстоянии и когда ступим на нее
            {
                Pole.kletki[nab[komplekt, nn]].uluchena =true;
            }
        }

        if (v_auk == "no")
        {
            int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
            valuta_igroka = valuta_igroka - Pole.kletki[i].cost;
            info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
            GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();


            //"[Имя игрока] купил[а] [название клетки] за $[стоимость клетки]K".
            string logi_stroka = info.sender.name + ":" + " купил " + Pole.kletki[i].kletka.name + " за " + Pole.kletki[i].cost.ToString();
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
        }
        else
        {
            int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());

            valuta_igroka = valuta_igroka - int.Parse(PhotonNetwork.room.customProperties["Stavka_Tek_prise"].ToString());

            
            info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
            GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

            //"[Имя игрока] выиграл аукцион и стал владельцем [название клетки] за $[сумма ставки]K."
            string logi_stroka2 = info.sender.name + ":" + " выиграл аукцион и стал владельцем " + Pole.kletki[i].kletka.name + " за " + PhotonNetwork.room.customProperties["Stavka_Tek_prise"].ToString();
            logi_stroka2 = logi_stroka2.Replace("\n", "");
            logi_stroka2 += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka2;

            foreach (PhotonPlayer ple in Players)
                ple.SetCustomProperties(new Hashtable() { { "v_auk", "yes" } }); // чтобы второй аук и т.д. отправили всем с обновленными свойствами участия в аке yes
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_kletka", null } }); // чтобы второй аук и т.д. разыгрывал новую клетку
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_Tek_prise", null } }); 

            if (info.sender.isLocal) // чтобвы вызывал один а не все.Иначе никто не походит =))
            {
                if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
                    GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
            }
            
        }


        Image pokraska = Instantiate(openWith[color], Pole.kletki[i].kletka.transform.position, Quaternion.identity) as Image;
        pokraska.transform.SetParent(Pole.kletki[i].kletka.GetComponent<Transform>().transform);
        pokraska.GetComponent<RectTransform>().offsetMax = new Vector2(Pole.kletki[i].kletka.GetComponent<RectTransform>().rect.width, Pole.kletki[i].kletka.GetComponent<RectTransform>().rect.height);
        pokraska.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        pokraska.transform.position = Pole.kletki[i].kletka.transform.position;
        GetComponent<Moskow>().obnov_okno_moskow(i);
    }
    [PunRPC]
    public void Vikup_rpc(PhotonMessageInfo info)
    {
        int i = int.Parse(gameObject.tag);
        Pole.kletki[i].kuplena = true;

        Destroy(Pole.kletki[i].kletka.transform.GetChild(1).gameObject);

        int komplekt = -1;

        for (int n = 0; n <= 7; n++)
            for (int nn = 0; nn <= 2; nn++)
                if (nab[n, nn] == i)// перебираем и находим в каком комплекте находится клетка
                    komplekt = n;

        polon = true;
        for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // перебираем все индексы клетки этого комплекта кроме пустого(нулевого)
        {
            if (Pole.kletki[nab[komplekt, 0]].color_vladelca != Pole.kletki[nab[komplekt, nn]].color_vladelca)
                polon = false;
            if (Pole.kletki[nab[komplekt, nn]].v_komplekte == false)
                polon = false;
        }

        if (polon == true)
        {
            for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // это чтобы включать кнопку улучшения на расстоянии и когда ступим на нее
            {
                Pole.kletki[nab[komplekt, nn]].uluchena = true;
            }
        }


        int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
        valuta_igroka = valuta_igroka - Pole.kletki[i].cost;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

        //"[Имя игрока] выкупил [название клетки]."
        string logi_stroka = info.sender.name + ":" + " выкупил " + Pole.kletki[i].kletka.name + ".";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

        if (info.sender.isLocal)
        {
            GetComponent<Moskow>().kupit.SetActive(false);
            GetComponent<Moskow>().prodat.SetActive(true);
            GetComponent<Moskow>().zalog.SetActive(true);

            if(valuta_igroka>=Pole.kletki[i].cost_up)
                GetComponent<Moskow>().cost_up.SetActive(true);
            else
                GetComponent<Moskow>().cost_up.SetActive(false);
            GetComponent<Moskow>().aukcion.SetActive(false);
            GetComponent<Moskow>().vikup.SetActive(false);
        }
        GetComponent<Moskow>().obnov_okno_moskow(i);
    }
    
    [PunRPC]
    public void Prodat_rpc(PhotonMessageInfo info)
    {
        int valuta_igroka;
        string logi_stroka;
        int i = int.Parse(gameObject.tag);


        if (Pole.kletki[i].kletka.transform.childCount == 1)
            Destroy(Pole.kletki[i].kletka.transform.GetChild(0).gameObject);
        else
        {
            Image cost_up;
            if (Pole.kletki[i].kletka.transform.GetChild(1).name[0] != '1')
            {
                cost_up = Instantiate(openWith[(int.Parse(Pole.kletki[i].kletka.transform.GetChild(1).name[0].ToString()) - 1).ToString()], Pole.kletki[i].kletka.transform.position, Quaternion.identity) as Image;
                cost_up.transform.SetParent(Pole.kletki[i].kletka.GetComponent<Transform>().transform);
                cost_up.GetComponent<RectTransform>().offsetMax = new Vector2(74, 19);
                cost_up.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                cost_up.transform.position = Pole.kletki[i].kletka.transform.position;
                cost_up.transform.position = new Vector3(Pole.kletki[i].kletka.transform.position.x, Pole.kletki[i].kletka.transform.position.y - 30, 0);
            }
            else Pole.kletki[i].k_uluchenie = false;



            valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
            valuta_igroka = valuta_igroka + (Pole.kletki[i].cost_up*90)/100;
            info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
            GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

            //"[Имя игрока] продал[а] акцию [название клетки]."
            logi_stroka = info.sender.name + ":" + " продал акцию " + gameObject.name+".";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

            if (info.sender.isLocal)
            {
                GetComponent<Moskow>().kupit.SetActive(false);// чтобы нельзя была покупать карту после продажи на расстоянии
                GetComponent<Moskow>().prodat.SetActive(true);
                if (Pole.kletki[i].kletka.transform.GetChild(1).name[0] != '1')
                    GetComponent<Moskow>().zalog.SetActive(false);
                else
                    GetComponent<Moskow>().zalog.SetActive(true);
                GetComponent<Moskow>().aukcion.SetActive(false);
                if (Pole.kletki[i].kletka.transform.GetChild(1).name[0] != '1' && Pole.kletki[i].uluchena == true)
                    GetComponent<Moskow>().cost_up.SetActive(false);
                else
                {
                    if (valuta_igroka >= Pole.kletki[i].cost_up)
                        GetComponent<Moskow>().cost_up.SetActive(true);
                    else
                        GetComponent<Moskow>().cost_up.SetActive(false);
                }
            }
            Destroy(Pole.kletki[i].kletka.transform.GetChild(1).gameObject);
            ne_uspet();
            return;
        }
        Pole.kletki[i].kuplena = false;
        Pole.kletki[i].zalogena = false;
        Pole.kletki[i].name_vladelca = null;

        
        int komplekt = -1;

        for (int n = 0; n <= 7; n++)
            for (int nn = 0; nn <= 2; nn++)
                if (nab[n, nn] == i)// перебираем и находим в каком комплекте находится клетка
                    komplekt = n;

        polon = true;
        for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // перебираем все индексы клетки этого комплекта кроме пустого(нулевого)
        {
            if (Pole.kletki[nab[komplekt, 0]].color_vladelca != Pole.kletki[nab[komplekt, nn]].color_vladelca)
                polon = false;
            if (Pole.kletki[nab[komplekt, nn]].v_komplekte == false)
                polon = false;
        }

        if (polon == true)
        {
            Pole.kletki[i].v_komplekte = false;
            for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // это чтобы включать кнопку улучшения на расстоянии и когда ступим на нее
            {
                Pole.kletki[nab[komplekt, nn]].uluchena = false;
            }
        }

        Pole.kletki[i].color_vladelca = null; // позже обнуляем т.к. с помощью этой переменной проверяем комплект ОДНОГО ЦВЕТА

        valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
        valuta_igroka = valuta_igroka + Pole.kletki[i].cost/2;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

        //"[Имя игрока] продал[а] [название клетки] за $[цена продажи]K."
        logi_stroka = info.sender.name + ":" + " продал " + Pole.kletki[i].kletka.name + " за " + (Pole.kletki[i].cost / 2).ToString() + ".";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

        if (info.sender.isLocal)
        {
            GetComponent<Moskow>().kupit.SetActive(false);// чтобы нельзя была покупать карту после продажи на расстоянии
            GetComponent<Moskow>().prodat.SetActive(false);
            GetComponent<Moskow>().zalog.SetActive(false);
            GetComponent<Moskow>().aukcion.SetActive(false);
            if (polon == true)
            GetComponent<Moskow>().cost_up.SetActive(false);
        }
        ne_uspet();
    }

    [PunRPC]
    public void Zalog_rpc(PhotonMessageInfo info)
    {
        int i = int.Parse(gameObject.tag);
        Pole.kletki[i].kuplena = false;
        Pole.kletki[i].zalogena = true;


        int komplekt = -1;

        for (int n = 0; n <= 7; n++)
            for (int nn = 0; nn <= 2; nn++)
                if (nab[n, nn] == i)// перебираем и находим в каком комплекте находится клетка
                    komplekt = n;

        polon = true;
        for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // перебираем все индексы клетки этого комплекта кроме пустого(нулевого)
        {
            if (Pole.kletki[nab[komplekt, 0]].color_vladelca != Pole.kletki[nab[komplekt, nn]].color_vladelca)
                polon = false;
            if (Pole.kletki[nab[komplekt, nn]].v_komplekte == false)
                polon = false;
        }

        if (polon == true)
        {
            for (int nn = 0; nn <= 2 && nab[komplekt, nn] != 0; nn++) // это чтобы включать кнопку улучшения на расстоянии и когда ступим на нее
            {
                Pole.kletki[nab[komplekt, nn]].uluchena = false;
            }
        }


        Image zalog = Instantiate(Zalog, Pole.kletki[i].kletka.transform.position, Quaternion.identity) as Image;
        zalog.transform.SetParent(Pole.kletki[i].kletka.GetComponent<Transform>().transform);
        zalog.GetComponent<RectTransform>().offsetMax = new Vector2(61, 50);
        zalog.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        zalog.transform.position = Pole.kletki[i].kletka.transform.position;

        int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
        valuta_igroka = valuta_igroka + Pole.kletki[i].cost/2;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

        //"[Имя игрока] внес[ла] под залог [название клетки]."
        string logi_stroka = info.sender.name + ":" + " внес под залог " + Pole.kletki[i].kletka.name + ".";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

        if (info.sender.isLocal)
        {
            if(valuta_igroka>=Pole.kletki[i].cost)
                GetComponent<Moskow>().vikup.SetActive(true);
            else
                GetComponent<Moskow>().vikup.SetActive(false);
            GetComponent<Moskow>().kupit.SetActive(false);
            GetComponent<Moskow>().prodat.SetActive(false);
            GetComponent<Moskow>().zalog.SetActive(false);
            GetComponent<Moskow>().aukcion.SetActive(false);
            GetComponent<Moskow>().cost_up.SetActive(false);
        }
        GetComponent<Moskow>().obnov_okno_moskow(i);
    }
    [PunRPC]
    public void vihod_rpc(int i) // это получает все когда игрок выходит закрытием страницы или кнопкой выйти
    {

        if (PhotonNetwork.room.customProperties["komu_plat"].ToString() != "" && Pole.kletki[i].color_vladelca != null) //ВТОРОЕ УСЛОВИЕ ОБЯЗАТЕЛЬНО!иначе один игрок будет заходить ДВА РАЗА.
        {
            obnul_komu_platit(); // обнуляем свойство komu_plat чтобы сюда не заходили по кнопке выйти или закрытию страницы
            string color_vladelca = Pole.kletki[Convert.ToInt32(PhotonNetwork.room.customProperties["komu_plat"])].color_vladelca;
            int valuta_igroka = int.Parse(GameObject.Find("Valuta_" + color_vladelca).GetComponent<Text>().text);

            if (PhotonNetwork.room.customProperties["ostatok_valuta"].ToString() != "")
            {
                valuta_igroka = valuta_igroka + int.Parse(PhotonNetwork.room.customProperties["ostatok_valuta"].ToString());
                GameObject.Find("Valuta_" + color_vladelca).GetComponent<Text>().text = valuta_igroka.ToString();
                if (PhotonNetwork.player.customProperties["color"].ToString() == color_vladelca)
                {
                    print(i);
                    PhotonNetwork.player.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
                    print(PhotonNetwork.player.customProperties["valuta"].ToString());
                }
                PhotonNetwork.room.SetCustomProperties(new Hashtable { { "ostatok_valuta", "" } });
            }
            valuta_igroka = valuta_igroka + Pole.kletki[i].cost/2;
            if (PhotonNetwork.player.customProperties["color"].ToString() == color_vladelca)
            {
                print(i);
                PhotonNetwork.player.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
                print(PhotonNetwork.player.customProperties["valuta"].ToString());
            }
            GameObject.Find("Valuta_" + color_vladelca).GetComponent<Text>().text = valuta_igroka.ToString();
            
            if (gameObject.transform.childCount != 0)
                Destroy(gameObject.transform.GetChild(0).gameObject);

            if (gameObject.transform.childCount != 0 && Pole.kletki[i].zalogena == true)
                Destroy(gameObject.transform.GetChild(1).gameObject);

            if (gameObject.transform.childCount != 0 && Pole.kletki[i].k_uluchenie == true)
            {
                print(i);
                print(int.Parse(Pole.kletki[i].kletka.transform.GetChild(1).name[0].ToString()));
                valuta_igroka = valuta_igroka + ((Pole.kletki[i].cost_up * 90) / 100) * int.Parse(Pole.kletki[i].kletka.transform.GetChild(1).name[0].ToString());
                if (PhotonNetwork.player.customProperties["color"].ToString() == color_vladelca)
                {
                    PhotonNetwork.player.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
                    print(PhotonNetwork.player.customProperties["valuta"].ToString());
                }
                GameObject.Find("Valuta_" + color_vladelca).GetComponent<Text>().text = valuta_igroka.ToString();

                Destroy(gameObject.transform.GetChild(1).gameObject);
            }

            Pole.kletki[i].v_komplekte = false;
            Pole.kletki[i].uluchena = false;

            Pole.kletki[i].kuplena = false;
            Pole.kletki[i].zalogena = false;
            Pole.kletki[i].k_uluchenie = false;
            Pole.kletki[i].color_vladelca = null;
            Pole.kletki[i].name_vladelca = null;
        }
        else
        {//удаляем купленные клетки вышедшего игрока
            if (gameObject.transform.childCount != 0)
                Destroy(gameObject.transform.GetChild(0).gameObject);

            if (gameObject.transform.childCount != 0 && Pole.kletki[i].zalogena == true)
                Destroy(gameObject.transform.GetChild(1).gameObject);

            if (gameObject.transform.childCount != 0 && Pole.kletki[i].k_uluchenie == true)
                Destroy(gameObject.transform.GetChild(1).gameObject);

            Pole.kletki[i].v_komplekte = false;
            Pole.kletki[i].uluchena = false;

            Pole.kletki[i].kuplena = false;
            Pole.kletki[i].zalogena = false;
            Pole.kletki[i].k_uluchenie = false;
            Pole.kletki[i].color_vladelca = null;
            Pole.kletki[i].name_vladelca = null;
        }

    }
    public IEnumerator Obnul()
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "komu_plat", "" } });
    }
    public void obnul_komu_platit()
    {
        StartCoroutine(Obnul());
    }
}