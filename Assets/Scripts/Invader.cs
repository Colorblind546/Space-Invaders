using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // Spriterenderer and array of every frame of its animation
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[2];

    // Time between frames and current active frame
    public float timeBetweenFrames;
    int currentFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    /// Destroys itself
    /// </summary>
    void GotHit()
    {
        Destroy(gameObject);
    }
}
