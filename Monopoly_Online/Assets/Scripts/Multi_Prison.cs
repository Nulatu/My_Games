using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Multi_Prison : Photon.MonoBehaviour
{
    public void multi_prison()
    {
        photonView.RPC("prison_rpc", PhotonTargets.All);

    }
    [PunRPC]
    public void prison_rpc(PhotonMessageInfo info)
    {
        //"[Имя игрока] попал в Тюрьму, но был отпущен из-за недостатка улик."
        string logi_stroka = info.sender.name + ":" + " попал в Тюрьму, но был отпущен из-за недостатка улик.";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
    }
}
