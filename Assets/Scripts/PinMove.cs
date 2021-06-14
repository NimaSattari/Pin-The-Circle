using UnityEngine;

public class PinMove : MonoBehaviour
{
    [SerializeField] GameObject PinLeg;
    bool CanShoot;
    bool touchedCircle;
    [SerializeField] float speed = 10f;
    Rigidbody2D Rigid;
    [SerializeField] ParticleSystem particleOrange;
    [SerializeField] ParticleSystem particleMelon;
    [SerializeField] ParticleSystem particleApple;
    [SerializeField] ParticleSystem particleCoco;
    [SerializeField] ParticleSystem particleLemon;

    [SerializeField] AudioClip clip;
    [SerializeField] public GameObject PinHead;
    [SerializeField] GameObject KnifeH1;
    [SerializeField] GameObject KnifeH2;
    [SerializeField] public bool CrossBow;
    Quaternion qStart, qEnd;
    [SerializeField] float angle;
    private float RotateSpeed = 2f;

    private void Awake()
    {
        Initialize();
        qStart = Quaternion.AngleAxis(angle, Vector3.forward);
        qEnd = Quaternion.AngleAxis(-angle, Vector3.forward);
    }
    private void Initialize()
    {
        PinLeg.SetActive(false);
        Rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
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
        PinLeg.SetActive(true);
        Rigid.isKinematic = true;
        CanShoot = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (touchedCircle)
        {
            return;
        }
        if (collision.tag == "Orange")
        {
            CanShoot = false;
            touchedCircle = true;
            Rigid.isKinematic = false;
            particleOrange.Play();
            this.GetComponent<AudioSource>().PlayOneShot(clip);
            Rigid.bodyType = RigidbodyType2D.Static;
            gameObject.transform.SetParent(collision.transform);
            collision.GetComponent<CircleRotate>().canShake = true;
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.SetScore();
            }
        }
        if (collision.tag == "Melon")
        {
            CanShoot = false;
            touchedCircle = true;
            Rigid.isKinematic = false;
            particleMelon.Play();
            this.GetComponent<AudioSource>().PlayOneShot(clip);
            Rigid.bodyType = RigidbodyType2D.Static;
            gameObject.transform.SetParent(collision.transform);
            collision.GetComponent<CircleRotate>().canShake = true;
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.SetScore();
            }
        }
        if (collision.tag == "Apple")
        {
            CanShoot = false;
            touchedCircle = true;
            Rigid.isKinematic = false;
            particleApple.Play();
            this.GetComponent<AudioSource>().PlayOneShot(clip);
            Rigid.bodyType = RigidbodyType2D.Static;
            gameObject.transform.SetParent(collision.transform);
            collision.GetComponent<CircleRotate>().canShake = true;
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.SetScore();
            }
        }
        if (collision.tag == "Coco")
        {
            CanShoot = false;
            touchedCircle = true;
            Rigid.isKinematic = false;
            particleCoco.Play();
            this.GetComponent<AudioSource>().PlayOneShot(clip);
            Rigid.bodyType = RigidbodyType2D.Static;
            gameObject.transform.SetParent(collision.transform);
            collision.GetComponent<CircleRotate>().canShake = true;
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.SetScore();
            }
        }
        if (collision.tag == "Lemon")
        {
            CanShoot = false;
            touchedCircle = true;
            Rigid.isKinematic = false;
            particleLemon.Play();
            this.GetComponent<AudioSource>().PlayOneShot(clip);
            Rigid.bodyType = RigidbodyType2D.Static;
            gameObject.transform.SetParent(collision.transform);
            collision.GetComponent<CircleRotate>().canShake = true;
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.SetScore();
            }
        }
        if (collision.tag == "Worm")
        {
            if (Score.instance != null)
            {
                Score.instance.IncrementScore();
            }
        }
        if(collision.tag == "Rock")
        {
            PinHead.SetActive(false);
            PinLeg.SetActive(false);
            KnifeH1.SetActive(true);
            KnifeH2.SetActive(true);
        }
    }
}
