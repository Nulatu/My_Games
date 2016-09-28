using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Multi_Tamognya : Photon.MonoBehaviour 
{
    public bool Iz_Tamognya_V_Prison;
    public void multi_tamognya()
    {
        photonView.RPC("tamognya_rpc", PhotonTargets.All);
    }
    [PunRPC]
    public void tamognya_rpc(PhotonMessageInfo info)
    {
        if (info.sender.isLocal)
        {
            GameObject.Find("Canvas").GetComponent<Kletka>().fishka.GetComponent<Transform>().position = GameObject.Find("Prison").GetComponent<Transform>().position;
            GameObject.Find("Canvas").GetComponent<Hod>().hod = 7;// ?? все фишки отправятся в семерку?
        }

        //"[Имя игрока] попал в Таможню, отправлен в Тюрьму."
        string logi_stroka = info.sender.name + ":" + " попал в Таможню, отправлен в Тюрьму и пропускает три хода.";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
    }
}
