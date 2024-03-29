using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAttack : GameManager
{
    #region Singleton
    public static GameManagerAttack _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    #endregion

    [Header("Set In Inspector")]
    [SerializeField] GameObject pinsParentGameObject;
    [SerializeField] GameObject fruitParentGameObject;
    [SerializeField] GameObject[] fruitPrefabs;
    [SerializeField] GameObject knifePrefab;
    [SerializeField] int fruitCollisionInitLifes = 3;

    [Header("Level Related")]
    [Range(100, 300)]
    public int rangeOfSpeedsBottom;
    [Range(100, 300)]
    public int rangeOfSpeedsTop;
    [Range(1, 10)]
    public int rangeOfFruitRotateTimerBottom;
    [Range(1, 10)]
    public int rangeOfFruitRotateTimerTop;
    [Range(1, 10)]
    public int rangeOfFruitFallSpeedBottom;
    [Range(1, 10)]
    public int rangeOfFruitFallSpeedTop;
    [Range(1, 10)]
    public int superAttackFruitCountBottom;
    [Range(1, 10)]
    public int superAttackFruitCountTop;
    [Range(1, 10)]
    public int superAttackFruitTop;
    [Range(1, 10)]
    public int superAttackFruitBottom;
    [Range(1, 10)]
    public int[] timeBetweenFruits = new int[2];

    List<GameObject> fruitsList = new List<GameObject>();
    int allMoney;
    int fruitsKilled;
    int superFruitCharge, superFruitChargeThisRound;
    int thisRoundScore;
    int lifes;
    bool canShootShake, shouldInstantiateFruit;

    public void Start()
    {
        if(SaveSystem.instance != null)
        {
            allMoney = SaveSystem.instance._money;
        }
        superFruitCharge = superAttackFruitCountTop;
        instance = this;
        shouldInstantiateFruit = true;
        lifes = fruitCollisionInitLifes;
        StartCoroutine(CreateFruit());
        StartCoroutine(LetShootShakeIn(0.25f));
        UIManager.instance.SetLifeText(lifes, Color.green);
        StartCoroutine(UIManager.instance.SetFruitText(0, Color.green));
        UIManager.instance.shooterButton.onClick.AddListener(() => ShootPin());
        UIManager.instance.StartGame();
    }

    private IEnumerator CreateFruit()
    {
        if (shouldInstantiateFruit)
        {
            InstatiateFruit();
            superFruitChargeThisRound++;
            if (superFruitChargeThisRound >= superFruitCharge)
            {
                for (int i = 0; i < Random.Range(superAttackFruitBottom, superAttackFruitTop); i++)
                {
                    yield return new WaitForSeconds(Random.Range(timeBetweenFruits[0] / Random.Range(2, 4), timeBetweenFruits[1] / Random.Range(2, 4)));
                    InstatiateFruit();
                }
                superFruitChargeThisRound = 0;
                superFruitCharge = Random.Range(superAttackFruitCountBottom, superAttackFruitCountTop);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(timeBetweenFruits[0], timeBetweenFruits[1]));
            }
            StartCoroutine(CreateFruit());
        }
    }

    private void InstatiateFruit()
    {
        Vector3 fruitInitPos = new Vector3(Random.Range(-1.5f, 1.5f), 6, 0);
        GameObject fruitInstant = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], fruitInitPos, Quaternion.identity, fruitParentGameObject.transform);
        fruitsList.Add(fruitInstant);

        fruitInstant.GetComponent<CircleRotator>().SetLevelValues(0, 1, rangeOfSpeedsBottom, rangeOfSpeedsTop,
                                                                    Random.Range(rangeOfFruitRotateTimerBottom, rangeOfFruitRotateTimerTop));

        fruitInstant.GetComponent<Rigidbody2D>().gravityScale = (Random.Range(rangeOfFruitFallSpeedBottom, rangeOfFruitFallSpeedTop));
    }

    private void CreatePin()
    {
        Instantiate(knifePrefab, pinsParentGameObject.transform.position, Quaternion.identity, pinsParentGameObject.transform);
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
        if (SaveSystem.instance._endlessModeFruitsKilledRecord < fruitsKilled)
        {
            SaveSystem.instance._endlessModeFruitsKilledRecord = fruitsKilled;
        }
        SaveSystem.instance.FileSaving();
    }

    public void ShootPin()
    {
        if (canShootShake)
        {
            CreatePin();
            StartCoroutine(LetShootShakeIn(0.25f));
        }
    }

    public override IEnumerator NextFruit()
    {
        yield return new WaitForSeconds(0.25f);
        fruitsKilled++;
        StartCoroutine(UIManager.instance.SetFruitText(fruitsKilled, Color.green));
    }

    private IEnumerator LetShootShakeIn(float timerr)
    {
        canShootShake = false;
        yield return new WaitForSeconds(timerr);
        canShootShake = true;
    }

    public void DecrementLife()
    {
        lifes--;
        UIManager.instance.SetLifeText(lifes, Color.red);
        if (lifes <= 0)
        {
            Lose();
        }
    }

    public override void Lose()
    {
        shouldInstantiateFruit = false;
        UIManager.instance.shooterButton.onClick.RemoveAllListeners();
        SaveLevel();
        UIManager.instance.LoadLosePanel();
        UIManager.instance.WinPanelActive(0, thisRoundScore, 0, false);
        AudioManager.instance.PlayOnShot(AudioManager.instance.loseSound);
    }

    public override void HandleKnifeToRock()
    {
        DecrementScore();
    }

    public override void HandleKnifeToKnife()
    {
        Lose();
    }

    public override void HandleKnifeToWorm()
    {
        IncrementScore();
    }

    public override void HandleKnifeToFruit()
    {
        IncrementScore();
    }
}
