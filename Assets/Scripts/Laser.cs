using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{





    string nothing = "";

    // Start is called before the first frame update

    void Start()
    {
        
        if (projectileType == nothing)
        {
            MoveProjectile("down");
        }

    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
