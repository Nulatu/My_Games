using UnityEngine;
using System.Collections;
public class Skills : MonoBehaviour 
{
    public GameObject boxleft;
    public GameObject boxright;
    public GameObject boxup;
    public GameObject boll;
    public float timer_plazma;
    static public float timerboll;
    public int fight; // для теста вызова этой переменной в другом скрипте без статика
    public AudioSource skillOn;
    void Start()
    {
        timer_plazma = 100;
        boxup.GetComponent<ParticleSystem>().Stop(true);
        boxleft.GetComponent<ParticleSystem>().Stop(true);
        boxright.GetComponent<ParticleSystem>().Stop(true);
    }  
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "boxleft" || col.gameObject.tag == "boxright"||col.gameObject.tag == "boxup" )
            if (boxleft.GetComponent<ParticleSystem>().isPlaying && boxright.GetComponent<ParticleSystem>().isPlaying && boxup.GetComponent<ParticleSystem>().isPlaying) 
            {
                boll.GetComponent<ParticleSystem>().Play(true); 
                timerboll = 4;
            }
	}
    public void anim_stop()
    {
        GetComponent<Animator>().enabled = false;
    }
    void Update()
    {
        if(timer_plazma>0) timer_plazma = timer_plazma - Time.deltaTime;
        timerboll = timerboll - Time.deltaTime;
        if (GetComponent<Animator>().enabled ==false&&Input.GetKey(KeyCode.Space))
        {
           GetComponent<Animator>().enabled = true;
           sound_objectt.musik(skillOn);
           timer_plazma=2;
           boxup.GetComponent<ParticleSystem>().Play(true);
           boxleft.GetComponent<ParticleSystem>().Play(true);
           boxright.GetComponent<ParticleSystem>().Play(true);
        }
        if (timerboll < 0) 
        { 
            boll.GetComponent<ParticleSystem>().Stop(true); 
            timerboll = 0; 
        }
    }
    void FixedUpdate()
    {
        if (timer_plazma < 0 )
        {
            boxup.GetComponent<ParticleSystem>().Stop(true);
            boxleft.GetComponent<ParticleSystem>().Stop(true);
            boxright.GetComponent<ParticleSystem>().Stop(true);
        }
    }
}
//(геймплей)//если мяч больше одного раза оттолкнуть горящей плитой, то и гореть он будет чуть больше заданного времени(timerboll).