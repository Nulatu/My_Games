using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Change_control : MonoBehaviour
{

    public Dropdown button;
    void Start()
    {
        if (PlayerPrefs.GetString("Control")=="left")
            button.value = 0;
        if(PlayerPrefs.GetString("Control") == "right")
            button.value = 1;
    }
	public void setcontrol()
    {
        if (button.value==0)
        PlayerPrefs.SetString("Control", "left");
        else
        PlayerPrefs.SetString("Control", "right");
    }
}
