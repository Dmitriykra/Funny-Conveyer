using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject pauseGame;
    [SerializeField] GameObject victoryGame;
    [SerializeField] GameObject enviro;
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] GameObject conveyer;
    [SerializeField] GameObject gameScreen;
    bool isGamePaused = false;

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        nextLevelButton.SetActive(false);
    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            StartGame();
        }
        else
        {
            StartMenu();
        }
    }

    public void StartMenu()
    {
        gameScreen.SetActive(false);
        enviro.SetActive(false);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Time.timeScale = 0;
        startMenu.SetActive(true);
    }

    public void StartGame()
    {
        gameScreen.SetActive(true);
        enviro.SetActive(true);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Time.timeScale = 1;
        startMenu.SetActive(false);
    }

    public void HandleButtonClick()
    {
        if (isGamePaused)
        {
            ContinueGame();
            isGamePaused = false;
        }
        else
        {
            PauseGame();
            isGamePaused = true;
        }
    }

    public void Victory()
    {
        conveyer.SetActive(false);
        nextLevelButton.SetActive(true);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseGame.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseGame.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Restart", 1);
        StartGame();
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("Restart", 0);
        Application.Quit();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

   
}
