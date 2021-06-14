using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHead : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource father;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pin Head")
        {
            this.GetComponent<AudioSource>().PlayOneShot(clip);
            GameObject.Find("GameManager").GetComponent<GameManager>().Shoot.onClick.RemoveAllListeners();
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOverBool = true;
            father.enabled = false;
        }
    }
}
