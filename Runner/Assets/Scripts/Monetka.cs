using UnityEngine;
using System.Collections;

public class Monetka : MonoBehaviour
{
    public HUDScript hud;
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float speed;

    private void Awake()
    {
        //gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(90, 90);
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
            hud.IncreaseScore(10);
            Destroy(this.gameObject);
        }
    }
}
