using UnityEngine;
using System.Collections;

public class ExitMenu : MonoBehaviour
{
    public GameObject musik_lvl_hard;
    public GameObject musik_lvl;
    public GameObject[] so;
    public string lvl;
    void Awake()
    {
        Skills.timerboll = 0;
        lvl = Application.loadedLevelName;

        int i = 0;
        so = GameObject.FindGameObjectsWithTag("sou_menu");
        foreach (GameObject go in so)
        {
            ++i;
            if (i == 2) Destroy(go);
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Destroy(so[0]);//чтобы в меню не воспроизводилась музыка уровня вместе с музыкой меню
            Application.LoadLevel("Menu");
        }
    }
    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Dangerous_Cube"))
        {
            if(lvl == "Level_1"){ DontDestroyOnLoad(musik_lvl);  Application.LoadLevel("Level_2");}
            if (lvl == "Level_2") { DontDestroyOnLoad(musik_lvl); Application.LoadLevel("Level_3"); }
            if (lvl == "Level_3") { DontDestroyOnLoad(musik_lvl); Application.LoadLevel("Level_4"); }
            if(lvl == "Level_4") Application.LoadLevel("Level_5");
            if (lvl == "Level_5") { DontDestroyOnLoad(musik_lvl_hard); Application.LoadLevel("Level_6"); }
            if (lvl == "Level_6") { DontDestroyOnLoad(musik_lvl_hard); Application.LoadLevel("Level_7"); }
            if (lvl == "Level_7") { DontDestroyOnLoad(musik_lvl_hard); Application.LoadLevel("Level_8"); }
            if (lvl == "Level_8") { DontDestroyOnLoad(musik_lvl_hard); Application.LoadLevel("Level_9"); }
            if (lvl == "Level_9") { DontDestroyOnLoad(musik_lvl_hard); Application.LoadLevel("Level_10"); }
            if (lvl == "Level_10") { Destroy(so[0]); Application.LoadLevel("Menu"); }
            if (lvl == "Level_11") { DontDestroyOnLoad(musik_lvl_hard); Application.LoadLevel("Level_12"); }
            if (lvl == "Level_12") { Destroy(so[0]); Application.LoadLevel("Menu"); }
        }
    }
}
