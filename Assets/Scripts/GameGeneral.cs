using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGeneral : MonoBehaviour
{
    #region Singleton
    [SerializeField] public static GameGeneral instance;
    private void Awake()
    {
        if (GameGeneral.instance == null)
        {
            GameGeneral.instance = this;
        }
        else
        {
            if (GameGeneral.instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    [SerializeField] GameManager gameManagerPrefab;

    public void SetGameManager(LevelAsset levelAsset)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            Destroy(GameManager.instance.gameObject);
            Destroy(UIManager.instance.gameObject);
        }
        GameManager gameManager = Instantiate(gameManagerPrefab);
        gameManager.levelAsset = levelAsset;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadYourAsyncScene(2, gameManager));
        }
        else
        {
            StartCoroutine(LoadYourAsyncScene(1, gameManager));
        }
        //StartCoroutine(LoadYourAsyncScene(1, gameManager));
    }

    IEnumerator LoadYourAsyncScene(int newSceneName, GameManager gameManager)
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(gameManager.gameObject, SceneManager.GetSceneByBuildIndex(newSceneName));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
        gameManager.StartLevel();
    }
}
