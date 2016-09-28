using UnityEngine;
using System.Collections;

public class Popodanie : MonoBehaviour
{
    public Sprite tex_smile;
    public Sprite surprise;//удивление
    static public float timer_stun;
    static public float time_smile;
    public Sprite raspad_1;

    public AudioSource touchCube;
    public AudioSource deathEnemy;
    public AudioSource deathCube;
    public AudioSource hitWall;
    public AudioSource hitPlatformMiddle;
    public AudioSource hitPlatformSide;
    public AudioSource fireballhit_wall;

    void Start()
    {
        /*пример реализации без статика
        Skills skill = GetComponent<Skills>();

        skill.fight = 21;
        print(skill.fight);
         */
    }
    void Update()
    {
        time_smile = time_smile - Time.deltaTime;
        if (Skills.timerboll != 0) face_cube.texture = surprise;  
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Cube" && Skills.timerboll == 0 || c.gameObject.tag == "K_Cube")
        {
            sound_objectt.musik(touchCube);
            time_smile = 0.3f;
            face_cube.texture = tex_smile;
        }
        if (c.gameObject.tag == "Dangerous_Cube")
        {
            sound_objectt.musik(deathEnemy);
        }


        if (c.gameObject.tag == "Cube" || c.gameObject.tag == "Dangerous_Cube")
        {
            if (Skills.timerboll != 0)
            {
                sound_objectt.musik(deathCube);
            }
        }
        if (c.gameObject.tag == "Zabor_")
        {
            time_smile = 0.4f;
            face_cube.texture = tex_smile;
        }
        //звук
        if (c.gameObject.tag == "Zabor_" || c.gameObject.tag == "Zabor_right" || c.gameObject.tag == "Zabor_left" || c.gameObject.tag == "Zabor_up")
        {
            if (Skills.timerboll != 0)
            {
                sound_objectt.musik(fireballhit_wall);
            }
                sound_objectt.musik(hitWall);
        }
        
        if (c.gameObject.tag == "boxup" )sound_objectt.musik(hitPlatformMiddle);
        
        if (c.gameObject.tag == "boxright" || c.gameObject.tag == "boxleft")sound_objectt.musik(hitPlatformSide);
        
       
    }
}