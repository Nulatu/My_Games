using UnityEngine;
using System.Collections;

public class viravnivanir : MonoBehaviour {

    public GameObject canvas;
    void Start ()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas.GetComponent<Canvas>().pixelRect.width / 19,0);
    }
}
