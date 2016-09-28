using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;
[RequireComponent(typeof(PhotonView))]
public class Pokraska : Photon.MonoBehaviour
{
    private int assignedColorForUserId;
    SpriteRenderer m_Renderer;
    void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (photonView.viewID != assignedColorForUserId && gameObject.tag == "eda")
        {
            if (PhotonNetwork.isMasterClient)
            {
                float r = UnityEngine.Random.Range(0.1f, 1f);
                float g = UnityEngine.Random.Range(0.1f, 1f);
                float b = UnityEngine.Random.Range(0.1f, 1f);
                int kod = photonView.viewID;
                PhotonNetwork.room.SetCustomProperties(new Hashtable()
                {
                    {kod+"_r",r },
                    {kod+"_g",g },
                    {kod+"_b",b }
                });
                m_Renderer.color = new Color(r, g, b, 1f);
                assignedColorForUserId = photonView.viewID;
            }
            else
            {
                int kod = photonView.viewID;
                float r = Convert.ToSingle(PhotonNetwork.room.customProperties[kod + "_r"]);
                float g = Convert.ToSingle(PhotonNetwork.room.customProperties[kod + "_g"]);
                float b = Convert.ToSingle(PhotonNetwork.room.customProperties[kod + "_b"]);
                m_Renderer.color = new Color(r, g, b, 1f);
                assignedColorForUserId = photonView.viewID;
            }
        }
        else if (photonView.viewID != assignedColorForUserId && gameObject.tag == "Player")
        {
            if (photonView.isMine)
            {
                float r = UnityEngine.Random.Range(0.1f, 1f);
                float g = UnityEngine.Random.Range(0.1f, 1f);
                float b = UnityEngine.Random.Range(0.1f, 1f);
                int kod = photonView.viewID;
                PhotonNetwork.player.SetCustomProperties(new Hashtable()
                {
                    {kod+"_r",r },
                    {kod+"_g",g },
                    {kod+"_b",b }
                });
                m_Renderer.color = new Color(r, g, b, 1f);
                assignedColorForUserId = photonView.viewID;
            }
            else
            {
                int kod = photonView.viewID;
                if (photonView.owner.customProperties[kod + "_r"] == null) return; // это когда мы хотим взять свойства игрока который еще не успел присвоить себе свойство во время старта(т.е. берем нулы,черный цвет)

                float r = Convert.ToSingle(photonView.owner.customProperties[kod + "_r"]);
                float g = Convert.ToSingle(photonView.owner.customProperties[kod + "_g"]);
                float b = Convert.ToSingle(photonView.owner.customProperties[kod + "_b"]);
                m_Renderer.color = new Color(r, g, b, 1f);
                assignedColorForUserId = photonView.viewID;
            }
        }
    }
}
