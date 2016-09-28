using UnityEngine;
using System.Collections;
using System;

public class SpawnScript : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMin;
    public float spawnMax;
    public GameObject panel_dorog;
    public GameObject canvas;
    public float razmer_objects;

    void Awake()
    {
        canvas = GameObject.Find("UI").gameObject;
        panel_dorog = GameObject.Find("Panel_dorog").gameObject;
        float razmer_paneli_dorog = canvas.GetComponent<Canvas>().pixelRect.height - (canvas.GetComponent<Canvas>().pixelRect.height * (1 - panel_dorog.GetComponent<RectTransform>().anchorMax.y));
        float razmer_dorogi = (canvas.GetComponent<Canvas>().pixelRect.height - (canvas.GetComponent<Canvas>().pixelRect.height * (1 - panel_dorog.GetComponent<RectTransform>().anchorMax.y))) / 5f;
        float kusichek = canvas.GetComponent<Canvas>().pixelRect.height - razmer_paneli_dorog;
        gameObject.transform.localPosition = new Vector3(canvas.GetComponent<Canvas>().pixelRect.width / 2, canvas.GetComponent<Canvas>().pixelRect.height/2 - kusichek - razmer_dorogi * Convert.ToInt32(gameObject.tag)+ razmer_dorogi/2, 0);
        razmer_objects = razmer_dorogi - (razmer_dorogi * 0.2f) / 2f;
    }
    void Start ()
    {
        //InvokeRepeating("Spawn", UnityEngine.Random.Range(spawnMin, spawnMax), UnityEngine.Random.Range(spawnMin, spawnMax)); // респаунит но только больше
        Invoke("Spawn", UnityEngine.Random.Range(spawnMin, spawnMax));
    }
	void Spawn()
    {
        int ran = UnityEngine.Random.Range(0, obj.GetLength(0));
        obj[ran].GetComponent<RectTransform>().sizeDelta = new Vector2(razmer_objects, razmer_objects);
        Instantiate(obj[ran],transform.position,Quaternion.identity);

        //obj[ran].GetComponent<RectTransform>().sizeDelta = new Vector2(razmer_objects, razmer_objects);
        obj[ran].transform.position = gameObject.transform.position;

        Invoke("Spawn", UnityEngine.Random.Range(spawnMin,spawnMax));
	}
}
