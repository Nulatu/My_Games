using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour
{
	public void exit_ () 
    {
        PhotonNetwork.LeaveRoom();
        //Application.LoadLevel("Lobby");//сюда нельзя иначе лишний раз заходим в иф if (PhotonNetwork.inRoom) и ссылкаемся на несуществующие элементы	
	}
    public void OnLeftRoom()
    {
        print("leftroom");
        Application.LoadLevel("Lobby");
    }
}
