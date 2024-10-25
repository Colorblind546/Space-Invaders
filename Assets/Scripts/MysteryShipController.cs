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
    float attackSpeed;
    int missileBarrageAmount;
    float laserSpeed;
    float barrageCooldown;
    public float health;

    // Main Camera
    GameObject mainCamera;
    screenShake_01 screenShake;

    // Projectiles
    [SerializeField] GameObject laser;
    [SerializeField] GameObject missile;

    // Player object
    GameObject playerObj;


    // Behaviour states
    enum InvaderStates
    {
        Rush, Hover, Evade
    };

    [SerializeField] InvaderStates state;

    // Explosion effects
    [SerializeField] GameObject bloodyExplosionEffect;
    [SerializeField] GameObject explosionEffect;

    

    // Simulated fake velocity for smoothdamp to work, and position in last frame
    Vector2 fakeVelocity;
    Vector3 lastFramePos;

    // RigidBody2D
    Rigidbody2D rb2d;

    // Current running coroutines
    Coroutine currentStateCoroutine;
    Coroutine constantFire;
    Coroutine constantBarrage;

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
        // Setting start positions for targets
        targetsTargetPos = new Vector3(-13, 13, 0);
        targetPos = transform.position;

        mainCamera = Camera.main.gameObject;
        screenShake = mainCamera.GetComponent<screenShake_01>();

        if (playerObj == null)
        {
            FindPlayer();
        }
        SetDifficultySettings();
        rb2d = GetComponent<Rigidbody2D>();
        lastFramePos = transform.position;
        state = InvaderStates.Hover;
        
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

        if (constantFire == null)
        {
            constantFire = StartCoroutine(ConstantFire());
        }
        if (constantBarrage == null)
        {
            constantBarrage = StartCoroutine(BarrageRandom());
        }





        switch (state)
        {
            case InvaderStates.Hover:
                {
                    if (currentStateCoroutine == null)
                    {
                        currentStateCoroutine = StartCoroutine(ShipHover());
                    }
                    

                    break;
                }
            case InvaderStates.Evade:
                {
                    if (currentStateCoroutine == null)
                    {
                        currentStateCoroutine = StartCoroutine(EvadeProjectiles());
                    }
                    break;
                }
            case InvaderStates.Rush:
                {
                    if (currentStateCoroutine == null)
                    {
                        print("Rush");
                        print(currentStateCoroutine);
                        currentStateCoroutine = StartCoroutine(RushPlayer());
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
        

        if (health <= 0)
        {
            ShipDeath();
        }

    }


    IEnumerator ConstantFire()
    {

        while (true)
        {
            if (state != InvaderStates.Rush)
            {
                if (PlayerPrefs.GetString("Difficulty") == "bullshit" || PlayerPrefs.GetString("Difficulty") == "hard")
                {
                    AimingShoot();
                    if (PlayerPrefs.GetString("Difficulty") == "bullshit")
                    {
                        AimingShoot();
                    }
                }
                else if (PlayerPrefs.GetString("Difficulty") == "normal" || PlayerPrefs.GetString("Difficulty") == "easy")
                {
                    Shoot();

                }
            }
            


            yield return new WaitForSeconds(attackSpeed);
        }

    }

    IEnumerator BarrageRandom()
    {

        while (true)
        {
            if (state != InvaderStates.Rush)
            {
                yield return new WaitForSeconds(barrageCooldown);
                if (state != InvaderStates.Rush)
                {
                    StartCoroutine(MissileBarrage());
                }
                
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
            


        }

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
                else if (PlayerPrefs.GetString("Difficulty") == "normal"|| PlayerPrefs.GetString("Difficulty") == "easy")
                {
                    Shoot();
                    
                }

                fury--;
                yield return new WaitForSeconds(rushAttackSpeed);
            }
            else
            {
                if (Random.value > 0.5f)
                {
                    state = InvaderStates.Evade;
                }
                else
                {
                    state = InvaderStates.Hover;
                }
                
            }

        }
        currentStateCoroutine = null;
    }


    /// <summary>
    /// Attempts to dodge and weave projectiles
    /// </summary>
    /// <returns></returns>
    IEnumerator EvadeProjectiles()
    {
        int duration = 5;
        while (state == InvaderStates.Evade)
        {
            if (duration > 0)
            {
                if (Mathf.Abs(playerObj.transform.position.x - targetsTargetPos.x) < 2)
                {
                    duration--;
                    EvasiveManeuver();
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    duration--;
                    yield return new WaitForSeconds(1f);
                }
                
            }
            else
            {
                if (Random.value > 0.5f)
                {
                    state = InvaderStates.Rush;
                }
                else
                {
                    state = InvaderStates.Hover;
                }
            }
        }
        currentStateCoroutine = null;

    }

    /// <summary>
    /// Makes ship hover, floating back and forth on screen
    /// </summary>
    /// <returns></returns>
    IEnumerator ShipHover()
    {
        int duration = 5;

        while (state == InvaderStates.Hover)
        {
            if (duration > 0)
            {
                targetsTargetPos = new Vector2(-13f, 12f);
                duration--;
                yield return new WaitForSeconds(hoverWaitTime);


                targetsTargetPos = new Vector2(13f, 12f);
                duration--;
                yield return new WaitForSeconds(hoverWaitTime);
            }
            else
            {
                if (Random.value > 0.3f)
                {
                    state = InvaderStates.Rush;
                }
                else
                {
                    state = InvaderStates.Evade;
                }
            }
        }
        currentStateCoroutine = null;
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
        GameObject laserObj = Instantiate(laser, transform.position, Quaternion.identity);
        Projectile laserProjectile = laserObj.GetComponent<Projectile>();
        laserProjectile.speed = laserSpeed;
        laserProjectile.projectileType = "aimed";
        if (playerObj != null)
        {
            laserProjectile.MoveAimedProjectile(Random.insideUnitCircle * 3f + (Vector2)playerObj.transform.position);
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
        targetsTargetPos = new Vector3(Random.Range(-13, 13), 13, targetsTargetPos.z);
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
        health--;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        screenShake.StartCoroutine(screenShake.Shaking());
    }

    

    void ShipDeath()
    {
        Instantiate(bloodyExplosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
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


        hoverWaitTime = PlayerPrefs.GetFloat("HoverWait");
        smoothDampTime = PlayerPrefs.GetFloat("Acceleration");
        shipTargetSpeed = PlayerPrefs.GetFloat("ShipSpeed");
        rushDistance = PlayerPrefs.GetFloat("RushDistance");
        rushAttackSpeed = PlayerPrefs.GetFloat("RushAttackSpeed");
        attackSpeed = PlayerPrefs.GetFloat("AttackSpeed");
        missileBarrageAmount = PlayerPrefs.GetInt("BarrageAmount");
        laserSpeed = PlayerPrefs.GetFloat("LaserSpeed");
        barrageCooldown = PlayerPrefs.GetFloat("BossMissileBarrageCooldown");
        if (PlayerPrefs.HasKey("Health") && PlayerPrefs.GetInt("Health") > 0)
        {
            health = PlayerPrefs.GetInt("Health");
        }
        else
        {
            health = 30;
        }
        


    }


}
