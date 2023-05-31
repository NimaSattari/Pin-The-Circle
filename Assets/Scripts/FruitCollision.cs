using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall") return;
        if (collision.tag == "PlayerWall")
        {
            GameManagerAttack._instance.DecrementLife();
            AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToKnifeSound);
        }
        GetComponentInParent<CircleRotator>().Knifed();
    }
}
