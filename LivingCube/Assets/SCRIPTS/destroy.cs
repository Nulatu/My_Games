using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour 
{
    public Animator destroy_;
    public string level;
    public GameObject sound;
    public GameObject[] so;
    void Awake()
    {
        int i = 0;
        so = GameObject.FindGameObjectsWithTag("sou");
        foreach (GameObject go in so)
        {
            ++i;
            if (i == 2) Destroy(go);
        }
    }
    public void destroyself()
    {
        Destroy(gameObject);
        if (transform.tag == "Ball")
        {
        DontDestroyOnLoad(sound);
        level=Application.loadedLevelName;
        Application.LoadLevel(level);
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ball" )
        {
            if(transform.tag=="Dangerous_Cube"||transform.tag=="Cube"&&Skills.timerboll!=0)
            destroy_.enabled = true;
            if (transform.tag == "Dangerous_Cube")
            {
                sound_objectt.musik(sound.GetComponent<AudioSource>());
                destroy_.enabled = true;//крик живого куба
            }
        }
        if (transform.tag == "Ball"&&c.gameObject.tag=="Zabor_")
        {
            destroy_.enabled = true;
        }

    }
}
