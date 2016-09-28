using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using UnityEngine.UI;
[RequireComponent(typeof(PhotonView))]
public class Nick_UI : Photon.MonoBehaviour
{
    public Text tm;
    void Start()
    {
        PhotonPlayer owner = this.photonView.owner;
        if (owner != null && tm.text == "")
        {
            tm.text = "Player" + owner.ID;
        }
    }
}
