using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard_01 : MonoBehaviour
{

    public int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
       scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateScorce (int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
