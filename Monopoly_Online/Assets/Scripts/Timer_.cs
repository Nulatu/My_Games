using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;
/*
Предметы блокировки в скрипте Timer_
1) сделка - torg
2) kletka_open для блокирования метода скрипта Kletka(управление клеткой на расстоянии)
3) кнопка кубик

*/
public class Timer_ : Photon.MonoBehaviour 
{
    public Text timer;
    public float timer_;

    public GameObject[] torg;
    public bool kletka_open;
    public GameObject kubik;

    public Sprite timer_sprite;
    public Sprite timer_red;

    public bool odin_zahod;

    public float timer_value;

    public Image load;
    //public int i;
	public void Start() 
    {
        //i = 1;
        timer_value = 100;
        kubik.GetComponent<Button>().interactable = false;
        foreach(GameObject tor in torg)
        {
            tor.SetActive(false);
        }

        if (PhotonNetwork.player.isMasterClient)
        {
             Invoke("hod_", 1);
        }
        Invoke("load_",2);
	}
    public void load_()
    {
        load.enabled = false;
        timer_ = timer_value;
    }
    public void hod_()
    {
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_hod", 1 } });
        photonView.RPC("Hod_rpc", PhotonTargets.All);
    }
    void FixedUpdate() 
    {
        timer_ = timer_ - Time.deltaTime;
        timer.text = Convert.ToInt32(timer_).ToString();
        
        if (timer_ <= 20 && odin_zahod==false)
        {
            odin_zahod = true;
            GetComponent<Image>().sprite = timer_red;
        }

        if (timer_ <= 0)
        {
            timer_ = timer_value;
            odin_zahod = false;
            if (PhotonNetwork.player.customProperties["hodit"].ToString() == "true" && GameObject.Find("Canvas").GetComponent<PLayers>().player[Convert.ToInt32(PhotonNetwork.player.customProperties["index"])].gameObject == true)
            {
                if(PhotonNetwork.player.customProperties["flag_sdat"].ToString() != "") // условие else if Для того чтобы не было два вызва hod_next_igrok
                    GameObject.Find("Canvas").GetComponent<PLayers>().sdats();
                else if(GameObject.Find("Multi").GetComponent<Moskow>().okno_torga.activeInHierarchy == false)
                    GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
                levels_();
                kubik.GetComponent<Button>().interactable=false;
                if (GameObject.Find("Multi").GetComponent<Moskow>().okno_torga.activeInHierarchy == true)
                    GameObject.Find("Multi").GetComponent<Moskow>().okno_torga.SetActive(false);
            }            
        }
	}
    public IEnumerator Levels()
    {
        yield return new WaitForSeconds(0.5f);
        photonView.RPC("del_bankrot_rpc", PhotonTargets.All);
        PhotonNetwork.LeaveRoom();
    }
    public void levels_()
    {
        StartCoroutine(Levels());
    }
    [PunRPC]
    public void del_bankrot_rpc(PhotonMessageInfo info)
    {
        Destroy(GameObject.Find("Canvas").GetComponent<PLayers>().player[Convert.ToInt32(info.sender.customProperties["index"])].gameObject);
    }
    [PunRPC]
    public void Hod_rpc(PhotonMessageInfo info)
    {
        timer_ = timer_value;
        GetComponent<Image>().sprite = timer_sprite;
        odin_zahod = false;
        int i = Convert.ToInt32(PhotonNetwork.room.customProperties["index_hod"].ToString());

        for (int s = 1; s <= 5; s++)
            if (i == s && GameObject.Find("Canvas").GetComponent<PLayers>().player[i - 1] == false)
            {
                i++;
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_hod", i } });
            }

        if (i == 6)
        {
            i = 1;//Обязательно!
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_hod", 1 } }); //чтобы последний игрок вызвавший аук передал аук первому игроку и тот его открыл
        }


        if (PhotonNetwork.player.ID == i && PhotonNetwork.player.customProperties["hodit"].ToString() == "false")
        {
            PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "hodit", "true" } });

            kletka_open = true;
            kubik.GetComponent<Button>().interactable = true;
            torg[i - 1].SetActive(true);
            torg[i - 1].GetComponent<Button>().interactable = true;
            i++;
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_hod", i } });
        }
        else
        {
            kubik.GetComponent<Button>().interactable = false;
            foreach (GameObject tor in torg)
            {
                tor.SetActive(false);
            }
            if (PhotonNetwork.player.isLocal)
            {//&& PhotonNetwork.player.ID != i
                torg[PhotonNetwork.player.ID - 1].SetActive(true);
                torg[PhotonNetwork.player.ID - 1].GetComponent<Button>().interactable = false;
            }
            PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "hodit", "false" } });
        }

        for (int s = 1; s <= 5; s++)
            if (i == s && GameObject.Find("Canvas").GetComponent<PLayers>().player[i - 1] == false)
            {
                i++;
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_hod", i } });
            }

        if (i == 6)
        {
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "index_hod", 1 } }); // чтобы запускать цикл сначала после нажатия кнопки да или нет   
        }

    }
}
