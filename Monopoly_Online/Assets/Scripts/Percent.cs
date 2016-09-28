using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Percent : MonoBehaviour
{
    public Text percent;
    public float f; //показатель прозрачности красного квадрата
    void Start()
    {
        if(Application.loadedLevelName =="Lobby")
            StartCoroutine("Zapusk_percent",3F);
        else
            StartCoroutine("Zapusk_percent",3F);
    }
    IEnumerator Zapusk_percent(float kol)
    {
        for (float f = 0f; f <= 100; f += kol)
        {
            percent.text = f.ToString();
            yield return new WaitForSeconds(.015f); 
            yield return null; 
        }
    }
}
