using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedLaser : Projectile
{
    // Start is called before the first frame update
    void Awake()
    {
        print("start from aimedlaser");
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
