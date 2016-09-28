using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Hod : Photon.MonoBehaviour 
{
    public int hod;
    public int one_kubik;
    public int two_kubik;
	public void Peredvisenie() 
    { 
        one_kubik = Random.Range(1, 7);
        two_kubik = Random.Range(1, 7);

        int vipalo = one_kubik + two_kubik;
        //два хода
        //if (hod != 1)
        //    hod = 1;
        //else hod = 2;
        ///// Три хода
        //if (hod == 1) hod = 2;
        //else if (hod == 2) hod = 11;
        //else hod = 1;
        
        if (one_kubik == two_kubik)
        {
            GameObject.Find("Tamognya").GetComponent<Tamognya>().Iz_Tamognya_V_Prison = false;
        }
        if (GameObject.Find("Tamognya").GetComponent<Tamognya>().Iz_Tamognya_V_Prison == false) hod = hod + vipalo;
        if (hod > 27)
        {
            //200 К за прохождение круга
            photonView.RPC("Krugovo_dohod", PhotonTargets.All);
            hod = hod - 28;
        }
        
        //"[Имя игрока] бросил кости 1:2."
        photonView.RPC("vipalo_rpc", PhotonTargets.All, one_kubik, two_kubik);      
	}
    [PunRPC]
    public void vipalo_rpc(int one_kubik,int two_kubik,PhotonMessageInfo info)
    {
        string logi_stroka = info.sender.name + ":" + " бросил кости " + one_kubik.ToString() + ":" + two_kubik.ToString() + ".";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;
    }
    [PunRPC]
    public void Krugovo_dohod(PhotonMessageInfo info)
    {       
        int valuta_igroka = int.Parse(info.sender.customProperties["valuta"].ToString());
        valuta_igroka += 200000;
        info.sender.SetCustomProperties(new Hashtable { { "valuta", valuta_igroka.ToString() } });
        GameObject.Find("Valuta_" + info.sender.customProperties["color"].ToString()).GetComponent<Text>().text = valuta_igroka.ToString();

        string logi_stroka = info.sender.name + ":" + " получил " + "200000." + "за прохождение круга";
        logi_stroka = logi_stroka.Replace("\n", "");
        logi_stroka += "\n";
        GameObject.Find("Text_logi").GetComponent<Text>().text += logi_stroka;        
    }
}
