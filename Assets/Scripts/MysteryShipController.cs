using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShipController : MonoBehaviour
{


    // Explosion effects
    [SerializeField] GameObject bloodyExplosionEffect;
    [SerializeField] GameObject explosionEffect;

    // Ship speed
    [SerializeField] float shipSpeed;

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
        rb2d = GetComponent<Rigidbody2D>();
        lastFramePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        fakeVelocity = (transform.position - lastFramePos) / Time.deltaTime;
        lastFramePos = transform.position;
        //targetsTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(targetPos, targetsTargetPos) > 0.2)
        {
            MoveTargetToTargetsTarget();
        }
        else
        {
            targetPos = targetsTargetPos;
        }
        
        MoveShipToTarget();
        
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(ShipHover());
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                StopAllCoroutines();
            }
        }

    }

    /// <summary>
    /// Moves target to targetsTarget at a linear speed
    /// </summary>
    void MoveTargetToTargetsTarget()
    {
        Vector3 targetMove = (targetsTargetPos - targetPos).normalized * shipSpeed;
        targetPos += targetMove * Time.deltaTime;
    }

    /// <summary>
    /// Moves ship to target with SmoothDamp
    /// </summary>
    void MoveShipToTarget()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref fakeVelocity, 0.25f);
    }

    IEnumerator ShipHover()
    {
        

        while (true)
        {
            targetsTargetPos = new Vector2(-14f, 14f);

            yield return new WaitForSeconds(5f);

            targetsTargetPos = new Vector2(14f, 14f);

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
            transform.position = basePosition + Random.insideUnitCircle * 0.5f;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    void ShipDeath()
    {
        Instantiate(bloodyExplosionEffect, transform.position, Quaternion.identity);
    }
}
