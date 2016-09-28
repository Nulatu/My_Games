using UnityEngine;
using System.Collections;

public class Inic : MonoBehaviour 
{
    [PunRPC]
	public void inic (int i) 
    {
        gameObject.transform.SetParent(Pole.kletki[i].kletka.GetComponent<Transform>().transform);
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
	}

}
