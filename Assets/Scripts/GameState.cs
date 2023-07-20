using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject pauseGame;
    [SerializeField] GameObject victoryGame;
    [SerializeField] GameObject enviro;
    [SerializeField] GameObject conveyer;
    [SerializeField] GameObject foodPoolObject;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseBtn;
    [SerializeField] SoundManager soundManager;

    bool isGamePaused = false;
    public float moveDuration = 2f; 
    public float targetHeight = 5f;

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        victoryGame.SetActive(false);

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
       
        pauseGame.SetActive(false);
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

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Restart", 0);
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
        pauseBtn.SetActive(false);
        soundManager.WinSound();
        victoryGame.SetActive(true);
        conveyer.SetActive(false);

        //все улетает вверх
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        Vector3 startPosition = foodPoolObject.transform.position;
        Vector3 targetPosition = foodPoolObject.transform.position + Vector3.up * targetHeight;

        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            foodPoolObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Убедимся, что объект точно достиг целевой позиции
        //foodPoolObject.transform.position = targetPosition;
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
        pauseGame.SetActive(false);
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
        soundManager.GameOverSound();
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

}
