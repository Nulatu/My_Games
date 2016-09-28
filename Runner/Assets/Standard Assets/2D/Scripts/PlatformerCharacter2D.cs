using System;
using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float ostanovka = 1f;
        private Rigidbody2D m_Rigidbody2D;
        public GameObject canvas;

        public GameObject[] target;
        public int hod = 2;

        private void Awake()
        {
            canvas = GameObject.Find("UI").gameObject;
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            target = new GameObject[5];
            for (int i = 0; i <= 4; i++)
            {
                target[i] = GameObject.Find(i.ToString());
            }
            //gameObject.transform.localPosition = new Vector3 target[3].transform.localPosition.x;
        }

        IEnumerator MyCoroutine(GameObject target)
        {
            //print("zahod");
            while (Vector2.Distance(new Vector2(1,transform.localPosition.y),new Vector2(1, target.transform.localPosition.y)) > 1)
            {
                //print(transform.localPosition.y);
                //print(target.transform.localPosition.y);
                transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(gameObject.transform.localPosition.x, target.transform.localPosition.y,0), m_MaxSpeed * Time.deltaTime); 
                //print("clik");
                yield return null;
            }
        }
        public void Move(float move, bool jump,bool down)
        {

            if (jump)
            {
                if(hod==0)
                    return ; // для того чтобы игрок не прыгнул верх и не спрятался от врагов за полоской хп(наверху)
                //m_Rigidbody2D.velocity = new Vector2(0, m_MaxSpeed);
                StartCoroutine(MyCoroutine(target[--hod]));
                Invoke("Stop", ostanovka);
            }
            if (down )
            {
                if (hod==4)
                    return; // для того чтобы игрок не спрыгнул за пределы игры(канваса)
                //m_Rigidbody2D.velocity = new Vector2(0, -m_MaxSpeed);
                StartCoroutine(MyCoroutine(target[++hod]));
                Invoke("Stop", ostanovka);
            }
        }

        void Stop()
        {
            m_Rigidbody2D.velocity = new Vector2(0, 0);
        }

    }
}
