using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fon : MonoBehaviour
{
    public Sprite[] obj;
	void Start () 
    {
        if (SceneManager.GetActiveScene().name == "Menu"&& PlayerPrefs.GetString("zashli") =="no")
        {
            PlayerPrefs.SetInt("Fon", Random.Range(0, 3));
            PlayerPrefs.SetString("zashli", "yes");
        }
        int i= PlayerPrefs.GetInt("Fon");
        gameObject.GetComponent<Image>().sprite = obj[i];
	}

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("zashli", "no");
    }
}
