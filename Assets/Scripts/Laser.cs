using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{

    
    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        if (projectileType == "")
        {
            MoveProjectile("down");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
