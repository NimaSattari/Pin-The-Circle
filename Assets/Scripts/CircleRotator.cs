using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float changeRotationTimer;
    [SerializeField] float ShakeSpeed;
    [SerializeField] float shakeAmount;

    public GameObject fruit;
    public SpriteRenderer face;

    Vector2 startingPos;
    float angle;
    float finalSpeed;
    float timeLeft;

    private void Awake()
    {
        timeLeft = Random.Range(changeRotationTimer - 2, changeRotationTimer + 2);
        startingPos = new Vector2(transform.position.x, transform.position.y);
        finalSpeed = rotateSpeed;
    }

    private void Update()
    {
        RotateCircle();

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            ChangeRotation();
            timeLeft = Random.Range(changeRotationTimer - 2, changeRotationTimer + 2);
        }
    }

    private void ChangeRotation()
    {
        int i = Random.Range(0, 2);

        if (i == 0)
        {
            finalSpeed = Random.Range(-rotateSpeed, -100);
        }
        else
        {
            finalSpeed = Random.Range(100, rotateSpeed);
        }
    }

    private void RotateCircle()
    {
        angle = transform.rotation.eulerAngles.z;
        angle += finalSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public IEnumerator Shake()
    {
        gameObject.transform.position = new Vector2(startingPos.x + (Mathf.Sin(Time.time * ShakeSpeed) * shakeAmount), startingPos.y + (Mathf.Sin(Time.time * ShakeSpeed) * shakeAmount));
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(startingPos.x, startingPos.y);
    }
}
