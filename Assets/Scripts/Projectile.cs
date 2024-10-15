using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Classes all projectiles need
    protected Rigidbody2D rb2D;
    public float speed;

    /// <summary>
    /// Tells projectile wheter to move it up or down, based on whether string reads "up" or "down", and sets velocity to that
    /// </summary>
    /// <param name="direction"></param>
    protected void MoveProjectile(string direction)
    {
        if (direction == "up")
        {
            rb2D.velocity = Vector3.up * speed;
        }
        else if (direction == "down")
        {
            rb2D.velocity = Vector3.down * speed;
        }
    }

}
