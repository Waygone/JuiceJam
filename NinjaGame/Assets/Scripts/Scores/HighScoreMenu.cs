using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText, highScoreText, timeText, totalScoreText;
    private float totalScore, highScore;


    private void Start()
    {
        timeText.text = "Time bonus: " + PlayerPrefs.GetInt("LevelTime", 0).ToString() + " x2";
        scoreText.text = "Score: " + PlayerPrefs.GetInt("LastScore", 0).ToString();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalScore = PlayerPrefs.GetInt("LevelTime", 0) * 2 + PlayerPrefs.GetInt("LastScore", 0);

        totalScoreText.text = "Total Score: " + totalScore.ToString();

        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", (int)totalScore);
        }
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
