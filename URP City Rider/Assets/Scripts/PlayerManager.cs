using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool GameOver;
    public static int TotalCoins;
    public static int Coins;
    public static int Score;
    public static int HighScore;
    public GameObject GameOverPanel;
    public GameObject PauseBTN;
    public GameObject Player;
    public TextMeshProUGUI ScoreInGame;
    public TextMeshProUGUI ScoreAfterGame;
    public TextMeshProUGUI CoinsInGame;
    public TextMeshProUGUI CoinsAfterGame;
    public Animator AnimGameOver;
    void Start()
    {
        Application.targetFrameRate = 100;
        Coins = 0;
        GameOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameOver && CarHits.ObstF)
        {
            AnimGameOver.SetTrigger("GameOver");
            GameOverPanel.SetActive(true);
            PauseBTN.SetActive(false);
        }

        if (GameOver && CarHits.ObstR)
        {
            AnimGameOver.SetTrigger("GameOver");
            GameOverPanel.SetActive(true);
            PauseBTN.SetActive(false);
        }

        if (GameOver && CarHits.ObstL)
        {
            AnimGameOver.SetTrigger("GameOver");
            GameOverPanel.SetActive(true);
            PauseBTN.SetActive(false);
        }
        


        Score = (int)Player.transform.position.z;

        if (Score < 0)
            Score = 0;

        if (Score > HighScore)
            HighScore = Score;


        CoinsInGame.text = "- " + Coins;
        CoinsAfterGame.text = "COINS - " + Coins;
        ScoreInGame.text = "SCORE - " + Score;
        ScoreAfterGame.text = "SCORE - " + Score;
    }
    
}
