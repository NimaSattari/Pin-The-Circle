using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameObject loseUIPanel;
    [SerializeField] GameObject winUIPanel;
    [SerializeField] GameObject pauseUIPanel;
    [SerializeField] GameObject pauseButton;

    [SerializeField] TextMeshProUGUI knifeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI fruitText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] AllLevelsAsset allLevelsAsset;
    public Button shooterButton;

    public void LoadWinPanel()
    {
        winUIPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void LoadLosePanel()
    {
        loseUIPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Pause()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        Time.timeScale = 0;
        pauseUIPanel.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        pauseUIPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        Time.timeScale = 1;

        GameGeneral.instance.SetGameManager(allLevelsAsset.allLevels[GameManager.instance.levelAsset.level - 1]);
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        AudioManager.instance.PlayOnShot(AudioManager.instance.uISounds[Random.Range(0, AudioManager.instance.uISounds.Length)]);
        Time.timeScale = 1;

        GameGeneral.instance.SetGameManager(allLevelsAsset.allLevels[GameManager.instance.levelAsset.level]);
    }

    private IEnumerator ChangeColor(TextMeshProUGUI text, float timerr, Color firstColor, Color secondColor)
    {
        text.DOColor(firstColor, timerr);
        yield return new WaitForSeconds(timerr);
        text.DOColor(secondColor, timerr);
    }

    public void SetKnifeText(int remainedKnifes, int howManyKnifesInGame, Color color)
    {
        knifeText.text = remainedKnifes.ToString() + "/" + howManyKnifesInGame;
        StartCoroutine(ChangeColor(knifeText, 0.25f, color, Color.white));
    }

    public void SetScoreText(int thisRoundScore, Color color)
    {
        scoreText.text = thisRoundScore.ToString();
        StartCoroutine(scoreText.GetComponent<DoTweenActions>().OneLoop());
        StartCoroutine(ChangeColor(scoreText, 0.25f, color, Color.white));
    }

    public IEnumerator SetFruitText(int remainedFruits, Color color)
    {
        fruitText.gameObject.SetActive(true);
        fruitText.text = remainedFruits.ToString();
        fruitText.GetComponent<DoTweenActions>().DoAnimation();
        StartCoroutine(ChangeColor(fruitText, 0.25f, color, Color.white));
        yield return null;
        yield return new WaitForSeconds(1f);
        fruitText.gameObject.SetActive(false);
    }

    public void SetLevelText(string level)
    {
        levelText.text = "Level: " + level;
    }
}
