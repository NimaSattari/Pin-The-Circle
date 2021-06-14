using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite[] Rocks;
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = Rocks[Random.Range(0, Rocks.Length)];
    }
}
