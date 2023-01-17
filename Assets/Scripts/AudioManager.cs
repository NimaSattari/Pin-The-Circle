using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    [SerializeField] public static AudioManager instance;
    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }
        else
        {
            if (AudioManager.instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public AudioClip winSound, loseSound, knifeToFruitSound, knifeToKnifeSound, knifeToRockSound, knifeToWormSound, uISound;

    [SerializeField] AudioSource audioSource;

    public void PlayOnShot(AudioClip audioClip)
    {
        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
