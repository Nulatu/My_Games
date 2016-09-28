using UnityEngine;
using System.Collections;

public class face_cube : MonoBehaviour
{
    public GameObject cub;
    public Sprite tex;
    static public Sprite texture;
    private float X;
    public float Y;
    public bool flag = false;
    void Start()
    {
        texture = tex;
        GetComponent<SpriteRenderer>().sprite = texture;
    }
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = texture;
        if (Popodanie.time_smile < 0 && Skills.timerboll == 0) 
        { 
            texture = tex; 
            GetComponent<SpriteRenderer>().sprite = texture; 
        }
    }
}
// возможные редкие ошибки - мяч не уничтожит пулю когда она в if (pul.transform.position.y < cub.transform.position.y - 1.5) 