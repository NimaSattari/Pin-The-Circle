using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Singleton
    public static MainMenuManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameObject mainUI1;
    [SerializeField] GameObject mainUI2;
    [SerializeField] GameObject classicLevelUI;
    [SerializeField] AllLevelsAsset allLevelsAsset;
    int levelPassed;
    int allMoney;

    private void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPass", levelPassed);
        allMoney = PlayerPrefs.GetInt("Money", allMoney);
        print(levelPassed);
        print(allMoney);
    }

    public void MenuUI2Load()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        if (levelPassed == 0)
        {
            PlayLevel(allLevelsAsset.allLevels[0]);
        }
        else
        {
            classicLevelUI.SetActive(false);
            mainUI2.SetActive(true);
            mainUI1.SetActive(false);
        }
    }

    public void MenuUI1Load()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        mainUI2.SetActive(false);
        classicLevelUI.SetActive(false);
        mainUI1.SetActive(true);
    }

    public void MenuLevelUILoad()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        mainUI2.SetActive(false);
        classicLevelUI.SetActive(true);
        mainUI1.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        Application.Quit();
    }

    public void PlayLevel(LevelAsset levelAsset)
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        GameGeneral.instance.SetGameManager(levelAsset);
    }
}
