using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int LevelPassed;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void LevelSelection()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        LevelPassed = PlayerPrefs.GetInt("LevelPass", LevelPassed);
        if (LevelPassed == 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(LevelPassed + 1);
        }
    }
    public void LoadThisLevel(int level)
    {
        SceneManager.LoadScene(level + 1);
    }
}
