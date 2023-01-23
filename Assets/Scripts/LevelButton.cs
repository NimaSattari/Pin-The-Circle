using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text levelText;
    public GameObject LockImage;
    public LevelAsset levelAsset;

    int LevelPassed;
    int scorePassed;

    private void Start()
    {
        LevelPassed = PlayerPrefs.GetInt("LevelPass", LevelPassed);
        scorePassed = PlayerPrefs.GetInt("scorePassed", scorePassed);
        levelText.text = levelAsset.level.ToString();

        if (levelAsset.level > LevelPassed + 1)
        {
            LockImage.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
        else
        {
            LockImage.SetActive(false);
            GetComponent<Button>().onClick.AddListener(() => MainMenuManager.instance.PlayLevel(levelAsset));
        }
    }
}