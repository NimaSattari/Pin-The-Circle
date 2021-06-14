using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    int OneBool;
    private void Awake()
    {
        OneBool = PlayerPrefs.GetInt("One", OneBool);
        if(OneBool == 1)
        {
            this.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void OK()
    {
        Time.timeScale = 1;
        OneBool = 1;
        PlayerPrefs.SetInt("One", OneBool);
        this.gameObject.SetActive(false);
    }
}
