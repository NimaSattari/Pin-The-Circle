using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpriteChanger : MonoBehaviour
{
    [SerializeField] Sprite[] fruitSprites;
    [SerializeField] SpriteRenderer fruitSpriteRenderer;

    [SerializeField] Sprite[] faceSprites;
    [SerializeField] SpriteRenderer faceSpriteRenderer;

    void Start()
    {
        fruitSpriteRenderer.sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
        faceSpriteRenderer.sprite = faceSprites[Random.Range(0, faceSprites.Length)];
    }
}
