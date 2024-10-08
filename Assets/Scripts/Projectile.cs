using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rblaser;

    int speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rblaser = GetComponent<Rigidbody2D>();

      

        rblaser.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {

    }


}