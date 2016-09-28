using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverScore : MonoBehaviour {

    int score = 0;
    public Text score_;
	void Start ()
    {
        score = PlayerPrefs.GetInt("Score");
        score_.text = score.ToString();
	}
}
