using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleRotator : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] Sprite[] faces;
    [SerializeField] GameObject[] objectsToInstant;
    [SerializeField] GameObject[] explodeParticle;
    [SerializeField] GameObject[] letterParticle;
    [SerializeField] List<GameObject> InstantiatePoints;
    [SerializeField] SpriteRenderer face;

    [Header("Level Related")]
    [Range(0, 7)]
    public int howManyObjectsToSpawn;
    public int needsThisManyKnifesToDie;
    public float lowRotateSpeed, highRotateSpeed;
    public float initChangeRotationTimer;
    public bool isAttackMode = false;

    float angle;
    float finalSpeed;
    int knifeEntered;
    float changeRotationTimer = 5;

    private void Update()
    {
        RotateCircle();

        changeRotationTimer -= Time.deltaTime;
        if (changeRotationTimer <= 0)
        {
            ChangeRotation();
        }
    }

    //Called By GameManager
    public void SetLevelValues(int HowManyObjectsToSpawn,int NeedsThisManyKnifesToDie, float LowRotateSpeed, float HighRotateSpeed, float InitChangeRotationTimer)
    {
        howManyObjectsToSpawn = HowManyObjectsToSpawn;
        needsThisManyKnifesToDie = NeedsThisManyKnifesToDie;
        lowRotateSpeed = LowRotateSpeed;
        highRotateSpeed = HighRotateSpeed;
        initChangeRotationTimer = InitChangeRotationTimer;
        Initialize();
    }

    private void Initialize()
    {
        ChangeRotation();
        InitializeSpawnedObjects();
    }

    private void InitializeSpawnedObjects()
    {
        for (int i = 0; i < howManyObjectsToSpawn; i++)
        {
            int whichPoint = Random.Range(0, InstantiatePoints.Count);
            int whichObjectToInstant = Random.Range(0, objectsToInstant.Length);

            Instantiate(objectsToInstant[whichObjectToInstant], InstantiatePoints[whichPoint].transform.position, InstantiatePoints[whichPoint].transform.rotation, InstantiatePoints[whichPoint].transform);
            InstantiatePoints.Remove(InstantiatePoints[whichPoint]);
        }
    }

    private void ChangeRotation()
    {
        int i = Random.Range(0, 2);

        if (i == 0)
        {
            finalSpeed = Random.Range(-highRotateSpeed, -lowRotateSpeed);
        }
        else
        {
            finalSpeed = Random.Range(lowRotateSpeed, highRotateSpeed);
        }
        changeRotationTimer = Random.Range(initChangeRotationTimer - 2, initChangeRotationTimer + 2);
    }

    private void RotateCircle()
    {
        angle = transform.rotation.eulerAngles.z;
        angle += finalSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public IEnumerator Shake()
    {
        transform.DOShakePosition(0.25f, 0.25f, 3);
        transform.DOShakeScale(0.25f, 0.25f, 3);
        yield return new WaitForSeconds(0.25f);
    }

    public void Knifed()
    {
        knifeEntered++;
        if (needsThisManyKnifesToDie == 10)
        {
            if ((needsThisManyKnifesToDie - knifeEntered) <= 9f)
            {
                ChangeFace(1);
                if ((needsThisManyKnifesToDie - knifeEntered) <= 8f)
                {
                    ChangeFace(2);
                    if ((needsThisManyKnifesToDie - knifeEntered) <= 7f)
                    {
                        ChangeFace(3);
                        if ((needsThisManyKnifesToDie - knifeEntered) <= 6f)
                        {
                            ChangeFace(4);
                            if ((needsThisManyKnifesToDie - knifeEntered) <= 5f)
                            {
                                ChangeFace(5);
                                if ((needsThisManyKnifesToDie - knifeEntered) <= 4f)
                                {
                                    ChangeFace(6);
                                    if ((needsThisManyKnifesToDie - knifeEntered) <= 3f)
                                    {
                                        ChangeFace(7);
                                        if ((needsThisManyKnifesToDie - knifeEntered) <= 2f)
                                        {
                                            ChangeFace(8);
                                            if ((needsThisManyKnifesToDie - knifeEntered) <= 1f)
                                            {
                                                ChangeFace(9);
                                                if ((needsThisManyKnifesToDie - knifeEntered) <= 0f)
                                                {
                                                    ChangeFace(10);
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
        if (needsThisManyKnifesToDie == 7)
        {
            if ((needsThisManyKnifesToDie - knifeEntered) <= 6f)
            {
                ChangeFace(2);
                if ((needsThisManyKnifesToDie - knifeEntered) <= 5f)
                {
                    ChangeFace(4);
                    if ((needsThisManyKnifesToDie - knifeEntered) <= 4f)
                    {
                        ChangeFace(6);
                        if ((needsThisManyKnifesToDie - knifeEntered) <= 3f)
                        {
                            ChangeFace(7);
                            if ((needsThisManyKnifesToDie - knifeEntered) <= 2f)
                            {
                                ChangeFace(8);
                                if ((needsThisManyKnifesToDie - knifeEntered) <= 1f)
                                {
                                    ChangeFace(9);
                                    if ((needsThisManyKnifesToDie - knifeEntered) <= 0f)
                                    {
                                        ChangeFace(10);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (needsThisManyKnifesToDie == 5)
        {
            if ((needsThisManyKnifesToDie - knifeEntered) <= 4f)
            {
                ChangeFace(2);
                if ((needsThisManyKnifesToDie - knifeEntered) <= 3f)
                {
                    ChangeFace(4);
                    if ((needsThisManyKnifesToDie - knifeEntered) <= 2f)
                    {
                        ChangeFace(6);
                        if ((needsThisManyKnifesToDie - knifeEntered) <= 1f)
                        {
                            ChangeFace(8);
                            if ((needsThisManyKnifesToDie - knifeEntered) <= 0f)
                            {
                                ChangeFace(10);
                            }
                        }
                    }
                }
            }
        }
        if (needsThisManyKnifesToDie == 3)
        {
            if ((needsThisManyKnifesToDie - knifeEntered) <= 2f)
            {
                ChangeFace(3);
                if ((needsThisManyKnifesToDie - knifeEntered) <= 1f)
                {
                    ChangeFace(6);
                    if ((needsThisManyKnifesToDie - knifeEntered) <= 0f)
                    {
                        ChangeFace(10);
                    }
                }
            }
        }
        if (knifeEntered == needsThisManyKnifesToDie)
        {
            ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        GameObject explodeParticleInstant = Instantiate(explodeParticle[Random.Range(0, explodeParticle.Length)], transform.position, Quaternion.identity);
        GameObject letterParticleInstant = Instantiate(letterParticle[Random.Range(0, letterParticle.Length)], transform.position, Quaternion.identity);
        Destroy(letterParticleInstant, 2);
        StartCoroutine(GameManager.instance.NextFruit());
        if (isAttackMode)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeFace(int faceid)
    {
        face.sprite = faces[faceid];
    }
}
