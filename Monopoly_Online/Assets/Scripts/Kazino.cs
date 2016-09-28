using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Kazino : Kletka
{
    public GameObject okno_kazino;
    public Sprite[] lots;
    public GameObject zapus_kazino;
    public GameObject close_kazino;

    public override void Povedenie(myClass Moskow_)
    {

        okno_kazino.SetActive(true);
        zapus_kazino.SetActive(true);
        close_kazino.SetActive(false);
        if (GameObject.Find("Okno_moskow") != null) GameObject.Find("Okno_moskow").SetActive(false);
        GameObject.Find("Rezultat").GetComponent<Text>().text = "Удачи!";
    }
    public void zapusk_kazino()
    {
        GetComponent<Multi_Kazino>().multi_kazino();

        zapus_kazino.SetActive(false);
        close_kazino.SetActive(true);

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
            GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
    }
}

