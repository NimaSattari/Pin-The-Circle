using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int Level;
    public int LevelPassed;
    public GameObject Lock;
    private void Awake()
    {
        LevelPassed = PlayerPrefs.GetInt("LevelPass", LevelPassed);
        if (Level > LevelPassed)
        {
            Lock.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
    }
}