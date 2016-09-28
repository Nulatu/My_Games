using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Multi_Kazino : Photon.MonoBehaviour 
{
    public GameObject okno_kazino;
    public Sprite[] lots;

    public void multi_kazino()
    {
        int value1 = Random.Range(0, 6);
        int value2 = Random.Range(0, 6);
        int value3 = Random.Range(0, 6);
        GameObject.Find("One_lot").GetComponent<Image>().sprite = lots[value1];
        GameObject.Find("Two_lot").GetComponent<Image>().sprite = lots[value2];
        GameObject.Find("Three_lot").GetComponent<Image>().sprite = lots[value3];
        string stavka = GameObject.Find("Stavka").GetComponent<Text>().text;

        photonView.RPC("kazino_rpc", PhotonTargets.All,stavka,value1,value2,value3);
    }
    [PunRPC]
    public void kazino_rpc(string stavka,int value1,int value2,int value3,PhotonMessageInfo info)
    {
        int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());

        if (value1 == value2 && value2 == value3)
        {
            if (info.sender.isLocal)// другой игрок не может видеть этот вывод потому что у него не открыто окно казино
            {
                GameObject.Find("Rezultat").GetComponent<Text>().text = "Джекпот!!";
            }
            int jackpot = int.Parse(stavka);
            if (value1 != 5)
            {
                jackpot = (int.Parse(stavka)) * (value1 + 2);
                valuta_igroka = valuta_igroka + jackpot;
            }
            if (value1 == 5)
            {
                jackpot = (int.Parse(stavka)) * (value1 + 5);
                valuta_igroka = valuta_igroka + jackpot;
            }
            info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
            GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

            //"[Имя игрока] сорвал куш в $[сумма]." 
            string logi_stroka = info.sender.name + ":" + " сорвал куш в " + jackpot.ToString() + ".";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
            //"[Имя игрока] получил выигрыш $[сумма]." 
            logi_stroka = info.sender.name + ":" + " получил выигрыш " + jackpot.ToString() + ".";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
        }
        else
        {
            if (info.sender.isLocal)// другой игрок не может видеть этот вывод потому что у него не открыто окно казино
            {
                GameObject.Find("Rezultat").GetComponent<Text>().text = "Проигрыш";
            }
            valuta_igroka = valuta_igroka - int.Parse(stavka);
            info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
            GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

            //"[Имя игрока] проиграл в казино $[сумма]."
            string logi_stroka = info.sender.name + ":" + " проиграл в казино " + stavka + ".";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
            //"[Имя игрока] оплатил проигрыш $[сумма]."
            logi_stroka = info.sender.name + ":" + " оплатил проигрыш " + stavka + ".";
            logi_stroka = logi_stroka.Replace("\n", "");
            logi_stroka += "\n";
            GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
        }
    }
}
