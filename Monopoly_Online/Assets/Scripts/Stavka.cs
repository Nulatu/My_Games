using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Stavka : Photon.MonoBehaviour 
{
    public Slider sly;
    public void pokaz()
    {
        Convert.ToInt32(GameObject.Find("Slider").GetComponent<Slider>().maxValue = Convert.ToInt32(PhotonNetwork.player.customProperties["valuta"]));

        int value;
        value = Convert.ToInt32(GameObject.Find("Slider").GetComponent<Slider>().value);
        value -= value % 1000;
        GetComponent<Text>().text = value.ToString();

        //GetComponent<Text>().text = (Convert.ToInt32(GameObject.Find("Slider").GetComponent<Slider>().value)).ToString(); // казино
    }
    public void pokaz_auk()
    {
        print("sss");
        sly.maxValue = Convert.ToInt32(PhotonNetwork.player.customProperties["valuta"]);
        int value;
        value = Convert.ToInt32(sly.value);
        value -= value %1000;
        GetComponent<Text>().text = value.ToString();

        if (GetComponent<Text>().text != PhotonNetwork.room.customProperties["Stavka_Tek_prise"].ToString())
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_plus", "true" } });
        else PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_plus", "false" } });

        GameObject.Find("Stavka_Tek_prise").GetComponent<Text>().text = GetComponent<Text>().text;
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "Stavka_Tek_prise", GetComponent<Text>().text } });
    }
}
