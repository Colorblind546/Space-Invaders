using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // Invader LayerMask
    [SerializeField] LayerMask invaderLayer;

    // Bullet to be fired with the Shoot method
    [SerializeField] GameObject bullet;

    // Shooting cooldown
    float fireRateLimit = 1;
    float fireRateTimer;

    // Bullet speed, can be changed in inspector
    [SerializeField] float bulletSpeed;

    // The Missile object
    [SerializeField] GameObject missile;

    // Missile speed
    [SerializeField] float missileSpeed;

    // Missile cooldown
    float missileCooldown;
    public float cooldownTimeElapsed = 0f;

    // Missile fire hold time
    public float missileHold;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MissileCooldown"))
        {
            missileCooldown = PlayerPrefs.GetFloat("MissileCooldown");
        }
        else
        {
            print("No MissileCooldown exists");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (fireRateTimer < fireRateLimit)
        {
            fireRateTimer += Time.deltaTime;
        }
        // Calls Shoot when spacebar is pressed if there are no bullets onscreen
        if (Input.GetKeyDown(KeyCode.Space) && fireRateTimer >= fireRateLimit)
        {
            Shoot();
        }

        // When cooldown isn't finished, player cannot fire missiles
        if (cooldownTimeElapsed >= missileCooldown)
        {
            MissileHold();
        }

        // Counts down cooldown for missiles
        if (cooldownTimeElapsed < missileCooldown)
        {
            cooldownTimeElapsed += Time.deltaTime;
        }

    }


    /// <summary>
    /// Shoots a bullet when called
    /// </summary>
    void Shoot()
    {
        if (bullet != null)
        {
            fireRateTimer = 0;
            GameObject lastFiredBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Projectile projectileScript = lastFiredBullet.GetComponent<Projectile>();
            projectileScript.speed = bulletSpeed;
        }
        else
        {
            Debug.Log("Bullet was null");
        }
    }

    /// <summary>
    /// If called, player can hold key to fire missiles
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
        Collider2D[] invaderColliders = Physics2D.OverlapCircleAll(transform.position, 60, invaderLayer);
        foreach (Collider2D collider in invaderColliders)
        {
            print(collider.name);
        }

        for (int i = 0; i < Mathf.Clamp(3, 0, invaderColliders.Length); i++)
        {
           
            if (invaderColliders[i] != null)
            {
                GameObject firedMissile = Instantiate(missile, transform.position, Quaternion.identity);
                HomingMissile homingMissile = firedMissile.GetComponent<HomingMissile>();
                homingMissile.SetTarget(invaderColliders[i].gameObject);
                yield return new WaitForSeconds(0.1f);
            }
            
        }
    }


}