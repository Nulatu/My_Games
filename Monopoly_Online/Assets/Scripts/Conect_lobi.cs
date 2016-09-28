using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;
public class Conect_lobi : Photon.MonoBehaviour
{
    public GameObject okno_create_room;
    public GameObject okno_join_room;
    public byte maxplayer = 5;
    public Text[] playerCount;
    public GameObject[] okno_room;
    public GameObject[] zayavka;
    public GameObject[] exit;
    public Button create;
    public int k;
    /// Подключиться автоматиечски?
    public bool AutoConnect = true;
    public byte Version = 1;
    ///если мы не хотим, чтобы подключить в Start (), мы должны "помнить", если мы позвонили ConnectUsingSettings ()
    private bool ConnectInUpdate = true;
    private PhotonPlayer[] Players { get { return PhotonNetwork.playerList; } }
    public Text name_;
    public bool stop;
    public int auk;
    public GameObject okno_zagruzka;
    
    public Text srok_bana;
    public GameObject ban;
    public GameObject ban_chat_;
    public Text srok_ban_chat;

    public ChatGui chat_inic;
    public void OnJoinedLobby()
    {
        Auth();
        if (PhotonNetwork.playerName == "")
            PhotonNetwork.playerName = Storage.Login;
        chat_inic.inic_chat();
        name_.text = PhotonNetwork.playerName;
    }
    private void Auth()
    {
        Storage.Login = PlayerPrefs.GetString("userLogin", "player" + UnityEngine.Random.Range(0, 100)); 
        //print(Storage.Login);
        PlayerPrefs.SetString("userLogin", Storage.Login);
        PlayerPrefs.Save();

        // если есть эти данные на компе то мы не устанавливаем новый логин а получаем из PlayerPrefs(т.е. из компа)
        StartCoroutine(NetworkApi.Instance.Login(Storage.Login));
        Invoke("ban_",0.5F);
    }
    public void ban_()
    {
        if (Storage.ban.Year > DateTime.Now.Year || Storage.ban.Month > DateTime.Now.Month || Storage.ban.Day > DateTime.Now.Day ||
            Storage.ban.Hour > DateTime.Now.Hour || Storage.ban.Minute > DateTime.Now.Minute)
        {
            srok_bana.text = Storage.ban.ToString("MM/dd/yyyy HH:mm:ss");
            ban.SetActive(true);
            okno_zagruzka.SetActive(false);
        }
        else
        {
            ban.SetActive(false);
            okno_zagruzka.SetActive(false);
            Storage.ban =new DateTime(0); // |=| Storage.ban.Year = 1
            StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game, Storage.ban, Storage.ban_chat, Storage.kredits));
        } 
    }
    public void ban_chat()
    {
        if (Storage.ban_chat.Year > DateTime.Now.Year || Storage.ban_chat.Month > DateTime.Now.Month || Storage.ban_chat.Day > DateTime.Now.Day ||
            Storage.ban_chat.Hour > DateTime.Now.Hour || Storage.ban_chat.Minute > DateTime.Now.Minute)
        {
            srok_ban_chat.text = Storage.ban_chat.ToString("MM/dd/yyyy HH:mm:ss");
            ban_chat_.SetActive(true);
        }
        else
        {
            print("sss");
            ban_chat_.SetActive(false);
            Storage.ban_chat = new DateTime(0); // |=| Storage.ban.Year = 1
            StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game, Storage.ban,Storage.ban_chat, Storage.kredits));
        }
    }

    public virtual void Start()
    {
        //ID в лобби у игроков одинаковые = -1
        k = -1;
        PhotonNetwork.autoJoinLobby = true;
    }
    public virtual void Update()
    {
        if(Storage.Login!="")
            StartCoroutine(NetworkApi.Instance.Login(Storage.Login));
        if (Storage.ban.Year != 1) ban_();
        if (Storage.ban_chat.Year != 1) ban_chat();

        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "." + Application.loadedLevel);//подключение к лобби
        }

        foreach (RoomInfo sto in PhotonNetwork.GetRoomList())//те кто не зашли в комнату или не создали комнату
        {
            int i = int.Parse(sto.name);
            rom.okno_room[i].name = "live";
            okno_room[i].SetActive(true);
            playerCount[i].text = sto.playerCount.ToString() + "/" + Convert.ToString(sto.maxPlayers);
            if (sto.customProperties["zaneto"].ToString() == "closed")
            {
                zayavka[i].GetComponent<Button>().interactable = false;
            }
            else zayavka[i].GetComponent<Button>().interactable = true;
        }

        for (int s = 0; s < rom.okno_room.Length; s++)
        {
            //print(s); print(rom.okno_room[s].ro);
            if (rom.okno_room[s].name != "live")
                okno_room[s].SetActive(false);
        }
        for (int s = 0; s < rom.okno_room.Length; s++)
        {
            rom.okno_room[s].name = "";
        }

        
        if (PhotonNetwork.inRoom)//те кто создали комнату или вошли в комнату и НЕ набрали нужное кол-во человек
        {//метка -1 означает что мы не в комнате// а мы сюда должны заходить только тогда, когда  подключились или создали комнату
            //когда мы не в комнате мы не должны заходить в этот иф(для этого служат верхние ифы просмотра комнат)
            //иначе playerCount будут перемешиваться
            maxplayer = Convert.ToByte(PhotonNetwork.room.maxPlayers);// чтобы все игроки в комнате узнали maxplayer , который инициализировал игрок, который создал комнату
            foreach (GameObject za in zayavka)
                za.GetComponent<Button>().interactable = false;
            okno_room[k].SetActive(true); // что при нажатии на заявку не пропадало окно стола из-за ифа !PhotonNetwork.inRoom
            if (!PhotonNetwork.player.isMasterClient && stop == false) // т.к. мастер клиент ругается на (int)Players[0].customProperties["color"]
            {
                GameObject.Find("Color_fishka").GetComponent<Color_>().colors(GameObject.Find("Color_fishka").GetComponent<Color_>().colo[(int)Players[0].customProperties["color_index"]]);
            }
            stop = true;

            if (PhotonNetwork.playerList.Length == maxplayer) // загружаем
            {
                PhotonNetwork.room.open = true;
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "zaneto", "closed" } });
                DontDestroyOnLoad(GameObject.Find("Color_fishka").gameObject);
                //дестройкать обьъект канвас чат лобби через несколько секунд нельзя..удалится тот кто перадался в комнату, а не тот который остался в лобби
                DontDestroyOnLoad(GameObject.Find("Canvas_chat_lobby").gameObject);
                Application.LoadLevel("Igrovoe_pole");
            }
            playerCount[k].text = PhotonNetwork.playerList.Length.ToString() + "/" + Convert.ToString(maxplayer);
        }
    }
    public void kol_player_inroom(int kol)
    {
        maxplayer=Convert.ToByte(kol);
    }
    public void Create_room()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (okno_room[i].activeInHierarchy == false) // okno_join_room не должно быть false!! иначе не найдет!
            {
                PhotonNetwork.CreateRoom(okno_room[i].name, true, true, maxplayer,  new Hashtable
                {
                    {"zaneto", "open"},
                }, new[] { "zaneto" });
                create.interactable = false;
                okno_create_room.SetActive(false);
                okno_room[i].SetActive(true);
                zayavka[i].SetActive(false);
                exit[i].SetActive(true);
                playerCount[i].text = PhotonNetwork.playerList.Length.ToString() + "/" + Convert.ToString(maxplayer);
                k = i;
                return;
            }
        }
    }
    public void Join_room(int i) // заявка
    {
        create.interactable = false;
        zayavka[i].SetActive(false);
        exit[i].SetActive(true);
        PhotonNetwork.JoinRoom(okno_room[i].name);
        k = i;
    }
    public void OnJoinedRoom()
    {
        playerCount[k].text = PhotonNetwork.playerList.Length.ToString() + "/" + Convert.ToString(maxplayer);
    }
    public void exitroom(int i)//выйти
    {
        if (PhotonNetwork.playerList.Length == 1)
        {
            zayavka[i].SetActive(true);
            exit[i].SetActive(false);
            okno_room[i].SetActive(false);
        }
        else
        {
            zayavka[i].SetActive(true);
            exit[i].SetActive(false);
        }
        create.interactable = true;
        PhotonNetwork.LeaveRoom();
        Debug.Log("Onleft room");
    }
    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        print("Disconnect");
        Debug.LogError("Cause: " + cause);
    }
}
/*   
public void OnJoinedLobby()
{
удаление лишних копий объектов чата почему то приводит к паденю браузеров и юнити!!
    GameObject[] chats=GameObject.FindGameObjectsWithTag("chat_lobby");
    if (chats.Length == 2)
    {
        //Destroy(chats[1]);
        //chats[0].GetComponent<Canvas>().enabled = true;
    }
}
*/