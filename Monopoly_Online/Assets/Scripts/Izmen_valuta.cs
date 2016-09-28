using UnityEngine;
using System.Collections;

public class Izmen_valuta : MonoBehaviour
{
    void Start()
    {
        Invoke("izmen_valuta", 1);// это для того чтобы расположение цветовой валюты динамически изменялась на старте т.е. динамическая изменяется цвет фишек(создание комнат)
    }
    void izmen_valuta()
    {
        gameObject.name = "Valuta_" + GameObject.Find("Canvas").GetComponent<PLayers>().Color[int.Parse(transform.parent.GetComponent<Transform>().name)].name;
    }
}
