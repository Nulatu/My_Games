using UnityEngine;
using System.Collections;

public class meClass
{
    public string name;
}

public class rom : Photon.MonoBehaviour
{
    public static meClass[] okno_room;

    void Start()
    {
        okno_room = new meClass[3];
        for (int i = 0; i < 3; i++)
        {
            okno_room[i] = new meClass();
           // okno_room[i]= GameObject.Find("Stol_" + i);
        }
    }
}
