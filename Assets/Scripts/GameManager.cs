using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    [Header("Set In Inspector")] 
    [SerializeField] GameObject pinsParentGameObject;
    [SerializeField] GameObject fruitParentGameObject;
    [SerializeField] GameObject pinPrefab;
    [SerializeField] GameObject[] fruitPrefabs;

    [Header("Level Related")]
    public LevelAsset levelAsset;
    [SerializeField] int level;
    [SerializeField] int howManyFruits;
    [Range(0, 7)]
    public int rangeOfObjectsOnTopOfFruitsBottom;
    [Range(0, 7)]
    public int rangeOfObjectsOnTopOfFruitsTop;
    [Range(100, 300)]
    public int rangeOfSpeedsBottom;
    [Range(100, 300)]
    public int rangeOfSpeedsTop;
    [Range(1, 10)]
    public int rangeOfFruitRotateTimerBottom;
    [Range(1, 10)]
    public int rangeOfFruitRotateTimerTop;

    List<GameObject> knifesList = new List<GameObject>();
    List<GameObject> fruitsList = new List<GameObject>();
    int[] numbers = { 3, 5, 7, 10 };
    int allMoney;
    int remainedKnifes;
    int remainedFruits;
    int thisRoundScore;
    int LevelPassed;
    int whichFruitNow;
    bool isLose;
    int howManyKnifesInGame;
    bool canShootFruitChange, canShootShake;

    public void StartLevel()
    {
        allMoney = PlayerPrefs.GetInt("Money", allMoney);
        instance = this;
        CreateFruits();
    }

    private void CreateFruits()
    {
        level = levelAsset.level;
        howManyFruits = levelAsset.howManyFruits;
        rangeOfObjectsOnTopOfFruitsBottom = levelAsset.rangeOfObjectsOnTopOfFruitsBottom;
        rangeOfObjectsOnTopOfFruitsTop = levelAsset.rangeOfObjectsOnTopOfFruitsTop;
        rangeOfSpeedsBottom = levelAsset.rangeOfSpeedsBottom;
        rangeOfSpeedsTop = levelAsset.rangeOfSpeedsTop;
        rangeOfFruitRotateTimerBottom = levelAsset.rangeOfFruitRotateTimerBottom;
        rangeOfFruitRotateTimerTop = levelAsset.rangeOfFruitRotateTimerTop;

        for (int i = 0; i < howManyFruits; i++)
        {
            int randomIndex = Random.Range(0, 4);
            int howManyKnifesForThisFruit = numbers[randomIndex];
            GameObject fruitInstant = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], fruitParentGameObject.transform.position, Quaternion.identity, fruitParentGameObject.transform);
            fruitsList.Add(fruitInstant);
            fruitInstant.GetComponent<CircleRotator>().SetLevelValues(Random.Range(rangeOfObjectsOnTopOfFruitsBottom, rangeOfObjectsOnTopOfFruitsTop),
                                                                        howManyKnifesForThisFruit,
                                                                        rangeOfSpeedsBottom,
                                                                        rangeOfSpeedsTop,
                                                                        Random.Range(rangeOfFruitRotateTimerBottom, rangeOfFruitRotateTimerTop));
            fruitInstant.SetActive(false);

            howManyKnifesInGame += howManyKnifesForThisFruit;
        }
        remainedFruits = howManyFruits;
        StartCoroutine(UIManager.instance.SetFruitText(remainedFruits, Color.green));
        fruitsList[0].SetActive(true);
        CreatePins();
    }

    private void CreatePins()
    {
        for (int i = 0; i < howManyKnifesInGame; i++)
        {
            GameObject pinInstant = Instantiate(pinPrefab, pinsParentGameObject.transform.position, Quaternion.identity, pinsParentGameObject.transform);
            knifesList.Add(pinInstant);
        }
        remainedKnifes = howManyKnifesInGame;
        StartCoroutine(LetShootFruitChangeIn(1f));
        StartCoroutine(LetShootShakeIn(0.25f));
        UIManager.instance.SetKnifeText(remainedKnifes, howManyKnifesInGame, Color.white);
        UIManager.instance.SetLevelText(level.ToString());
        UIManager.instance.shooterButton.onClick.AddListener(() => ShootPin());
    }

    public void DecrementRemainedKnifes()
    {
        knifesList.Remove(knifesList[0]);
        remainedKnifes--;

        UIManager.instance.SetKnifeText(remainedKnifes, howManyKnifesInGame, Color.green);

    }

    public void IncrementRemainedKnifes()
    {
        remainedKnifes++;
        GameObject pinInstant = Instantiate(pinPrefab, pinsParentGameObject.transform.position, Quaternion.identity, pinsParentGameObject.transform);
        knifesList.Add(pinInstant);

        UIManager.instance.SetKnifeText(remainedKnifes, howManyKnifesInGame, Color.red);
    }

    public void IncrementScore()
    {
        thisRoundScore++;
        UIManager.instance.SetScoreText(thisRoundScore, Color.green);
    }

    public void DecrementScore()
    {
        thisRoundScore--;
        UIManager.instance.SetScoreText(thisRoundScore, Color.green);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Money", allMoney + thisRoundScore);
    }

    public void ShootPin()
    {
        if (canShootFruitChange && canShootShake)
        {
            knifesList[0].GetComponent<Knife>().FirePin();
            DecrementRemainedKnifes();
            if (remainedKnifes == 0)
            {
                StartCoroutine(WaitIfLoseInShootPin());
            }
            StartCoroutine(LetShootShakeIn(0.25f));
        }
    }

    public IEnumerator NextFruit()
    {
        StartCoroutine(LetShootFruitChangeIn(1f));
        yield return new WaitForSeconds(0.25f);
        fruitsList[whichFruitNow].SetActive(false);
        whichFruitNow++;
        remainedFruits--;
        StartCoroutine(UIManager.instance.SetFruitText(remainedFruits, Color.green));
        if (whichFruitNow < howManyFruits)
        {
            fruitsList[whichFruitNow].SetActive(true);
        }
    }

    public IEnumerator WaitIfLoseInShootPin()
    {
        UIManager.instance.shooterButton.onClick.RemoveAllListeners();
        yield return new WaitForSeconds(0.5f);
        if (!isLose)
        {
            AudioManager.instance.PlayOnShot(AudioManager.instance.winSound);
            LevelPassed = PlayerPrefs.GetInt("LevelPass", LevelPassed);
            SaveScore();
            UIManager.instance.LoadWinPanel();
            if (level >= LevelPassed)
            {
                LevelPassed = level;
                PlayerPrefs.SetInt("LevelPass", LevelPassed);
            }
        }
    }

    private IEnumerator LetShootFruitChangeIn(float timerr)
    {
        canShootFruitChange = false;
        yield return new WaitForSeconds(timerr);
        canShootFruitChange = true;
    }

    private IEnumerator LetShootShakeIn(float timerr)
    {
        canShootShake = false;
        yield return new WaitForSeconds(timerr);
        canShootShake = true;
    }

    public void Lose()
    {
        isLose = true;
        UIManager.instance.LoadLosePanel();
        AudioManager.instance.PlayOnShot(AudioManager.instance.loseSound);
    }
}
