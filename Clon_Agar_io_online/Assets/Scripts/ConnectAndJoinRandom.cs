using System;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class ConnectAndJoinRandom : Photon.MonoBehaviour
{
    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    [HideInInspector]
    public bool AutoConnect = true;

    public GameObject Prefab_player;

    public string Version;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject load_RU;
    [HideInInspector]
    public GameObject load_ENG;
    private bool ConnectInUpdate = true;
    public bool i_play_in_server = false;
    [HideInInspector]
    public Transform stenka_up;

    public GameObject Prefab_eda;
    public int kol_vo;
    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;
        //если поставить autoJoinLobby тру то у нас мастер клиент не попадает в кулбек OnConnectedToMaster, а попадает сразу в кулбек OnJoinedLobby
        //есть два варианта связок autoJoinLobby false 
        //OnConnectedToMaster - Lobby - Room
        //а можно и сразу из  OnConnectedToMaster в Room 
    }

    public virtual void Update()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            //Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");
            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version);
        }
    }


    public virtual void OnConnectedToMaster()
    {
        //print("OnConnectedToMaster");
        //тот кто первый законектился к фотону потом Попадает в кулбек OnPhotonRandomJoinFailed т.к. не может присоединиться к рандомной несуществующей комнате
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnJoinedLobby()
    {
        //print("OnJoinedLobby");
        //Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }
    public virtual void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = 20 }, null);
    }
    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        PhotonNetwork.player.SetCustomProperties(new Hashtable
            {
                {"Ping_Player",PhotonNetwork.GetPing() }
            });

        if (PhotonNetwork.gameVersion == "test")
        {
            player = PhotonNetwork.Instantiate(Prefab_player.name, new Vector3(0, 0, 0), Quaternion.identity, 0);

        }
        else if (!Application.isEditor || Application.isEditor && i_play_in_server)
        {
            float tochka_predela = stenka_up.position.y - Prefab_player.transform.localScale.magnitude; // при условии что поле = квадрат!
            player = PhotonNetwork.Instantiate(Prefab_player.name, new Vector3(UnityEngine.Random.Range(-tochka_predela, tochka_predela), UnityEngine.Random.Range(-tochka_predela, tochka_predela), 0), Quaternion.identity, 0);

            //PhotonNetwork.InstantiateSceneObject(Prefab_eda.name, new Vector3(UnityEngine.Random.Range(-tochka_predela, tochka_predela), UnityEngine.Random.Range(-tochka_predela, tochka_predela), 0), Quaternion.identity, 0, null);
            if (PhotonNetwork.player.isMasterClient)
            {
                for (int i = 1; i <= kol_vo; i++)
                {
                    PhotonNetwork.InstantiateSceneObject(Prefab_eda.name, new Vector3(UnityEngine.Random.Range(-tochka_predela, tochka_predela), UnityEngine.Random.Range(-tochka_predela, tochka_predela), 0), Quaternion.identity, 0, null);
                }
            }
        }

        load_RU.SetActive(false);
        load_ENG.SetActive(false);
    }
}
