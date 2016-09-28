using UnityEngine;
using System.Collections;

public class BackgroundSeter : MonoBehaviour 
{

    public Texture2D Background;
    private GUITexture tex;
    private Vector2 oldScreenSize;


	void Start () {
        oldScreenSize.x = Screen.width;
        oldScreenSize.y = Screen.height;
        tex = GetComponent<GUITexture>();
        tex.texture = Background;
        RecanculateSize();
	}

	void FixedUpdate () {
        if (oldScreenSize.x != Screen.width || oldScreenSize.y != Screen.height)
        {
            //print("Recanculate Size");
            RecanculateSize();
            oldScreenSize.x = Screen.width;
            oldScreenSize.y = Screen.height;
        }
	}

    public void RecanculateSize()
    {
        Rect _new =  tex.pixelInset;
        _new.x = -((Screen.width + 2) / 2);
        _new.y = -((Screen.height + 2) / 2);
        _new.width = Screen.width + 2;
        _new.height = Screen.height + 2;
        tex.pixelInset = _new;
    }
}
