using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Multi_Bonus : Photon.MonoBehaviour 
{
    public void multi_bonus()
    {
        int bonus = Random.Range(1, 5) * 50000;
        photonView.RPC("bonus_rpc", PhotonTargets.All,bonus);
    }
    [PunRPC]
    public void bonus_rpc(int bonus,PhotonMessageInfo info)
    {

        int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
        valuta_igroka += bonus;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();
        
        //"[Имя игрока] получил бонус в размере $[сумма]K."
        string logi_stroka = info.sender.name + ":" + " получил бонус в размере " + bonus.ToString() + ".";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;

    }
}
