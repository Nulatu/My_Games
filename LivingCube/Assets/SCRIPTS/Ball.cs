using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
//[RequireComponent(typeof(Animator))]
public class Ball : MonoBehaviour {
	Rigidbody2D rb;
	[SerializeField] float delta = 100f;
	[SerializeField] float speed = 10f;
	[SerializeField]
	[Range (0.1f, 1f)]
	float minSpeed = 1f;
	Vector2 lastPos;
	void Start () 
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 5f), Random.Range(1f, 5f));//запускаем мяч

		rb = GetComponent<Rigidbody2D>();
		lastPos = transform.position;
	}
    
	void OnCollisionEnter2D(Collision2D coll) 
    {

        if (Mathf.Abs(lastPos.y - transform.position.y) < delta)
         rb.velocity *= 1.3f;

        if (Mathf.Abs(lastPos.x - transform.position.x) < delta)
            rb.velocity *= 1.3f; 

		lastPos = transform.position;
	}

	void FixedUpdate () 
    {
        if (Skills.timerboll != 0) speed = 16f;
        if (Skills.timerboll == 0) speed = 10f;
		Vector2 vel = rb.velocity;

        if (vel.sqrMagnitude > speed * speed) 
        {
            rb.velocity = Vector2.ClampMagnitude(vel, speed);
		}
		else if (vel.sqrMagnitude < speed * speed * minSpeed * minSpeed)
        {
			rb.velocity = vel * 1.3f;
		}
	}
}
