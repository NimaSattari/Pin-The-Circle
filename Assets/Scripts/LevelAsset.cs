using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelAsset : ScriptableObject
{
    public int level;
    public int howManyFruits;
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
}
