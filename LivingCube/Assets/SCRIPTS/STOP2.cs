using UnityEngine;
using System.Collections;
public class STOP2 : MonoBehaviour
{
    STOP2 sto;
    public string Level;
    void Start()
    {
        sto = GetComponent<STOP2>();
        Level=Application.loadedLevelName;//lvl 4
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Space))
            sto.enabled = false;
        if (Level == "Level_4")
            transform.position = new Vector3(-2.1f, -6.83f, 0);
        if (Level != "Level_4")
            transform.position = new Vector3(1.63f, -6.83f, 0);
    }
}
