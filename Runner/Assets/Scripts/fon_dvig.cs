using UnityEngine;
using System.Collections;

public class fon_dvig : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeZ;
    private Vector3 startPosition;
    public Transform player;
    float times = 2;
    void Start ()
    {
        startPosition = new Vector3(player.position.x,player.position.y,5);
    }
	void FixedUpdate()
    {
        times -= Time.deltaTime;
    }

	void Update ()
    {
        if (times < 0)
        {
            startPosition = new Vector3(player.position.x, player.position.y, 5);
            times = 2;
        }
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        //gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(value_hp, 0);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
