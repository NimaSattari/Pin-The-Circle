using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameObject loseUIPanel;
    [SerializeField] GameObject winUIPanel;
    [SerializeField] GameObject pauseUIPanel;
    [SerializeField] GameObject pauseButton;

    public void LoadWinPanel()
    {
        winUIPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void LoadLosePanel()
    {
        loseUIPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Pause()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        Time.timeScale = 0;
        pauseUIPanel.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        pauseUIPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
