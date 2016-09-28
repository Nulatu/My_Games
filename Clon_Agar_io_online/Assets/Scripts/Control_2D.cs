using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Control_2D : Photon.MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2D;
    void Awake()
    {
        Invoke("Inic", 0.5f);
        if (photonView.isMine && gameObject.transform.GetChild(0).tag == "MainCamera")
        {//нельзя способом SetActive(false) ! тогда фотоновские префабы после выхода игрока не будут удалятся.т.к. их не видно
            gameObject.transform.GetChild(0).GetComponent<Camera>().enabled = true; // нельзя сет актив фалс на PhotonNetwork.Instantiate объектах 
            //иначе объект не удалится когда игрок выйдет у другого игрока
        }
        if (!photonView.isMine && gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Inic()
    {
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Nick_UI>().enabled = true;
        }
    }
    void Update()
    {
        if (!photonView.isMine)
        {
            return;
        }
        if (gameObject.tag != "MainCamera")
        {//0.05f
            rb2D.MovePosition(rb2D.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * 0.05f * Time.fixedDeltaTime); //*Time.fixedDeltaTime
        }
    }
}
