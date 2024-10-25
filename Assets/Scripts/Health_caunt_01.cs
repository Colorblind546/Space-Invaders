using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Health_caunt_01 : MonoBehaviour
{

    [SerializeField] GameObject bloodyExplosionEffect;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] screenShake_01 screenShake;

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
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        screenShake.StartCoroutine(screenShake.Shaking());
        Destroy(collision.gameObject);
        lives -= 1;

    }

    void Die()
    {
        Instantiate(bloodyExplosionEffect, transform.position, Quaternion.identity);
        Instantiate(bloodyExplosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Invoke(nameof(WaitTillLoad), 2);
    }

    
    

    /// <summary>
    /// Loads gameOver scene
    /// </summary>
    void WaitTillLoad()
    {
        SceneManager.LoadScene(2);
    }

}
