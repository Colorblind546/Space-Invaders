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
            PlayerPrefs.SetFloat("MissileCooldown", 15);
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
        PlayerPrefs.SetInt("PlayerHealth", 10);

        // Enemy Settings
        PlayerPrefs.SetFloat("ShootChance", 0.25f);

        // Boss Settings
        PlayerPrefs.SetFloat("HoverWait", 7.5f);
        PlayerPrefs.SetFloat("Acceleration", 1f);
        PlayerPrefs.SetFloat("ShipSpeed", 5f);
        PlayerPrefs.SetFloat("RushDistance", 17.5f);
        PlayerPrefs.SetFloat("RushAttackSpeed", 2);
        PlayerPrefs.SetFloat("AttackSpeed", 7.5f);
        PlayerPrefs.SetInt("BarrageAmount", 1);
        PlayerPrefs.SetFloat("LaserSpeed", 5);
        PlayerPrefs.SetFloat("BossMissileBarrageCooldown", 30);
        PlayerPrefs.SetInt("Health", 15);
    }

    /// <summary>
    /// Boring and still pretty easy
    /// </summary>
    public void SetDifficultyNormal()
    {
        PlayerPrefs.SetString("Difficulty", "normal");
        PlayerPrefs.SetFloat("MissileCooldown", 20);
        PlayerPrefs.SetInt("PlayerHealth", 5);

        // Enemy Settings
        PlayerPrefs.SetFloat("ShootChance", 0.4f);

        // Boss Settings
        PlayerPrefs.SetFloat("HoverWait", 5f);
        PlayerPrefs.SetFloat("Acceleration", 0.6f);
        PlayerPrefs.SetFloat("ShipSpeed", 8.5f);
        PlayerPrefs.SetFloat("RushDistance", 15f);
        PlayerPrefs.SetFloat("RushAttackSpeed", 1.75f);
        PlayerPrefs.SetFloat("AttackSpeed", 4f);
        PlayerPrefs.SetInt("BarrageAmount", 3);
        PlayerPrefs.SetFloat("LaserSpeed", 7.5f);
        PlayerPrefs.SetFloat("BossMissileBarrageCooldown", 20);
        PlayerPrefs.SetInt("Health", 30);
    }

    /// <summary>
    /// You got gud
    /// </summary>
    public void SetDifficultyHard()
    {
        PlayerPrefs.SetString("Difficulty", "hard");
        PlayerPrefs.SetFloat("MissileCooldown", 25);
        PlayerPrefs.SetInt("PlayerHealth", 3);

        // Enemy Settings
        PlayerPrefs.SetFloat("ShootChance", 0.65f);

        // Boss Settings
        PlayerPrefs.SetFloat("HoverWait", 4f);
        PlayerPrefs.SetFloat("Acceleration", 0.5f);
        PlayerPrefs.SetFloat("ShipSpeed", 10f);
        PlayerPrefs.SetFloat("RushDistance", 12.5f);
        PlayerPrefs.SetFloat("RushAttackSpeed", 1);
        PlayerPrefs.SetFloat("AttackSpeed", 2.5f);
        PlayerPrefs.SetInt("BarrageAmount", 5);
        PlayerPrefs.SetFloat("LaserSpeed", 10);
        PlayerPrefs.SetFloat("BossMissileBarrageCooldown", 15);
        PlayerPrefs.SetInt("Health", 75);
    }

    /// <summary>
    /// "If anyone beats this, they are a god at space invaders" - Tsun Zu
    /// </summary>
    public void SetDifficultyBullshit()
    {
        PlayerPrefs.SetString("Difficulty", "bullshit");
        PlayerPrefs.SetFloat("MissileCooldown", 35);
        PlayerPrefs.SetInt("PlayerHealth", 1);

        // Enemy Settings
        PlayerPrefs.SetFloat("ShootChance", 0.9f);

        // Boss Settings
        PlayerPrefs.SetFloat("HoverWait", 3f);
        PlayerPrefs.SetFloat("Acceleration", 0.35f);
        PlayerPrefs.SetFloat("ShipSpeed", 5f);
        PlayerPrefs.SetFloat("RushDistance", 10f);
        PlayerPrefs.SetFloat("RushAttackSpeed", 0.65f);
        PlayerPrefs.SetFloat("AttackSpeed", 1.25f);
        PlayerPrefs.SetInt("BarrageAmount", 7);
        PlayerPrefs.SetFloat("LaserSpeed", 10);
        PlayerPrefs.SetFloat("BossMissileBarrageCooldown", 12.5f);
        PlayerPrefs.SetInt("Health", 150);
    }
}
