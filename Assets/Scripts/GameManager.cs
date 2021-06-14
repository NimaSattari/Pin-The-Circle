using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Button Shoot;
    [SerializeField] GameObject Pin;
    GameObject[] Pins;
    float PinDistance = 3f;
    int PinIndex;
    [SerializeField] int HowManyPins;
    public bool GameOverBool = false;
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject Win;
    [SerializeField] Sprite[] Faces;
    [SerializeField] SpriteRenderer TheFace;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject back;
    [SerializeField] AudioClip WinSound;
    [SerializeField] int HowManyPinsForFaces;
    public static int LevelPassed;

    private void Awake()
    {
        Text LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        LevelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex - 1);
        if (instance == null)
        {
            instance = this;
        }
        GetButton();
    }
    private void Start()
    {
        CreatePin();
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                // Quit the application
                Menu.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1;
        }
        if (GameOverBool)
        {
            GameOver.SetActive(true);
        }
    }
    void GetButton()
    {
        Shoot = GameObject.Find("Shoot Button").GetComponent<Button>();
        Shoot.onClick.AddListener(() => ShootPin());
    }
    public void ShootPin()
    {
        Pins[PinIndex].GetComponent<PinMove>().FirePin();
        Pins[PinIndex].transform.parent = null;
        Pins[PinIndex].GetComponent<PinMove>().PinHead.GetComponent<BoxCollider2D>().enabled = true;
        Pins[PinIndex].GetComponent<PinMove>().CrossBow = false;
        transform.position += new Vector3(0f, 3f, 0f);
        PinIndex++;
        if(PinIndex == Pins.Length)
        {
            Shoot.onClick.RemoveAllListeners();
            StartCoroutine(LoadNext());
        }
        if (HowManyPinsForFaces == 10)
        {
            if ((Pins.Length - PinIndex) <= 9f)
            {
                StartCoroutine(changeFace(1));
                if ((Pins.Length - PinIndex) <= 8f)
                {
                    StartCoroutine(changeFace(2));
                    if ((Pins.Length - PinIndex) <= 7f)
                    {
                        StartCoroutine(changeFace(3));
                        if ((Pins.Length - PinIndex) <= 6f)
                        {
                            StartCoroutine(changeFace(4));
                            if ((Pins.Length - PinIndex) <= 5f)
                            {
                                StartCoroutine(changeFace(5));
                                if ((Pins.Length - PinIndex) <= 4f)
                                {
                                    StartCoroutine(changeFace(6));
                                    if ((Pins.Length - PinIndex) <= 3f)
                                    {
                                        StartCoroutine(changeFace(7));
                                        if ((Pins.Length - PinIndex) <= 2f)
                                        {
                                            StartCoroutine(changeFace(8));
                                            if ((Pins.Length - PinIndex) <= 1f)
                                            {
                                                StartCoroutine(changeFace(9));
                                                if ((Pins.Length - PinIndex) <= 0f)
                                                {
                                                    StartCoroutine(changeFace(10));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if(HowManyPinsForFaces == 7)
        {
            if ((Pins.Length - PinIndex) <= 6f)
            {
                StartCoroutine(changeFace(2));
                if ((Pins.Length - PinIndex) <= 5f)
                {
                    StartCoroutine(changeFace(4));
                    if ((Pins.Length - PinIndex) <= 4f)
                    {
                        StartCoroutine(changeFace(6));
                        if ((Pins.Length - PinIndex) <= 3f)
                        {
                            StartCoroutine(changeFace(7));
                            if ((Pins.Length - PinIndex) <= 2f)
                            {
                                StartCoroutine(changeFace(8));
                                if ((Pins.Length - PinIndex) <= 1f)
                                {
                                    StartCoroutine(changeFace(9));
                                    if ((Pins.Length - PinIndex) <= 0f)
                                    {
                                        StartCoroutine(changeFace(10));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if(HowManyPinsForFaces == 5)
        {
            if ((Pins.Length - PinIndex) <= 4f)
            {
                StartCoroutine(changeFace(2));
                if ((Pins.Length - PinIndex) <= 3f)
                {
                    StartCoroutine(changeFace(4));
                    if ((Pins.Length - PinIndex) <= 2f)
                    {
                        StartCoroutine(changeFace(6));
                        if ((Pins.Length - PinIndex) <= 1f)
                        {
                            StartCoroutine(changeFace(8));
                            if ((Pins.Length - PinIndex) <= 0f)
                            {
                                StartCoroutine(changeFace(10));
                            }
                        }
                    }
                }
            }
        }
        if(HowManyPinsForFaces == 3)
        {
            if ((Pins.Length - PinIndex) <= 2f)
            {
                StartCoroutine(changeFace(3));
                if ((Pins.Length - PinIndex) <= 1f)
                {
                    StartCoroutine(changeFace(6));
                    if ((Pins.Length - PinIndex) <= 0f)
                    {
                        StartCoroutine(changeFace(10));
                    }
                }
            }
        }
    }
    IEnumerator changeFace(int face)
    {
        yield return new WaitForSeconds(0.1f);
        TheFace.sprite = Faces[face];
    }
    void CreatePin()
    {
        Pins = new GameObject[HowManyPins];
        Vector3 temp = transform.position;
        for(int i = 0;i<Pins.Length; i++)
        {
            Pins[i] = Instantiate(Pin, temp, Quaternion.identity) as GameObject;
            Pins[i].transform.parent = gameObject.transform;
            temp.y -= PinDistance;
        }
    }
    public void InstantiatePin()
    {
        Instantiate(Pin, transform.position, Quaternion.identity);
    }
    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(0.5f);
        if (!GameOverBool)
        {
            LevelPassed = PlayerPrefs.GetInt("LevelPass", LevelPassed);
            if (SceneManager.GetActiveScene().buildIndex > LevelPassed)
            {
                LevelPassed = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("LevelPass", LevelPassed);
                Score.instance.IncrementScore();
            }
            GetComponent<AudioSource>().PlayOneShot(WinSound);
            yield return new WaitForSeconds(0.5f);
            Win.SetActive(true);
            back.SetActive(false);
        }
    }

    public void LoadThisByMoney()
    {
        if (Score.instance.publicMoney >= 2)
        {
            Score.instance.DecrementScore();
            Score.instance.DecrementScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void LoadThisByLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }
    public void BackMenu()
    {
        Time.timeScale = 0;
        Menu.SetActive(true);
    }
    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Reset()
    {
        Score.instance.BackTheScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Back()
    {
        Score.instance.BackTheScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ResetNext()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackNext()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
