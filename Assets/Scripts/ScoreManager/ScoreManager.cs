using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score { get; private set; }
    private int highScore;
    
    public Text scoreText;
    public Text highScoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("highScore");
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
        CheckHighestScore();
    }
    public void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString();
    }
    public void CheckHighestScore()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            highScoreText.text = "High Score :" + score;
        }
    }
}
