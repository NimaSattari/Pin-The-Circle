using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    int TwoBool;
    private void Awake()
    {
        TwoBool = PlayerPrefs.GetInt("Two", TwoBool);
        if(TwoBool == 1)
        {
            this.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void OK()
    {
        Time.timeScale = 1;
        TwoBool = 1;
        PlayerPrefs.SetInt("Two", TwoBool);
        this.gameObject.SetActive(false);
    }
}
