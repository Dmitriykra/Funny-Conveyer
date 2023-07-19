using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class TaskForLevel : MonoBehaviour
{
    [SerializeField] Grab npc;
    [SerializeField] GameState gameState;
    [SerializeField] TextMeshProUGUI taskForLeveleTxt;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] List<string> foodNames = new List<string>();
    [SerializeField] int targetNumber;
    [SerializeField] Transform gameCamera;
    [SerializeField] float duration = 1f;
    Vector3 startCameraPos;
    Vector3 targetCameraPos = new Vector3(0.282999992f, 1.25399995f, -1.58599997f);
    Vector3 targetCameraRot = new Vector3(16.4781017f, 353.30658f, 0.0768934786f);
    [SerializeField] AnimationCurve curve;
    float elapsedTime;

    public static TaskForLevel instance;
    public string targetFoodName;
    int randomFoodInd;
    int scoreCount = 0;

    void Start()
    {
        instance = this;
        InitFoodList();
        InitScore();
        startCameraPos = gameCamera.position;
    }

    void InitFoodList()
    {
        foodNames.Add("Apple");
        foodNames.Add("Orange");
        foodNames.Add("Cherry");
        foodNames.Add("Vine");

        int maxTargetNumber = foodNames.Count + 2;
        targetNumber = Random.Range(1, maxTargetNumber);

        randomFoodInd = Random.Range(0, foodNames.Count);
        targetFoodName = foodNames[randomFoodInd];


        if (taskForLeveleTxt != null)
        {
            if (targetNumber == 1)
            {
                taskForLeveleTxt.text = "Collect " + targetNumber + " " + targetFoodName;
            }
            else
            {
                taskForLeveleTxt.text = "Collect " + targetNumber + " " + targetFoodName + "s";
            }
        }
    }

    void InitScore()
    {
        scoreText.text = "Score: " + scoreCount + "/" + targetNumber;
    }

    public void UpdateScore(int score)
    {
        scoreCount += score;
        InitScore();

        if (scoreCount < 0)
        {
            gameState.GameOver();
            taskForLeveleTxt.text = "Game Over";
        }

        if (scoreCount == targetNumber)
        {
            InitScore();
            StartCoroutine(VictoryCondition());


        }
    }

    IEnumerator VictoryCondition()
    {
        while (elapsedTime < duration)
        {
            float percenytCompleat = elapsedTime / duration;

            gameCamera.position = Vector3.Lerp(
                startCameraPos,
                targetCameraPos,
                curve.Evaluate(percenytCompleat));

            gameCamera.rotation = Quaternion.Lerp(
                Quaternion.Euler(startCameraPos),
                Quaternion.Euler(targetCameraRot),
                curve.Evaluate(percenytCompleat));

            elapsedTime += Time.deltaTime;

            yield return null;

            gameState.Victory();

            taskForLeveleTxt.text = "Level Passed!";

            npc.VictoryDance();
        } 
    }
}
