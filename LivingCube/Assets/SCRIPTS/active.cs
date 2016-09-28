using UnityEngine;
using System.Collections;

public class active : MonoBehaviour 
{
    public float activ;
    public bool activ_;
    public GameObject act;

	// Use this for initialization
	void Start () 
    {
        act=GameObject.FindGameObjectWithTag("K_Cube_Activ");
        act.GetComponent<active>();
        activ = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        activ = activ - Time.deltaTime;
        if (activ < 0)
        {
            activ_ = !activ_;
            act.SetActive(activ_);
            activ = 1;
        }
	}
}
