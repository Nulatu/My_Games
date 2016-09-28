using UnityEngine;
using System.Collections;

public class Player_Povedenie : Photon.MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D other)
    {
        if (!photonView.isMine) return;
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.transform.localScale.x < gameObject.transform.localScale.x)
            {
                photonView.RPC("Big", PhotonTargets.AllBuffered);
                other.gameObject.GetComponent<PhotonView>().RPC("Destroy_player", PhotonTargets.AllBuffered);
            }
            else if (other.gameObject.transform.localScale.x > gameObject.transform.localScale.x)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("Big", PhotonTargets.AllBuffered);
                photonView.RPC("Destroy_player", PhotonTargets.AllBuffered);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "eda")
        {
            other.gameObject.GetComponent<PhotonView>().RPC("Destroy_eda",PhotonTargets.AllBuffered);
            photonView.RPC("Big",PhotonTargets.AllBuffered);
        }
    }
    [PunRPC]
    public void Big()
    {
        gameObject.transform.localScale +=Vector3.one*0.08f;
    }
    [PunRPC]
    public void Destroy_player()
    {
        if (photonView.isMine)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            System_lungvich zagruzka = GameObject.Find("UI").GetComponent<System_lungvich>();
            zagruzka.Start();
            Invoke("Exit", 3f);
        }
        else
            Destroy(gameObject);
    }
    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
    }
}
