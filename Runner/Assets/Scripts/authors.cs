using UnityEngine;
using System.Collections;

public class authors : MonoBehaviour {

	// Use this for initialization
	public void Pokaz_authors (string name)
    {
        if(name == "laguha")
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.FixApp.Frog2");
        if (name == "zayka")
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.FixApp.Bunny_Adventure");
        if (name == "almaz")
            Application.OpenURL("https://vk.com/thecrystalsgame");
        if (name == "mouse")
            Application.OpenURL("https://vk.com/mousesgame");
        if (name == "vk")
            Application.OpenURL("https://vk.com/club50285882");
        if (name == "twitter")
            Application.OpenURL("https://mobile.twitter.com/allspeinn");

    }
}
