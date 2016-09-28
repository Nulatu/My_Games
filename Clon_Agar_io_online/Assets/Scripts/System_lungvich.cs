using UnityEngine;
using System.Collections;

public class System_lungvich : MonoBehaviour
{
    public GameObject but_play_ENG;
    public GameObject but_play_RU;
    public void Start()
    {
        if (Application.systemLanguage.ToString() == "Russian")
        {
            but_play_RU.SetActive(true);
            but_play_ENG.SetActive(false);
        }
        else
        {
            but_play_ENG.SetActive(true);
            but_play_RU.SetActive(false);
        }
    }
}
