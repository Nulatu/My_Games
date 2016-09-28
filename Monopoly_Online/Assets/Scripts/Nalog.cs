using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Nalog : Kletka
{
    public override void Povedenie(myClass Moskow_)
    {

        if (GameObject.Find("Okno_kazino") != null) GameObject.Find("Okno_kazino").SetActive(false);
        if (GameObject.Find("Okno_moskow") != null) GameObject.Find("Okno_moskow").SetActive(false);

        GetComponent<Multi_Nalog>().multi_nalog();

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
            GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
    }
    
}
