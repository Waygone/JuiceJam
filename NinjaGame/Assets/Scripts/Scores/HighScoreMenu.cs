using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText, highScoreText, timeText, totalScoreText, timeBonusText;
    private float totalScore, highScore;


    private void Start()
    {
        timeText.text = "Time: " + PlayerPrefs.GetFloat("LevelFinishTime", 0).ToString("0.00");
        timeBonusText.text = "Time Bonus: " + PlayerPrefs.GetInt("LevelTimeBonus", 0).ToString();
        scoreText.text = "Score: " + PlayerPrefs.GetInt("LastScore", 0).ToString();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalScore = PlayerPrefs.GetInt("LevelTimeBonus", 0) + PlayerPrefs.GetInt("LastScore", 0);

        totalScoreText.text = "Total Score: " + totalScore.ToString();

        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", (int)totalScore);
        }
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
