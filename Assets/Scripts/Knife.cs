using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knife : MonoBehaviour
{
    [SerializeField] GameObject PinRemain;
    [SerializeField] GameObject KnifeH1;
    [SerializeField] GameObject KnifeH2;
    [SerializeField] GameObject[] particles;
    [SerializeField] float speed = 10f;
    [SerializeField] float angle;
    [SerializeField] bool CrossBow;

    bool CanShoot;
    bool isItInFruit;
    float RotateSpeed = 2f;
    Quaternion qStart, qEnd;
    Rigidbody2D Rigid;

    private void Awake()
    {
        Initialize();
        qStart = Quaternion.AngleAxis(angle, Vector3.forward);
        qEnd = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    private void Initialize()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (CanShoot)
        {
            Rigid.velocity = (transform.up * speed);
        }
        if (CrossBow)
        {
            angle -= Time.deltaTime;
            if (angle < 0)
            {
                angle = 0;
            }
            qStart = Quaternion.AngleAxis(angle, Vector3.forward);
            qEnd = Quaternion.AngleAxis(-angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(qStart, qEnd, (Mathf.Sin(Time.time * RotateSpeed) + 1.0f) / 2.0f);
        }
    }

    public void FirePin()
    {
        CrossBow = false;
        CanShoot = true;
        transform.parent = null;
        Rigid.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rock")
        {
            PinRemain.SetActive(false);
            KnifeH1.SetActive(true);
            KnifeH2.SetActive(true);
            AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToRockSound);
            KnifeH1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-40, 40), Random.Range(-40, 40)));
            KnifeH2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-40, 40), Random.Range(-40, 40)));
            KnifeH1.GetComponent<Rigidbody2D>().MoveRotation(Mathf.LerpAngle(KnifeH1.GetComponent<Rigidbody2D>().rotation, (Random.Range(-180, 180)), 5 * Time.deltaTime));
            KnifeH2.GetComponent<Rigidbody2D>().MoveRotation(Mathf.LerpAngle(KnifeH2.GetComponent<Rigidbody2D>().rotation, (Random.Range(-180, 180)), 5 * Time.deltaTime));
            GameManager.instance.DecrementScore();
            GameManager.instance.IncrementRemainedKnifes();
            SetParentToNull();
            Destroy(gameObject, 2f);
        }
        else if (collision.tag == "Pin Head")
        {
            if (collision.GetComponentInParent<Knife>().isItInFruit)
            {
                AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToKnifeSound);
                if (GameManager.instance != null)
                {
                    GameManager.instance.Lose();
                }
            }
        }
        else if (collision.tag == "Worm")
        {
            AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToWormSound);
            if (GameManager.instance != null)
            {
                GameManager.instance.IncrementScore();
            }
        }
        else if (collision.tag == "Fruit")
        {
            CanShoot = false;
            Rigid.isKinematic = false;
            Rigid.bodyType = RigidbodyType2D.Static;
            isItInFruit = true;
            GameObject paricle = Instantiate(particles[Random.Range(0, particles.Length)], transform.position, Quaternion.identity);
            Destroy(paricle, 2f);
            AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToFruitSound);
            transform.SetParent(collision.transform);
            StartCoroutine(collision.GetComponentInParent<CircleRotator>().Shake());
            if (GameManager.instance != null)
            {
                GameManager.instance.IncrementScore();
            }
        }
    }

    private IEnumerator SetParentToNull()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent = null;
    }
}
