using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventMenu : MonoBehaviour
{
    float time_ = 0;
    void Update()
    {
        time_ += Time.deltaTime;
        if (time_ >= 5)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                SceneManager.LoadScene("No_Connect");
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "No_Connect")
                    SceneManager.LoadScene("Game");
            }
            time_ = 0;
        }
        
    }

    public void Knopka(string eventmenu)
    {
        if (eventmenu == "Exit") Application.Quit();
        SceneManager.LoadScene(eventmenu);
    }  
}
