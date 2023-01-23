using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text levelText;
    public GameObject LockImage;
    public LevelAsset levelAsset;
    public GameObject[] starsImages;

    int LevelPassed;
    int scorePassed;

    private void Start()
    {
        LevelPassed = SaveSystem.instance._whichLevel;
        scorePassed = SaveSystem.instance._levelStars[levelAsset.level - 1];
        levelText.text = levelAsset.level.ToString();

        if (levelAsset.level > LevelPassed + 1)
        {
            LockImage.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
        else
        {
            for (int i = 0; i < scorePassed; i++)
            {
                starsImages[i].SetActive(true);
            }
            LockImage.SetActive(false);
            GetComponent<Button>().onClick.AddListener(() => MainMenuManager.instance.PlayLevel(levelAsset));
        }
    }
}