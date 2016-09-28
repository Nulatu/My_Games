using UnityEngine;
using System.Collections;

public class ima : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //gameObject.GetComponent<RectTransform>().offsetMax= new Vector2(60, 60);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(110,110);
        //gameObject.GetComponent<RectOffset>().top = 2;
        //gameObject.GetComponent<RectTransform>().offsetMin=new Vector2(-60,-60);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
