using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    // Variables all projectiles need
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

    /// <summary>
    /// Makes projectiles fly towards a position in space, to be used in FixedUpdate
    /// </summary>
    /// <param name="targetPos"></param>
    protected void FlyTowards(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        if (rb2D != null)
        {
            rb2D.AddForce(transform.up * speed * Time.deltaTime);
        }
    }

}
