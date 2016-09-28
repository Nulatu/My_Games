using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("ss");
            //Application.LoadLevel(2);
           // return;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
