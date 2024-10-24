using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MissileCooldown"))
        {
            PlayerPrefs.SetFloat("MissileCooldown", 20);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Git gud or play this
    /// </summary>
    public void SetDifficultyEasy()
    {
        PlayerPrefs.SetString("Difficulty", "easy");
        PlayerPrefs.SetFloat("MissileCooldown", 10);
    }

    /// <summary>
    /// Boring and still pretty easy
    /// </summary>
    public void SetDifficultyNormal()
    {
        PlayerPrefs.SetString("Difficulty", "normal");
        PlayerPrefs.SetFloat("MissileCooldown", 20);
    }

    /// <summary>
    /// You got gud
    /// </summary>
    public void SetDifficultyHard()
    {
        PlayerPrefs.SetString("Difficulty", "hard");
        PlayerPrefs.SetFloat("MissileCooldown", 25);
    }

    /// <summary>
    /// "If anyone beats this, they are a god at space invaders" - Tsun Zu
    /// </summary>
    public void SetDifficultyBullshit()
    {
        PlayerPrefs.SetString("Difficulty", "bullshit");
        PlayerPrefs.SetFloat("MissileCooldown", 35);
    }
}
