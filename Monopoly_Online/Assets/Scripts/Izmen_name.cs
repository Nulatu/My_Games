using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Izmen_name : Photon.MonoBehaviour
{
    void Start()
    {
        Invoke("izmen_name", 1);// это для того чтобы не ругнулся иф на несуществующую фишку
    }
    public void izmen_name()
    {
        if (GameObject.Find("Canvas").GetComponent<Kletka>().fishka.GetComponent<PhotonView>().ownerId == int.Parse(transform.parent.GetComponent<Transform>().name) + 1)
        {
            string name_ = PhotonNetwork.playerName;
            string zapis_name = "Name_" + transform.parent.GetComponent<Transform>().name.ToString();
            photonView.RPC("set_name", PhotonTargets.All, name_, zapis_name);
        }
    }
    [PunRPC]
    public void set_name(string name_, string zapis_name)
    {
        GameObject.Find(zapis_name).GetComponent<Text>().text = name_;
    }
}