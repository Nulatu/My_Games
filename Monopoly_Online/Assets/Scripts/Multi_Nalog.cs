using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Multi_Nalog : Photon.MonoBehaviour
{
    public void multi_nalog()
    {
        int nalog = Random.Range(1, 5) * 50000;
        photonView.RPC("nalog_rpc",PhotonTargets.All,nalog);

    }
    [PunRPC]
    public void nalog_rpc(int nalog,PhotonMessageInfo info)
    {
		int valuta_igroka = int.Parse (info.sender.customProperties ["valuta"].ToString());
		valuta_igroka -= nalog;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

        if (valuta_igroka < 0) // удаляем банкрота
        {
            GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = "0";
            Destroy(GameObject.Find("Canvas").GetComponent<PLayers>().player[int.Parse(info.sender.customProperties["index"].ToString())].gameObject);
            if (info.sender.isLocal)
            {
                GameObject.Find("Multi").GetComponent<Multi>().proigrish.SetActive(true);
                GameObject.Find("Kubik_two").GetComponent<Button>().interactable = false;
                Invoke("exit",2);
            }

        }


		//"[Имя игрока] обязан оплатит налог размером $[сумма]."
		string logi_stroka = info.sender.name + ":" + " обязан оплатить налог размером " + nalog.ToString () + ".";
		logi_stroka = logi_stroka.Replace ("\n", "");
		logi_stroka += "\n";
		GameObject.Find ("Text_logi").GetComponent<Text> ().text += logi_stroka;
		//"[Имя игрока] оплатил штраф в $[сумма]K."
		logi_stroka = info.sender.name + ":" + " оплатил штраф в " + nalog.ToString () + ".";
		logi_stroka = logi_stroka.Replace ("\n", "");
		logi_stroka += "\n";
		GameObject.Find ("Text_logi").GetComponent<Text> ().text += logi_stroka;        
    }
    public void exit()
    {
        PhotonNetwork.LeaveRoom();
    }
}
