using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Lobby : MonoBehaviour
{
    void Start() // ����������� ������ ����� ������ �� ������� � �����.
    {
        StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game, Storage.ban, Storage.ban_chat, Storage.kredits));
    }

    private void DrawHome() // ��� ����� ���������� ������ �� ������
    {
        StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game, Storage.ban, Storage.ban_chat, Storage.kredits));
    }
}
