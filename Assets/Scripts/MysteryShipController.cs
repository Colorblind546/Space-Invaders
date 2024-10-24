using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MysteryShipController : MonoBehaviour
{

    // Difficulty based behaviour variables
    float hoverWaitTime;
    float smoothDampTime;
    float shipTargetSpeed;
    float rushDistance;
    float rushAttackSpeed;
    int missileBarrageAmount;
    float laserSpeed;


    // Projectiles
    [SerializeField] GameObject laserAimed;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject missile;

    // Player object
    GameObject playerObj;


    // Behaviour states
    enum InvaderStates
    {
        Rush, Hover, Evade
    };

    InvaderStates state;

    // Explosion effects
    [SerializeField] GameObject bloodyExplosionEffect;
    [SerializeField] GameObject explosionEffect;

    

    // Simulated fake velocity for smoothdamp to work, and position in last frame
    Vector2 fakeVelocity;
    Vector3 lastFramePos;

    // RigidBody2D
    Rigidbody2D rb2d;

    // Current running coroutine
    Coroutine currentCoroutine;

    // Target and target's target
    /// <summary>
    /// Target the ship follows
    /// </summary>
    public Vector3 targetPos;

    /// <summary>
    /// Target that the target the ship follows follows
    /// </summary>
    Vector3 targetsTargetPos;

    // Start is called before the first frame update
    void Start()
    {


        if (playerObj == null)
        {
            FindPlayer();
        }
        SetDifficultySettings();
        rb2d = GetComponent<Rigidbody2D>();
        lastFramePos = transform.position;
        state = InvaderStates.Rush;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            FindPlayer();
        }

        // Calculate velocity
        fakeVelocity = (transform.position - lastFramePos) / Time.deltaTime;
        lastFramePos = transform.position;


        switch (state)
        {
            case InvaderStates.Hover:
                {
                    if (currentCoroutine == null)
                    {
                        currentCoroutine = StartCoroutine(ShipHover());
                    }
                    

                    break;
                }
            case InvaderStates.Evade:
                {
                    if (currentCoroutine == null)
                    {
                        currentCoroutine = StartCoroutine(EvadeProjectiles());
                    }
                    break;
                }
            case InvaderStates.Rush:
                {
                    if (currentCoroutine == null)
                    {
                        print("Rush");
                        print(currentCoroutine);
                        currentCoroutine = StartCoroutine(RushPlayer());
                    }
                    break;
                }


        }




        // Moves target if far enough
        if (Vector2.Distance(targetPos, targetsTargetPos) > 0.2)
        {
            MoveTargetToTargetsTarget();
        }
        else
        {
            targetPos = targetsTargetPos;
        }
        
        // Move ship to target
        MoveShipToTarget();
        

        

    }

    IEnumerator RushPlayer()
    {
        int fury = 15;

        while (state == InvaderStates.Rush)
        {

            targetsTargetPos.x = playerObj.transform.position.x;
            targetsTargetPos.y = playerObj.transform.position.y + rushDistance;
            if (fury > 0)
            {

                if (PlayerPrefs.GetString("Difficulty") == "bullshit"|| PlayerPrefs.GetString("Difficulty") == "hard")
                {
                    AimingShoot();
                    if (PlayerPrefs.GetString("Difficulty") == "bullshit")
                    {
                        AimingShoot();
                    }
                }
                else
                {
                    Shoot();
                    
                }

                fury--;
                yield return new WaitForSeconds(rushAttackSpeed);
            }
            else
            {
                state = InvaderStates.Evade;
            }

        }

       
    }


    /// <summary>
    /// Attempts to dodge and weave projectiles
    /// </summary>
    /// <returns></returns>
    IEnumerator EvadeProjectiles()
    {

        while (state == InvaderStates.Evade)
        {

            if (Mathf.Abs(playerObj.transform.position.x - targetsTargetPos.x) < 2)
            {
                EvasiveManeuver();
                yield return new WaitForSeconds(2.5f);
            }

            yield return null;
        }

    }

    /// <summary>
    /// Shoots downwards
    /// </summary>
    void Shoot()
    {
        GameObject laserObj = Instantiate(laser, transform.position, Quaternion.identity);
        Projectile laserProjectile = laserObj.GetComponent<Projectile>();
        laserProjectile.speed = laserSpeed;
    }

    /// <summary>
    /// Shoots at the player
    /// </summary>
    void AimingShoot()
    {
        GameObject laserObj = Instantiate(laserAimed, transform.position, Quaternion.identity);
        Projectile laserProjectile = laserObj.GetComponent<Projectile>();
        laserProjectile.speed = laserSpeed;
        laserProjectile.projectileType = "aimed";
        if (playerObj != null)
        {
            laserProjectile.MoveAimedProjectile(Random.insideUnitCircle * 2.5f + (Vector2)playerObj.transform.position);
        }
        
        

    }

    /// <summary>
    /// Fires barrage of missiles
    /// </summary>
    /// <returns></returns>
    IEnumerator MissileBarrage()
    {
        for (int i = 0; i < missileBarrageAmount; i++)
        {

            if (playerObj != null)
            {
                GameObject firedMissile = Instantiate(missile, transform.position, Quaternion.identity);
                HomingMissile homingMissile = firedMissile.GetComponent<HomingMissile>();
                homingMissile.SetTarget(playerObj);
                yield return new WaitForSeconds(0.25f);
            }

        }
    }

    void EvasiveManeuver()
    {
        targetsTargetPos = new Vector3(Random.Range(-13, 13), targetsTargetPos.y, targetsTargetPos.z);
    }

    /// <summary>
    /// Moves target to targetsTarget at a linear speed
    /// </summary>
    void MoveTargetToTargetsTarget()
    {
        Vector3 targetMove = (targetsTargetPos - targetPos).normalized * shipTargetSpeed;
        targetPos += targetMove * Time.deltaTime;
    }

    /// <summary>
    /// Moves ship to target with SmoothDamp
    /// </summary>
    void MoveShipToTarget()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref fakeVelocity, smoothDampTime);
    }

    /// <summary>
    /// Makes ship hover, floating back and forth on screen
    /// </summary>
    /// <returns></returns>
    IEnumerator ShipHover()
    {
        

        while (state == InvaderStates.Hover)
        {
            targetsTargetPos = new Vector2(-13f, 12f);

            yield return new WaitForSeconds(5f);

            targetsTargetPos = new Vector2(13f, 12f);

            yield return new WaitForSeconds(5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        OnHit();
    }

    /// <summary>
    /// Performs all action it needs to perform when hit
    /// </summary>
    void OnHit()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        StartCoroutine(Shake(0.1f));
    }

    /// <summary>
    /// Shakes the ship
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator Shake(float duration)
    {
        Vector2 basePosition = transform.position;
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            transform.position = basePosition + Random.insideUnitCircle * 1f;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = basePosition;
        fakeVelocity = Vector2.zero;
    }

    void ShipDeath()
    {
        Instantiate(bloodyExplosionEffect, transform.position, Quaternion.identity);
    }

    void FindPlayer()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");

        }
    }

    void SetDifficultySettings()
    {

         hoverWaitTime = 3;
         smoothDampTime = 0.5f;
         shipTargetSpeed = 15;
         rushDistance = 10;
         rushAttackSpeed = 1;
         missileBarrageAmount = 3;
         laserSpeed = 5;



    }


}
