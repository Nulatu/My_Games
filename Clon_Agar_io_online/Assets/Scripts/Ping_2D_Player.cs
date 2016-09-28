using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Ping_2D_Player : Photon.MonoBehaviour
{
    public int ping_2D_Player;
    void print_ping_player()
    {
        PhotonNetwork.player.SetCustomProperties(new Hashtable
            {
                {"Ping_Player",PhotonNetwork.GetPing() }
            });

        ping_2D_Player = Convert.ToInt32(photonView.owner.customProperties["Ping_Player"]);
    }        
    void Awake()
    {
        if (photonView.isMine)
        {
            InvokeRepeating("print_ping_player", 0f, 5f); // для теста пинга
        }
    }
}
