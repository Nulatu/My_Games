using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Prison : Kletka 
{
    public int zadergka;
    public override void Povedenie(myClass Moskow_)
    {

        if (GameObject.Find("Okno_kazino") != null) GameObject.Find("Okno_kazino").SetActive(false);
        if (GameObject.Find("Okno_moskow") != null) GameObject.Find("Okno_moskow").SetActive(false);

        if (GameObject.Find("Tamognya").GetComponent<Tamognya>().Iz_Tamognya_V_Prison == true) ++zadergka;
        if (zadergka == 3) GameObject.Find("Tamognya").GetComponent<Tamognya>().Iz_Tamognya_V_Prison = false;

        GetComponent<Multi_Prison>().multi_prison();

        if (GameObject.Find("Timer").GetComponent<Timer_>().enabled == true)
            GameObject.Find("Multi").GetComponent<Moskow>().hod_next_igrok();
    }
}
