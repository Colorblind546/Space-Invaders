using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Health_caunt_01 : MonoBehaviour
{
    protected int lives = 3;
    public TMP_Text LivesText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerHealth") && PlayerPrefs.GetInt("PlayerHealth") > 0)
        {
            lives = PlayerPrefs.GetInt("PlayerHealth");
        }
        else
        {
            lives = 5;
        }
        
        if (LivesText != null)
        {
            LivesText.text = "LIVES " + lives;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LivesText != null)
        {
            LivesText.text = "LIVES: " + lives;
        }

        if (lives <= 0)
        {
            Die();
        }
        
        LivesText.text = "LIVES: " + lives;

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        lives -= 1;

    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
