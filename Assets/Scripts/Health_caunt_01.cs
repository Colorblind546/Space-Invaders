using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_caunt_01 : MonoBehaviour
{
    protected int lives = 3;
    protected string health;
    // Start is called before the first frame update
    void Start()
    {
        print("Lives "+lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            Die();
        }
        print("Lives " + lives);
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
