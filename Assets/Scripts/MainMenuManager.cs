using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject levelUI;
    [SerializeField] GameObject mainUI;

    int levelPassed;

    private void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPass", levelPassed);
    }

    public void LevelSelection()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        if (levelPassed == 0)
        {
            PlayLevel();
        }
        else
        {
            levelUI.SetActive(true);
            mainUI.SetActive(false);
        }
    }

    public void ReturnToMainUI()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        levelUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        Application.Quit();
    }

    public void PlayLevel()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISound);
        SceneManager.LoadScene(1);
    }
}
