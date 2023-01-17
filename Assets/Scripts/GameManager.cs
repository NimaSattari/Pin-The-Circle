using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] private TextMeshProUGUI knifeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject pinsParentGameObject;
    [SerializeField] private GameObject pinPrefab;
    [SerializeField] private int howManyKnifesInGame;
    [SerializeField] private int level;
    [SerializeField] private List<GameObject> allPins = new List<GameObject>();
    [SerializeField] Sprite[] faces;
    [SerializeField] private Button shooterButton;
    [SerializeField] SpriteRenderer theFace;
    [SerializeField] List<GameObject> fruits;

    public int money;
    int remainedKnifes;
    int score;
    int pinIndex;
    int LevelPassed;
    bool isLose;

    private void Start()
    {
        remainedKnifes = howManyKnifesInGame;
        knifeText.text = remainedKnifes.ToString() + "/" + howManyKnifesInGame;
        levelText.text = "Level: " + level.ToString();
        money = PlayerPrefs.GetInt("Money", money);
        CreatePins();
    }

    public void SetRemainedKnifes()
    {
        remainedKnifes--;
        knifeText.text = remainedKnifes.ToString() + "/" + howManyKnifesInGame;
        pinIndex = howManyKnifesInGame - remainedKnifes;
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void DecrementScore()
    {
        score--;
        scoreText.text = score.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Money", money + score);
    }

    void CreatePins()
    {
        for (int i = 0; i < howManyKnifesInGame; i++)
        {
            GameObject pinInstant = Instantiate(pinPrefab, pinsParentGameObject.transform.position, Quaternion.identity, pinsParentGameObject.transform);
            allPins.Add(pinInstant);
        }
        shooterButton.onClick.AddListener(() => ShootPin());
    }

    IEnumerator ChangeFace(int face)
    {
        yield return new WaitForSeconds(0.1f);
        theFace.sprite = faces[face];
    }

    public void ShootPin()
    {
        SetRemainedKnifes();
        allPins[pinIndex - 1].GetComponent<Knife>().FirePin();

        if (howManyKnifesInGame == 10)
        {
            if ((remainedKnifes) <= 9f)
            {
                StartCoroutine(ChangeFace(1));
                if ((remainedKnifes) <= 8f)
                {
                    StartCoroutine(ChangeFace(2));
                    if ((remainedKnifes) <= 7f)
                    {
                        StartCoroutine(ChangeFace(3));
                        if ((remainedKnifes) <= 6f)
                        {
                            StartCoroutine(ChangeFace(4));
                            if ((remainedKnifes) <= 5f)
                            {
                                StartCoroutine(ChangeFace(5));
                                if ((remainedKnifes) <= 4f)
                                {
                                    StartCoroutine(ChangeFace(6));
                                    if ((remainedKnifes) <= 3f)
                                    {
                                        StartCoroutine(ChangeFace(7));
                                        if ((remainedKnifes) <= 2f)
                                        {
                                            StartCoroutine(ChangeFace(8));
                                            if ((remainedKnifes) <= 1f)
                                            {
                                                StartCoroutine(ChangeFace(9));
                                                if ((remainedKnifes) <= 0f)
                                                {
                                                    StartCoroutine(ChangeFace(10));
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
        if (howManyKnifesInGame == 7)
        {
            if ((remainedKnifes) <= 6f)
            {
                StartCoroutine(ChangeFace(2));
                if ((remainedKnifes) <= 5f)
                {
                    StartCoroutine(ChangeFace(4));
                    if ((remainedKnifes) <= 4f)
                    {
                        StartCoroutine(ChangeFace(6));
                        if ((remainedKnifes) <= 3f)
                        {
                            StartCoroutine(ChangeFace(7));
                            if ((remainedKnifes) <= 2f)
                            {
                                StartCoroutine(ChangeFace(8));
                                if ((remainedKnifes) <= 1f)
                                {
                                    StartCoroutine(ChangeFace(9));
                                    if ((remainedKnifes) <= 0f)
                                    {
                                        StartCoroutine(ChangeFace(10));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (howManyKnifesInGame == 5)
        {
            if ((remainedKnifes) <= 4f)
            {
                StartCoroutine(ChangeFace(2));
                if ((remainedKnifes) <= 3f)
                {
                    StartCoroutine(ChangeFace(4));
                    if ((remainedKnifes) <= 2f)
                    {
                        StartCoroutine(ChangeFace(6));
                        if ((remainedKnifes) <= 1f)
                        {
                            StartCoroutine(ChangeFace(8));
                            if ((remainedKnifes) <= 0f)
                            {
                                StartCoroutine(ChangeFace(10));
                            }
                        }
                    }
                }
            }
        }
        if (howManyKnifesInGame == 3)
        {
            if ((remainedKnifes) <= 2f)
            {
                StartCoroutine(ChangeFace(3));
                if ((remainedKnifes) <= 1f)
                {
                    StartCoroutine(ChangeFace(6));
                    if ((remainedKnifes) <= 0f)
                    {
                        StartCoroutine(ChangeFace(10));
                    }
                }
            }
        }

        if (pinIndex == howManyKnifesInGame)
        {
            StartCoroutine(WaitIfLoseInShootPin());
        }
    }

    public IEnumerator WaitIfLoseInShootPin()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isLose)
        {
            AudioManager.instance.PlayOnShot(AudioManager.instance.winSound);
            LevelPassed = PlayerPrefs.GetInt("LevelPass", LevelPassed);
            UIManager.instance.LoadWinPanel();
            if (level >= LevelPassed)
            {
                LevelPassed = level;
                PlayerPrefs.SetInt("LevelPass", LevelPassed);
                SaveScore();
            }
        }
    }

    public void Lose()
    {
        isLose = true;
        shooterButton.onClick.RemoveAllListeners();
        UIManager.instance.LoadLosePanel();
        AudioManager.instance.PlayOnShot(AudioManager.instance.loseSound);
    }
}
