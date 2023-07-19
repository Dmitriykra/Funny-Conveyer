using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameState gameState;

    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        StartCoroutine(RunGameOver());
    }

    IEnumerator RunGameOver()
    {
        yield return new WaitForSeconds(1f);
        gameState.GameOver();
    }
}