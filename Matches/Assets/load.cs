using UnityEngine;
using UnityEngine.UI;

public class load : MonoBehaviour
{

    float time_=0.5f;
    int kol_vo_tochek;
    string stroka_bez_tochek;
    void Start()
    {
        stroka_bez_tochek = gameObject.GetComponent<Text>().text;
    }
	void Update ()
    {
        time_ += 0.01f; // + Time.deltatime не работает в паузе 

        if (time_ >= 1f)
        {
            gameObject.GetComponent<Text>().text += ".";
            if (kol_vo_tochek == 3)
            {
                gameObject.GetComponent<Text>().text = stroka_bez_tochek;
                kol_vo_tochek = -1;
            }
            time_ = 0;
            kol_vo_tochek++;
        }
    }
}
