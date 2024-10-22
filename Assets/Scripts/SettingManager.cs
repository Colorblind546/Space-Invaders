using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("GameSpeed"))
        {
            PlayerPrefs.SetFloat("GameSpeed", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = PlayerPrefs.GetFloat("GameSpeed");
    }


    /// <summary>
    /// Git gud or play this
    /// </summary>
    public void SetDifficultyEasy()
    {
        PlayerPrefs.SetString("Difficulty", "easy");
        PlayerPrefs.SetFloat("GameSpeed", 0.8f);
    }

    /// <summary>
    /// Boring and still easy
    /// </summary>
    public void SetDifficultyNormal()
    {
        PlayerPrefs.SetString("Difficulty", "normal");
        PlayerPrefs.SetFloat("GameSpeed", 1f);
    }

    /// <summary>
    /// You got gud
    /// </summary>
    public void SetDifficultyHard()
    {
        PlayerPrefs.SetString("Difficulty", "hard");
        PlayerPrefs.SetFloat("GameSpeed", 1.5f);
    }

    /// <summary>
    /// "If anyone beats this, they are a god at space invaders" - Tsun Zu
    /// </summary>
    public void SetDifficultyBullshit()
    {
        PlayerPrefs.SetString("Difficulty", "bullshit");
        PlayerPrefs.SetFloat("GameSpeed", 5f);
    }
}
