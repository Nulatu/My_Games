using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public GameObject sound;
    public int Fil;
    public string level;
    public GameObject[] so;
    void Awake()
    {
        Fil = 3;
        int i = 0;
        so = GameObject.FindGameObjectsWithTag("sou");
        foreach (GameObject go in so)
        {
            ++i;
            if(i==2)Destroy(go);
        }
        DontDestroyOnLoad(sound);

        level=Application.loadedLevelName;
    }
 
     
    public void  start_game()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_1");
    }
    public void Level_2()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_2");
    }
    public void Level_3()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_3");
    }
    public void Level_4()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_4");
    }
    public void Level_5()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_5");
    }
    public void Level_6()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_6");
    }
    public void Level_7()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_7");
    }
    public void Level_8()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_8");
    }
    public void Level_9()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_9");
    }
    public void Level_10()
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_10");
    }
    public void Level_11()//бонус
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_11");
    }
    public void Level_12()//бонус
    {
        Destroy(so[0]);
        Application.LoadLevel("Level_12");
    }
    public IEnumerator Control()
    {
        //DontDestroyOnLoad(sound);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("control");
    }
    public void control_()
    {
        StartCoroutine(Control());
    }

    public IEnumerator Levels()
    {
        DontDestroyOnLoad(sound);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Levels");
    }
    public void levels_()
    {
        StartCoroutine(Levels());
    }
    public IEnumerator Menu_()
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Menu");
    }
    public void menu_()
    {
        StartCoroutine(Menu_());
    }
}
