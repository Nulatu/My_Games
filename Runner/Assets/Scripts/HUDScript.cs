using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{
    /*
    private static HUDScript instance;
    public static HUDScript Instance
    {
        get { return instance ?? (instance = new HUDScript()); }
    }
    protected HUDScript() { }
    */
    public static float Speed = 200;
    float PlayerScore = 0;
    public Text playerscore;
    public Image hp;
    public float value_hp;
    public float polnoe_hp;
    public GameObject knopka_left;
    public GameObject knopka_right;
    public GameObject player;
    public GameObject image;
    public GameObject panel_dorog;
    public int mnogitel_speed=1;

    public SpawnScript raz;
    void Awake()
    {
        Speed = 200;//чтобы после рестарта уровня не была предыдущая скорость
        mnogitel_speed = 1;
        raz = GameObject.Find("0").GetComponent<SpawnScript>();
        //instance = this;
    }
    void Start()
    {
        player.GetComponent<RectTransform>().sizeDelta = new Vector2(raz.razmer_objects, raz.razmer_objects);

        player.transform.localPosition = new Vector3(-gameObject.GetComponent<Canvas>().pixelRect.width / 2 + player.GetComponent<RectTransform>().rect.width, GameObject.Find("2").transform.localPosition.y, 0);

        polnoe_hp = hp.GetComponent<RectTransform>().rect.width;
        value_hp = hp.GetComponent<RectTransform>().rect.width;
    }
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            PlayerScore += Time.deltaTime;

            if (Convert.ToInt32(PlayerScore / 10) >= mnogitel_speed)
            {
                Speed += 10;
                ++mnogitel_speed;
            }
            playerscore.text = Convert.ToInt32(PlayerScore).ToString();
            if (PlayerScore >= 1000&& PlayerPrefs.GetString("Game_new")=="")
                PlayerPrefs.SetString("Game_new","open");
        }
        else if (SceneManager.GetActiveScene().name == "Game_new")
        {
            PlayerScore += Time.deltaTime*2;

            if (Convert.ToInt32(PlayerScore / 10) >= mnogitel_speed)
            {
                Speed += 10;
                mnogitel_speed+=2;
            }
            playerscore.text = Convert.ToInt32(PlayerScore).ToString();
        }
    }

    public void IncreaseScore(int amount)
    {
        PlayerScore += amount;
    }
    public void Minus_HP(float minus_hp)
    {
        value_hp -= polnoe_hp * (minus_hp / 100);
        hp.GetComponent<RectTransform>().offsetMax = new Vector2(value_hp, 0);
        if (hp.GetComponent<RectTransform>().offsetMax.x <= 0)
            SceneManager.LoadScene("GameOver");
    }
    void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)PlayerScore);
    }
}
