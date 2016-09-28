using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PLayers : Photon.MonoBehaviour
{
    public Transform playerPrefab;
    public Transform Valuta;
    public Sprite[] Color;
    public string valuta;
    private PhotonPlayer[] Players 
    { 
        get 
        { 
            return PhotonNetwork.playerList; 
        } 
    }
    static int kol { get { return PhotonNetwork.playerList.Length; } }
    public string stavka_auk;
    public GameObject[] player=new GameObject[5];
    public string v_auk;
    public string auk_open;
    public GameObject win;


    void Start() // бд - количество игр
    {
        Storage.Kol_game += 1;
        StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game, Storage.ban, Storage.ban_chat, Storage.kredits));
    }
    public void OnJoinedRoom()
    {
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "komu_plat", "" } });
        PhotonNetwork.room.SetCustomProperties(new Hashtable { { "ostatok_valuta", "" } });
        //Если будет глючить из-за отключения канваса//GameObject.Find("Canvas_chat_lobby").GetComponent<Canvas>().worldCamera= переменная камеры ; //чтобы иници. камеру в ворд спейсе канваса. Т.к. после перемещения из лобби в комнату он теряет камеру к которой был привязан(может исп. префаб?)
        GameObject.Find("Canvas_chat_lobby").GetComponent<Canvas>().enabled = false;

        for (int i = 0; i <= 4; i++) // выбор цвета из лоби при создании комнаты
            Color[i] = GameObject.Find("Color_fishka").GetComponent<Color_>().Color[i];

        GetComponent<Kletka>().fishka = PhotonNetwork.Instantiate(this.playerPrefab.name, GameObject.Find("Start").GetComponent<Transform>().transform.position, Quaternion.identity, 0);
        photonView.RPC("set_parent", PhotonTargets.AllBuffered); // AllBuffered для того чтобы новоприбывший игрок помнил что предыдущий установил трансформ родитель канвас

        photonView.RPC("set", PhotonTargets.All, 0);

        PhotonNetwork.player.SetCustomProperties(new Hashtable
        {
            {"color",GetComponent<Kletka>().fishka.GetComponent<Image>().sprite.name},
			{"valuta",valuta},
            {"index",GetComponent<Kletka>().fishka.GetComponent<PhotonView>().ownerId-1},
            {"stavka_auk",stavka_auk},
            {"v_auk",v_auk},
            {"auk_open",auk_open},
            {"hodit","false"},
            {"flag_sdat",""}
        });
    }
    [PunRPC]
    public void set_parent(PhotonMessageInfo info)//покраска фишек и запихиваем в канвас фишки
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in gos)
        {
            if (go.GetComponent<PhotonView>().ownerId == 1)
            {
                player[0] = go;
                go.GetComponent<Image>().sprite = Color[0];
            }
            if (go.GetComponent<PhotonView>().ownerId == 2)
            {
                player[1] = go;
                go.GetComponent<Image>().sprite = Color[1];
                //GameObject.Find("Valuta_" + Color[1].name.ToString()).GetComponent<Text>().text = "1500";
            }
            if (go.GetComponent<PhotonView>().ownerId == 3)
            {
                player[2] = go;
                go.GetComponent<Image>().sprite = Color[2];
            }
            if (go.GetComponent<PhotonView>().ownerId == 4)
            {
                player[3] = go;
                go.GetComponent<Image>().sprite = Color[3];
            }
            if (go.GetComponent<PhotonView>().ownerId == 5)
            {
                player[4] = go;
                go.GetComponent<Image>().sprite = Color[4];
            }

            go.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>().transform);
        }
    }
    [PunRPC]
    public void set(int x)//покраска фишек и запихиваем в канвас фишки
    {
        //проверка на нуллы нужна!!!иначе будем ссылаться на пустые данные
        float widt;
        widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 4;

        if (player[0] != null && player[0].transform.position.normalized == Pole.kletki[x].kletka.transform.position.normalized)
        {
            player[0].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x - widt, Pole.kletki[x].kletka.transform.position.y);
        }

        if (player[1] != null && player[1].transform.position.normalized == Pole.kletki[x].kletka.transform.position.normalized)
        {
            player[1].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x + widt, Pole.kletki[x].kletka.transform.position.y);
        }

        if (player[2] != null && player[2].transform.position.normalized == Pole.kletki[x].kletka.transform.position.normalized)
        {
            if ((int)Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.y != -57)
            {
                widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 5f;
            }
            else widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 3.5f;
            player[2].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x, Pole.kletki[x].kletka.transform.position.y + widt);
        }

        if (player[3] != null && player[3].transform.position.normalized == Pole.kletki[x].kletka.transform.position.normalized)
        {
            if ((int)Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.y != -57)
            {
                widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 5f;
            }
            else widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 3.5f;
            player[3].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x, Pole.kletki[x].kletka.transform.position.y - widt);
        }

        if (player[4] != null && player[4].transform.position.normalized == Pole.kletki[x].kletka.transform.position.normalized)
        {
            player[4].GetComponent<Canvas>().sortingOrder = 1;
            player[4].GetComponent<RectTransform>().transform.position = Pole.kletki[x].kletka.transform.position;
        }
    }
    //ТЕСТОВАЯ функция для расстановки фишек
    [PunRPC] // тест всех вместе по всем клеткам
    public void setx(int x)//покраска фишек и запихиваем в канвас фишки
    {
        //проверка на нуллы нужна!!!иначе будем ссылаться на пустые данные
        float widt;
        widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 4;

        if (player[0] != null )
        {
            player[0].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x - widt, Pole.kletki[x].kletka.transform.position.y);
        }

        if (player[1] != null )
        {
            player[1].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x + widt, Pole.kletki[x].kletka.transform.position.y);
        }

        if (player[2] != null )
        {
            if ((int)Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.y != -57)
            {
                widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 5f;
            }
            else widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 3.5f;
            player[2].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x, Pole.kletki[x].kletka.transform.position.y + widt);
        }

        if (player[3] != null )
        {
            if ((int)Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.y != -57)
            {
                widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 5f;
            }
            else widt = Pole.kletki[x].kletka.GetComponent<RectTransform>().rect.width / 3.5f;
            player[3].GetComponent<RectTransform>().transform.position = new Vector3(Pole.kletki[x].kletka.transform.position.x, Pole.kletki[x].kletka.transform.position.y - widt);
        }

        if (player[4] != null )
        {
            player[4].GetComponent<Canvas>().sortingOrder = 1;
            player[4].GetComponent<RectTransform>().transform.position = Pole.kletki[x].kletka.transform.position;
        }
    }
    void Update()
    {
        for (int ii = 0; ii <= 4;ii++ )
        {
            if (player[ii] == null)               
                GameObject.Find("Exit_" + ii).GetComponent<Image>().enabled = true;
            else GameObject.Find("Exit_" + ii).GetComponent<Image>().enabled = false;
        }
        //print(kol);
        //выход игрока по закрытию страницы
        for (int i = 0; i <= 4 - 1; i++) // игроков пять а индекс массива последний 4!!! и поэтому kol-1
        {
            if (player[i] == null)
            {
                if (PhotonNetwork.playerList.Length == 1)
                    if (GetComponent<Kletka>().fishka != null && win.activeInHierarchy==false)
                    {
                        Storage.Kol_win += 1;
                        //print(Storage.Kol_win);
                        StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game, Storage.ban, Storage.ban_chat, Storage.kredits));
                        win.SetActive(true);
                    }
                //kol = PhotonNetwork.playerList.Length; Подходит для фотонплеерс // это для того чтобы убрать лишние итерации
                for (int f = 0; f < 28; f++)
                    if (Pole.kletki[f].color_vladelca == Color[i].name)
                        Pole.kletki[f].kletka.GetComponent<PhotonView>().RPC("vihod_rpc", PhotonTargets.All, f);
            }
        }
    }
    public void exit_()//выход игрока по кнопке выйти
    {
        if (PhotonNetwork.player.customProperties["hodit"].ToString() == "true") // сюда мы должны заходить только когда сами ходим!
            GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
        // в кулбеке ОнЛефт не успеем присвоить свойство комнате
        PhotonNetwork.LeaveRoom();
        //Application.LoadLevel("Lobby");//сюда нельзя иначе лишний раз заходим в иф if (PhotonNetwork.inRoom) и ссылкаемся на несуществующие элементы
    }
    public void sdats()//выход игрока по кнопке выйти и по кнопке сдаться
    {
        // если игрок нажимает на кнопку сдаться не имея при себе клеток то мы не вызываем рпц  vihod_rpc!!!!!!!!
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "ostatok_valuta", PhotonNetwork.player.customProperties["valuta"].ToString() } });
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "komu_plat", PhotonNetwork.player.customProperties["flag_sdat"].ToString() } }); // тег клетки на который должны были заплатить аренду
        GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
        PhotonNetwork.LeaveRoom();
    } 
    public void OnLeftRoom() // кулбек вызывается у того кто выходит в комнату.
    {
        Destroy(GameObject.Find("BD"));
        Destroy(GameObject.Find("Color_fishka"));
        DontDestroyOnLoad(GameObject.Find("Canvas_chat_lobby"));
        Application.LoadLevel("Lobby");
    }
    //когда мы исп. PhotonNetwork.LeaveRoom(); мы не попадаем в кулбек OnPhotonPlayerDisconnected
    public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) // для того чтобы ловить вышедшего игрока по закрытию страницы(который в это время ходил) и передать ход другому
    { //otherPlayer - этот тот кто вышел по закрытию страницы
        if (PhotonNetwork.isMasterClient) // Для того чтобы hod_next_igrok(); вызывал ОДИН , ибо этот кулбек передается всем.
        {
            foreach (PhotonPlayer ple in Players) // это для тех кто выходит не вовремя ХОДА ,он не должен запускать hod_next_igrok(); это для него рутерн.
                if (ple.customProperties["hodit"].ToString() == "true")
                    return;

            if (otherPlayer.customProperties["flag_sdat"].ToString()!="")
                sdats(); // если игрок закрывает страницу на чужой клетке не имея при себе клеток то мы не вызываем рпц  vihod_rpc!!!!!!!!
            GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
        }
    }
}