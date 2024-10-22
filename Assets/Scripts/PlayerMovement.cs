using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Player moveSpeed
    public float moveSpeed;
    float baseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves player at half speed, for more fine movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = baseSpeed / 2;
        }
        else
        {
            moveSpeed = baseSpeed;
        }

        // Moves left if 'A' is held, and right if 'D' is held
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -14.1875f, 14.1875f), transform.position.y, 0);
        

    }

    /// <summary>
    /// Moves player left
    /// </summary>
    void MoveLeft()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// Moves player right
    /// </summary>
    void MoveRight()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

    
}