using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTime : GameManager
{
    [Header("Set In Inspector")]
    [SerializeField] GameObject pinsParentGameObject;
    [SerializeField] GameObject fruitParentGameObject;
    [SerializeField] GameObject pinPrefab;
    [SerializeField] GameObject[] fruitPrefabs;

    [Header("Level Related")]
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
    public float initialTime = 30f;
    public float timeToRmoveWhenKnifeCollideToKnife;
    public float timeToAddWhenKnifeToFruit;
    public float timeToAddWhenFruitKilled;

    List<GameObject> knifesList = new List<GameObject>();
    List<GameObject> fruitsList = new List<GameObject>();
    int[] numbers = { 3, 5, 7, 10 };
    int allMoney;
    int fruitsKilled;
    int thisRoundScore;
    int whichFruitNow;
    int howManyKnifesInGame;
    bool canShootFruitChange, canShootShake;
    float timer;

    public void Start()
    {
        allMoney = SaveSystem.instance._money;
        instance = this;
        CreateFruit();
        timer = initialTime;
    }

    private void Update()
    {
        if (canShootFruitChange && canShootShake)
        {
            timer -= Time.deltaTime;
            UIManager.instance.SetTimeText(timer);
            if (timer < 0)
            {
                timer = 0;
                UIManager.instance.SetTimeText(timer);
                canShootFruitChange = false;
                Lose();
            }
        }
    }

    private void CreateFruit()
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

        howManyKnifesInGame += howManyKnifesForThisFruit;

        CreatePins();
    }

    private void CreatePins()
    {
        for (int i = 0; i < howManyKnifesInGame; i++)
        {
            GameObject pinInstant = Instantiate(pinPrefab, pinsParentGameObject.transform.position, Quaternion.identity, pinsParentGameObject.transform);
            knifesList.Add(pinInstant);
        }
        StartCoroutine(LetShootFruitChangeIn(1f));
        StartCoroutine(LetShootShakeIn(0.25f));
        UIManager.instance.shooterButton.onClick.AddListener(() => ShootPin());
        UIManager.instance.StartGame();
    }

    public void DecrementRemainedKnifes()
    {
        knifesList.Remove(knifesList[0]);

    }

    public void IncrementRemainedKnifes()
    {
        GameObject pinInstant = Instantiate(pinPrefab, pinsParentGameObject.transform.position, Quaternion.identity, pinsParentGameObject.transform);
        knifesList.Add(pinInstant);
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

    public void SaveLevel()
    {
        SaveSystem.instance._money = (allMoney + thisRoundScore);
        if (SaveSystem.instance._timeTrailFruitKilledRecord < fruitsKilled)
        {
            SaveSystem.instance._timeTrailFruitKilledRecord = fruitsKilled;
        }
        SaveSystem.instance.FileSaving();
    }

    public void ShootPin()
    {
        if (canShootFruitChange && canShootShake)
        {
            knifesList[0].GetComponent<Knife>().FirePin();
            DecrementRemainedKnifes();
            StartCoroutine(LetShootShakeIn(0.25f));
        }
    }

    public override IEnumerator NextFruit()
    {
        StartCoroutine(LetShootFruitChangeIn(1f));
        yield return new WaitForSeconds(0.25f);
        fruitsList[whichFruitNow].SetActive(false);
        whichFruitNow++;
        CreateFruit();
        fruitsKilled++;
        timer += timeToAddWhenFruitKilled;
        UIManager.instance.SetTimeTextWithColor(timer, Color.green);
        StartCoroutine(UIManager.instance.SetFruitText(fruitsKilled, Color.green));
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

    public override void Lose()
    {
        UIManager.instance.shooterButton.onClick.RemoveAllListeners();
        SaveLevel();
        UIManager.instance.LoadLosePanel();
        UIManager.instance.WinPanelActive(0, thisRoundScore, 0, false);
        AudioManager.instance.PlayOnShot(AudioManager.instance.loseSound);
    }

    public override void HandleKnifeToRock()
    {
        DecrementScore();
        IncrementRemainedKnifes();
    }

    public override void HandleKnifeToKnife()
    {
        timer -= timeToRmoveWhenKnifeCollideToKnife;
        UIManager.instance.SetTimeTextWithColor(timer, Color.red);
    }

    public override void HandleKnifeToWorm()
    {
        IncrementScore();
    }

    public override void HandleKnifeToFruit()
    {
        IncrementScore();
        timer += timeToAddWhenKnifeToFruit;
        UIManager.instance.SetTimeTextWithColor(timer, Color.green);
    }
}
