using System;
using TMPro;
using UnityEngine;

public class Movable : MonoBehaviour
{
    float horizontalMove;
    float sideBorder = 3f;
    float moveSpeed = 500f;
    public TextMeshProUGUI currentScore;
    int totalScoreNumber = 0;
    int score;
    public SoundManager _soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        totalScoreNumber += PlayerPrefs.GetInt("PlayerScore");
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            horizontalMove = Input.GetAxis("Mouse X");
            SideMove();
        }
        else
        {
            horizontalMove = 0f;
        }
    }

    private void SideMove()
    {
        float xAxisMove = transform.position.x + 
                          horizontalMove * moveSpeed * Time.deltaTime;

        //xAxisMove = Mathf.Clamp(xAxisMove, - sideBorder, sideBorder);    

        transform.position = new Vector3(xAxisMove, 
            transform.position.y, transform.position.z);

        //Debug.Log(GetScreenWidth());
    }


    public static float GetScreenWidth()
    {
        return Screen.width;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name.Contains("Apple"))
        {
            _soundManager.GetFoodAudio();
            Score();
            Destroy(other.gameObject);
        }
    }

    void Score()
    {
        score++;
        currentScore.text = ("Score " + score).ToString();
        totalScoreNumber+=score;
        TotalScore();
    }
    public int TotalScore()
    {
        PlayerPrefs.SetInt("PlayerScore", totalScoreNumber);
        return totalScoreNumber;
    }

    
}