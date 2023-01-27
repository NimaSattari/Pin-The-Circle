using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Knife : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] GameObject PinRemain;
    [SerializeField] GameObject KnifeH1;
    [SerializeField] GameObject KnifeH2;
    [SerializeField] GameObject[] fruitParticles;
    [SerializeField] GameObject knifeToKnifeParticle;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] SpriteRenderer mySpriteRenderer;

    [Header("Level Related")]
    [SerializeField] float shootSpeed = 10f;
    [SerializeField] float rotateAngle = 15f;
    [SerializeField] float rotateSpeed = 2f;
    [SerializeField] bool isCrossBow;

    //private
    bool canShoot;
    bool isItInFruit;
    Quaternion qStart, qEnd;

    private void Awake()
    {
        qStart = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        qEnd = Quaternion.AngleAxis(-rotateAngle, Vector3.forward);
    }

    void FixedUpdate()
    {
        if (canShoot)
        {
            myRigidbody.velocity = (transform.up * shootSpeed);
        }
        if (isCrossBow)
        {
            qStart = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
            qEnd = Quaternion.AngleAxis(-rotateAngle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(qStart, qEnd, (Mathf.Sin(Time.time * rotateSpeed) + 1.0f) / 2.0f);
        }
    }

    public void FirePin()
    {
        isCrossBow = false;
        canShoot = true;
        transform.parent = null;
        myRigidbody.isKinematic = true;
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
            if (GameManager.instance != null)
            {
                GameManager.instance.HandleKnifeToRock();
            }
            SetParentToNull();
            Destroy(gameObject, 2f);
        }
        else if (collision.tag == "Pin Head")
        {
            Knife otherKnife = collision.GetComponentInParent<Knife>();
            if(otherKnife != null)
            {
                if (otherKnife.isItInFruit)
                {
                    AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToKnifeSound);
                    StartCoroutine(ChangeColor(0.5f, Color.red, Color.white));
                    StartCoroutine(otherKnife.ChangeColor(0.5f, Color.red, Color.white));
                    GameObject paricle = Instantiate(knifeToKnifeParticle, transform.position, Quaternion.identity, this.transform);
                    Destroy(paricle, 2f);
                    if (GameManager.instance != null)
                    {
                        GameManager.instance.HandleKnifeToKnife();
                    }
                }
            }
            else
            {
                AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToKnifeSound);
                StartCoroutine(ChangeColor(0.5f, Color.red, Color.white));
                GameObject paricle = Instantiate(knifeToKnifeParticle, transform.position, Quaternion.identity, this.transform);
                Destroy(paricle, 2f);
                if (GameManager.instance != null)
                {
                    GameManager.instance.HandleKnifeToKnife();
                }
            }
        }
        else if (collision.tag == "Worm")
        {
            AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToWormSound);
            if (GameManager.instance != null)
            {
                GameManager.instance.HandleKnifeToWorm();
            }
        }
        else if (collision.tag == "Fruit")
        {
            canShoot = false;
            myRigidbody.isKinematic = false;
            myRigidbody.bodyType = RigidbodyType2D.Static;
            isItInFruit = true;
            GameObject paricle = Instantiate(fruitParticles[Random.Range(0, fruitParticles.Length)], transform.position, Quaternion.identity, this.transform);
            Destroy(paricle, 2f);
            AudioManager.instance.PlayOnShot(AudioManager.instance.knifeToFruitSound);
            transform.SetParent(collision.transform);
            StartCoroutine(collision.GetComponentInParent<CircleRotator>().Shake());
            if (GameManager.instance != null)
            {
                GameManager.instance.HandleKnifeToFruit();
            }
        }
    }

    private IEnumerator SetParentToNull()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent = null;
    }

    private IEnumerator ChangeColor(float timerr, Color firstColor, Color secondColor)
    {
        mySpriteRenderer.DOColor(firstColor, timerr);
        yield return new WaitForSeconds(timerr);
        mySpriteRenderer.DOColor(secondColor, timerr);
    }
}
