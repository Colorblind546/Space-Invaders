using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // Bullet to be fired with the Shoot method
    [SerializeField] GameObject bullet;

    // Last fired bullet to keep track of whether there is a bullet fired by the player that is still onscreen
    GameObject lastFiredBullet = null;

    // Bullet speed, can be changed in inspector
    [SerializeField] float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calls Shoot when spacebar is pressed if there are no bullets onscreen
        if (Input.GetKeyDown(KeyCode.Space) && lastFiredBullet == null)
        {
            Shoot();
        }


    }


    /// <summary>
    /// Shoots a projectile when called
    /// </summary>
    void Shoot()
    {
        if (bullet != null)
        {
            lastFiredBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            //Projectile projectileScript = lastFiredBullet.GetComponent<Projectile>();
            //projectileScript.speed = bulletSpeed;
        }
        else
        {
            Debug.Log("Bullet was null");
        }
    }


}