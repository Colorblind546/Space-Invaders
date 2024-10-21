using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Projectile
{

    public Vector3 targetPosition;

    GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Invoke(nameof(DestroySelf), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Updates target position and moves the missile
        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }
        
        if (targetPosition != null)
        {
            FlyTowards(targetPosition);
        }
        
        
    }
    /// <summary>
    /// Sets missile target
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(GameObject target)
    {
        targetObject = target;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
