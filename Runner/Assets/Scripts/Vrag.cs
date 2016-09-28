using UnityEngine;
using System.Collections;

public class Vrag : MonoBehaviour
{
    public HUDScript hud;
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float speed;
    private void Awake()
    {
        speed = HUDScript.Speed;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("UI").GetComponent<Transform>().transform);
        m_Rigidbody2D.velocity = new Vector2(-speed, 0);
        hud = GameObject.Find("UI").GetComponent<HUDScript>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(gameObject.tag == "vrag10")
            {
                hud.Minus_HP(10);
            }
            else if(gameObject.tag == "vrag50")
            {
                hud.Minus_HP(50);
            }
            else if (gameObject.tag == "vrag100")
            {
                hud.Minus_HP(100);
            }
            Destroy(this.gameObject);
        }
    }
}
