 using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tamognya : Kletka
{
    public bool Iz_Tamognya_V_Prison;
    public override void Povedenie(myClass Moskow_)
    {

        if (GameObject.Find("Okno_kazino") != null) GameObject.Find("Okno_kazino").SetActive(false);
        if (GameObject.Find("Okno_moskow") != null) GameObject.Find("Okno_moskow").SetActive(false);

        GetComponent<Multi_Tamognya>().multi_tamognya();

        Iz_Tamognya_V_Prison=true;

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
            GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
    }
}
