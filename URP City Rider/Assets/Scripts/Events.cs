using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class Events : MonoBehaviour
{
    public Animator AnimStart;
    public Animator AnimTapToStart;
    public Animator CarGameStart;
    public TextMeshProUGUI TotalCoinsText;
    public TextMeshProUGUI HighScoreText;
    public GameObject PausePanel;
    public GameObject PauseBTN;

    private void Start()
    {
        TotalCoinsText.text = "- " + PlayerManager.TotalCoins;
        HighScoreText.text = "HIGHSCORE - " + PlayerManager.HighScore;
    }
    public void ReplayGame()
    {
        PlayerManager.GameOver = false;
        SceneManager.LoadScene("Level01");
        SceneManager.UnloadScene("StartMenu");
        Time.timeScale = 1;

    }
    public void BackToMenu()
    {
        PlayerManager.TotalCoins += PlayerManager.Coins;
        SceneManager.LoadScene("StartMenu");
        SceneManager.UnloadScene("Level01");
        Time.timeScale = 1;
    }
    public void StartGame()
    {        
        PlayerManager.GameOver = false;
        AnimStart.SetTrigger("GameStarted");
        AnimTapToStart.SetTrigger("GameStarts");
        Invoke("AfterAnimation", 1f);
        Time.timeScale = 1;
    }
    private void AfterAnimation()
    {
        SceneManager.LoadScene("Level01");
        SceneManager.UnloadScene("StartMenu");
    }
    public void Pause()
    {
        AnimStart.SetTrigger("Pause");
        PausePanel.SetActive(true);
        PauseBTN.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        AnimStart.SetTrigger("Resume");
        Invoke("AfterPause", 0.5f);
        Time.timeScale = 1;
    }
    private void AfterPause()
    {
        PausePanel.SetActive(false);
        PauseBTN.SetActive(true);
    }
}
