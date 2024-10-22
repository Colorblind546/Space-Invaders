using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard_01 : MonoBehaviour
{

    public int score = 0;
    TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void UpdateScorce (int points)
    {
        print("recieved points: " + points);
        score += points;
        scoreText.text = "Score: " + score;
    }
}
