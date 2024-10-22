using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{

    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        MoveProjectile("down");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
