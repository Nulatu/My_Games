using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fon_levels : MonoBehaviour
{
    public Sprite[] obj;
    void Start()
    {
        int i = Random.Range(0, 7);
        gameObject.GetComponent<Image>().sprite = obj[i];
    }
}