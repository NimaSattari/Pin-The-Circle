using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    [SerializeField] float _RotateSpeed;
    [SerializeField] float RotateSpeed;
    [SerializeField] float FinalSpeed;
    [SerializeField] float timeR;
    [SerializeField] bool ChangeRotate;
    int Turn;
    [SerializeField] float ShakeSpeed;
    [SerializeField] float amount;
    [SerializeField] public bool canShake = false;

    bool CanRottate;
    float angle;
    Vector2 startingPos;

    private void Awake()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
        CanRottate = true;
        if (ChangeRotate)
        {
            StartCoroutine(ChangeRotation());
        }
    }

    void Update()
    {
        if (canShake)
        {

            gameObject.transform.position = new Vector2(startingPos.x + (Mathf.Sin(Time.time * ShakeSpeed) * amount), startingPos.y + (Mathf.Sin(Time.time * ShakeSpeed) * amount));
            StartCoroutine(Shake());
        }
        if (CanRottate)
        {
            RotateCircle();
        }
    }
    IEnumerator ChangeRotation()
    {
        int i = Random.Range(0, 2);
        if (i != Turn)
        {
            FinalSpeed = FinalSpeed / 1.25f;
            yield return new WaitForSeconds(0.175f);
            FinalSpeed = FinalSpeed / 1.25f;
            yield return new WaitForSeconds(0.175f);
            FinalSpeed = FinalSpeed / 1.25f;
            yield return new WaitForSeconds(0.175f);
            FinalSpeed = FinalSpeed / 1.25f;
            yield return new WaitForSeconds(0.175f);
        }
        Turn = i;
        if (i == 0)
        {
            FinalSpeed = Random.Range(_RotateSpeed, -100);
        }
        else
        {
            FinalSpeed = Random.Range(100, RotateSpeed);
        }
        yield return new WaitForSeconds(Random.Range(timeR - 2, timeR + 2));
        StartCoroutine(ChangeRotation());
    }
    void RotateCircle()
    {
        angle = transform.rotation.eulerAngles.z;
        angle += FinalSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(0.1f);
        canShake = false;
        transform.position = new Vector2(startingPos.x, startingPos.y);
    }
}
