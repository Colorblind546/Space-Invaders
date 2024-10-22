using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // Spriterenderer and array of every frame of its animation
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[2];

    // Explosion effect
    [SerializeField] GameObject explosionEffect;

    // Time between frames and current active frame
    public float timeBetweenFrames;
    int currentFrame;

    // Camera object
    GameObject cameraObj;
    screenShake_01 screenShake;

    // ScoreCounter GameObject and script
    public GameObject scoreCounter;
    ScoreBoard_01 scoreBoard;

    // Score yielded
    [SerializeField] int score;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraObj = Camera.main.gameObject;
        screenShake = cameraObj.GetComponent<screenShake_01>();
    }


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ChangeSpriteFrame), 0f, timeBetweenFrames);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Calls GotHit when it collides with sometheing that has the tag "missile", also destroys the object it collided with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GotHit();
        Destroy(collision.gameObject);
    }

    /// <summary>
    /// Changes current sprite of the invader to the next sprite in the sprites array, or loops back to 0 if at the last sprite
    /// </summary>
    void ChangeSpriteFrame()
    {
        if (currentFrame == sprites.Length)
        {
            currentFrame = 0;
        }
        spriteRenderer.sprite = sprites[currentFrame];
        currentFrame++;
    }

    /// <summary>
    /// Destroys itself, and creates an explosion in its wake 
    /// </summary>
    void GotHit()
    {
        screenShake.StartCoroutine(screenShake.Shaking());
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        if (scoreCounter != null)
        {
            print("board exists");
            scoreBoard = scoreCounter.GetComponent<ScoreBoard_01>();
            if (scoreBoard != null)
            {
                print("board is script and adding points");
                scoreBoard.UpdateScorce(score);
            }
        }
        else
        {
            print("scoreboard is null");
        }
        gameObject.SetActive(false);
    }


}
