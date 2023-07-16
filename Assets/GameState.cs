using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameState : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject pauseGame;
    Canvas canvas;
    int scoreCount;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

    }

    private void Start()
    {
        
        scoreCount = 0;
        scoreText.text = "Score: " + scoreCount;
        

        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            StartGame();
        }
        else
        {
            StartMenu();
            
        }
    }

    private void StartMenu()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Time.timeScale = 0;
        startMenu.SetActive(true);
    }

    public void StartGame()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Time.timeScale = 1;
        startMenu.SetActive(false);
    }

    public void PauseGame()
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

    public void IncreaseScore(int score)
    {
        scoreCount += score;
        scoreText.text = "Score: " + scoreCount;

        if(scoreCount < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
