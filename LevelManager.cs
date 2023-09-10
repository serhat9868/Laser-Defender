using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delayAmount = 3f;
    ScoreKeeper scoreKeeper = null;
    Health health;

    private void Awake()
    {
        health = FindObjectOfType<Health>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
        scoreKeeper.ResetScore();

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
        if (scoreKeeper == null)
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }
        scoreKeeper.ResetScore();
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad("GameOverScene"));
    }
    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator WaitAndLoad(string sceneName)
    {
        yield return new WaitForSeconds(delayAmount);

        SceneManager.LoadScene(sceneName);
    }
}
