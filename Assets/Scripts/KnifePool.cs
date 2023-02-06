using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    //prefab of the item you want to make
    public GameObject prefab;

    //the allowed amount of it in screen
    public int amount;

    //if you run out of objects and need more rightaway turn this to true
    public bool expandable;
}

public class KnifePool : MonoBehaviour
{
    public static KnifePool instance;

    //List of items you want to make
    public List<PoolItem> items;
    public List<GameObject> pooledItems;

    private void Awake()
    {
        instance = this;
    }

    public GameObject Get(string tag)
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
            {
                return pooledItems[i];
            }
        }
        foreach (var item in items)
        {
            if (item.prefab.tag == tag && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }
        return null;
    }

    public void MakeKnifes()
    {
        pooledItems = new List<GameObject>();
        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }
}