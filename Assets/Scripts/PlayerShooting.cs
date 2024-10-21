using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // Invader LayerMask
    [SerializeField] LayerMask invaderLayer;

    // Bullet to be fired with the Shoot method
    [SerializeField] GameObject bullet;

    // Last fired bullet to keep track of whether there is a bullet fired by the player that is still onscreen
    GameObject lastFiredBullet = null;

    // Bullet speed, can be changed in inspector
    [SerializeField] float bulletSpeed;

    // The Missile object
    [SerializeField] GameObject missile;

    // Missile speed
    [SerializeField] float missileSpeed;

    // Missile cooldown
    [SerializeField] float missileCooldown;
    public float cooldownTimeElapsed = 0f;

    // Missile fire hold time
    public float missileHold;

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

        if (cooldownTimeElapsed >= missileCooldown)
        {
            MissileHold();
        }

        if (cooldownTimeElapsed < missileCooldown)
        {
            cooldownTimeElapsed += Time.deltaTime;
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
            Projectile projectileScript = lastFiredBullet.GetComponent<Projectile>();
            projectileScript.speed = bulletSpeed;
        }
        else
        {
            Debug.Log("Bullet was null");
        }
    }

    /// <summary>
    /// If called, player must hold key to fire missiles
    /// </summary>
    void MissileHold()
    {
        if (Input.GetKey(KeyCode.E) && missileHold > 0)
        {
            missileHold -= Time.deltaTime;
        }
        else if (missileHold < 1)
        {
            missileHold = 1;
        }
        if (missileHold <= 0 && Input.GetKey(KeyCode.E))
        {
            cooldownTimeElapsed = 0;
            StartCoroutine(FireTheICBMBarrage());
        }
    }

    /// <summary>
    /// Fires an amount of missiles
    /// </summary>
    IEnumerator FireTheICBMBarrage()
    {
        Collider2D[] invaderColliders = Physics2D.OverlapCircleAll(transform.position, 45, invaderLayer);
        foreach (Collider2D collider in invaderColliders)
        {
            print(collider.name);
        }

        for (int i = 0; i < 3; i++)
        {
            if (i <= invaderColliders.Length)
            {
                if (invaderColliders[i] != null)
                {
                    GameObject firedMissile = Instantiate(missile, transform.position, Quaternion.identity);
                    HomingMissile homingMissile = firedMissile.GetComponent<HomingMissile>();
                    homingMissile.SetTarget(invaderColliders[i].gameObject);
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }
    }


}