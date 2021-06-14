using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    Text scoreText;
    Text HighestScore;
    public static int highscore;
    int score;
    public int publicMoney;

    private void Awake()
    {
        HighestScore = GameObject.Find("HighestScore").GetComponent<Text>();
        highscore = PlayerPrefs.GetInt("Money", highscore);
        HighestScore.text = highscore.ToString();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        MakeInstance();
    }
    private void Update()
    {
        publicMoney = highscore;
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
        highscore++;
        HighestScore.text = "" + highscore;

        PlayerPrefs.SetInt("Money", highscore);
    }
    public void DecrementScore()
    {
        score--;
        scoreText.text = score.ToString();
        highscore--;
        HighestScore.text = "" + highscore;

        PlayerPrefs.SetInt("Money", highscore);
    }
    public int GetScore()
    {
        return score;
    }
    public void BackTheScore()
    {
        highscore -= score;
        HighestScore.text = "" + score;

        PlayerPrefs.SetInt("Money", highscore);
    }
}