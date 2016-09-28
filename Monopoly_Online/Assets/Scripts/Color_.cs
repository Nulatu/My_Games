using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Color_ : Photon.MonoBehaviour
{
    public Sprite[] colo;

    public Sprite[] Color;

    public void colors(Sprite color)
    {
        for (int i = 0; i <= 4; i++)
        {
            if (color.name == colo[i].name)
            {
                PhotonNetwork.player.SetCustomProperties(new Hashtable
            {
                {"color_index",i},//индекс спрайта фишки мастер клиента 
            });
            }
        }
        Color[0] = color;
        for (int i = 1, s = 0; i <= 4; s++)
        {
            if (color.name != colo[s].name)
            { 
                Color[i] = colo[s];
                i++;
            }
        }
    }
}
