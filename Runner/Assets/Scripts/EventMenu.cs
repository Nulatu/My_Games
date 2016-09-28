using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EventMenu : MonoBehaviour
{
    public GameObject pravil_control_ui;
    public GameObject button_new_game;
    public GameObject pauza;
    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Game"&& PlayerPrefs.GetString("control_pokaz") == "")
        {
            Pauza();
            pravil_control_ui.SetActive(true);
            PlayerPrefs.SetString("control_pokaz","close");
        }
        if (SceneManager.GetActiveScene().name == "Menu"&& PlayerPrefs.GetString("Game_new") == "open")
            button_new_game.SetActive(true);
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauza.SetActive(true);
                Pauza();
            }
        }
    }
    public void Knopka(string eventmenu)
    {
        if (eventmenu == "Exit") Application.Quit();
        SceneManager.LoadScene(eventmenu);
        //Application.LoadLevel(eventmenu);
    }
    public void Pauza()
    {
        Time.timeScale = 0;
    }
    public void Exit_Pauza()
    {
        Time.timeScale = 1;
    }
}
