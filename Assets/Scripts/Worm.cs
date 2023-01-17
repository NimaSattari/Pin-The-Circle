using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] GameObject Body, dead1, dead2, Shadow, Clothe;

    [SerializeField] GameObject[] hats;

    private void Start()
    {
        hats[Random.Range(0, hats.Length)].SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        Body.SetActive(false);
        dead1.SetActive(true);
        dead2.SetActive(true);
        Shadow.SetActive(false);
        if(Clothe != null)
        {
            Clothe.SetActive(false);
        }
        dead1.transform.parent = null;
        dead2.transform.parent = null;
        dead1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30), Random.Range(-30, 30)));
        dead2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30), Random.Range(-30, 30)));
        dead2.GetComponent<Rigidbody2D>().MoveRotation(Mathf.LerpAngle(dead2.GetComponent<Rigidbody2D>().rotation, (Random.Range(-180, 180)), 5 * Time.deltaTime));
        dead1.GetComponent<Rigidbody2D>().MoveRotation(Mathf.LerpAngle(dead1.GetComponent<Rigidbody2D>().rotation, (Random.Range(-180, 180)), 5 * Time.deltaTime));
    }
}
