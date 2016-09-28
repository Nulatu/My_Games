using UnityEngine;
using System.Collections;
public class Plita : MonoBehaviour 
{
    public GameObject zabl;
    public GameObject zabr;
    public GameObject plita;
    public BoxCollider2D box;
    public int speed;
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.LeftArrow) &&zabl.transform.position.x +1< plita.transform.position.x - box.size.x / 4)
			transform.position+=Vector3.left*Time.deltaTime*speed;

		if (Input.GetKey(KeyCode.RightArrow) && plita.transform.position.x + box.size.x / 4 < zabr.transform.position.x-1)
            transform.position += Vector3.right * Time.deltaTime * speed;	
    }
}
