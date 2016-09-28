using UnityEngine;
using System.Collections;

public class rasstanovka : Photon.MonoBehaviour
{
    // это для того чтобы объекты не респаунились друг на друге
    void Awake()
    {
        if (!photonView.isMine)
            GetComponent<Rigidbody2D>().isKinematic = true; // тот кто не создавал эту еду, не должен видеть физику иначе дергаются объекты еды.
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "eda")
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * 20);

    }
    void OnCollisionExit2D(Collision2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
    [PunRPC]
    public void Destroy_eda()
    {
        Destroy(gameObject);
    }
}
