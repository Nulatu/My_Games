using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

/// <summary>
/// A minimal UI to show connection info in a demo.
/// </summary>
public class IELdemo : MonoBehaviour
{
    public GUISkin Skin;
    public void OnGUI()
    {
        if (this.Skin != null)
        {
            GUI.skin = this.Skin;
        }

        if (PhotonNetwork.connected)
        {
            if (Application.systemLanguage.ToString() == "Russian")
            {
                GUILayout.Label("Игроков на сервере: " + PhotonNetwork.playerList.Length + "/20");
                GUILayout.Label("Ваш пинг: " + PhotonNetwork.GetPing());
            }
            else
            {
                GUILayout.Label("Player on server: " + PhotonNetwork.playerList.Length + "/20");
                GUILayout.Label("Your ping: " + PhotonNetwork.GetPing());
            }
        }
    }
}
