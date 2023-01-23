using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
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

    public virtual IEnumerator NextFruit()
    {
        yield return null;
    }

    public virtual void HandleKnifeToRock()
    {

    }

    public virtual void HandleKnifeToKnife()
    {

    }

    public virtual void HandleKnifeToWorm()
    {

    }

    public virtual void HandleKnifeToFruit()
    {

    }
    public virtual void Lose()
    {

    }
}
