using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Invaders.cs script, not to be confused with invader.cs
    public Invaders invaders;

    // Invader LayerMask
    [SerializeField] LayerMask invaderLayer;

    // Invader move direction, and position
    Vector3 direction = Vector3.right;
    Vector3 position; // Never used

    // Invader laser Object
    [SerializeField] GameObject invaderLaser;

    // Current GameManager instance
    private static GameManager _instance;

    //cooldown for invaders shooting

    bool cooldown;

    // Om GameManager inte finns s� skriver den ett error i logs 
    public static GameManager Instance
    {
        get
        {

            if(_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }


    // Kollar om GameManager redan finns n�r spelet startar, om det g�r det s� f�rst�rs det. Annars s� g�r det s� att den inte f�rst�rs n�r den laddar. 

    private void Awake()
        
    {
        
        cooldown = false;
        position = transform.position; // This is never used, should we remove it?

        
        // Gör inte det här så att den säger att den ska förstöras, och sen stoppar sig från att förstöras, som leder till att inget händer? - Morgan
        if (_instance)

            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    { 
        // Sets direction to right 
        direction = Vector3.right;
    }

    void Raycastcheck()
    {

    }

    // Update is called once per frame
    void Update()
    {



        foreach (GameObject ob in invaders.Invaderss) // Best�mmer vad varje invader gameobject ska g�ra 
        {
            // skjuter en laser rakt ner som kollar om n�got �r i v�gen

            RaycastHit2D hitcheck = Physics2D.Raycast(ob.transform.position - Vector3.up, -Vector2.up, 10f, invaderLayer);


            if(hitcheck.collider == null && !cooldown)
            {

                   Instantiate(invaderLaser, ob.transform.position, Quaternion.identity);
                   Invoke("resetcooldown", 1f);
                   cooldown = true;
               


            }

            // Invaders �ker fr�n sida till sida

            float speed = 1f;
            ob.transform.position += speed * Time.deltaTime * direction;

            Vector3 rightwall = new Vector3(15, -15, -10);
            //Camera.main.ViewportToWorldPoint(Vector3.right);

            Vector3 leftwall = new Vector3(-15, -15, -10);
            //Camera.main.ViewportToWorldPoint(Vector3.zero);

            if (direction == Vector3.right && ob.transform.position.x >= rightwall.x - 1f)
            {
                print(rightwall + ", and " + leftwall);
                Advance();
                break;
            }
            else if (direction == Vector3.left && ob.transform.position.x <= leftwall.x + 1f)
            {
                Advance();
                break;
            }

        }


    }

    void resetcooldown()
    {
        cooldown = false;
    }

    /// <summary>
    /// Moves invaders down a step, apppoaching the player
    /// </summary>
    public void Advance() // Invaders byter h�ll n�r de kommer n�ra v�ggen 
    {
        foreach (GameObject invaderObj in invaders.Invaderss)
        {
            direction = new Vector3(-direction.x, 0, 0);
            Vector3 position = invaderObj.transform.position;
            position.y -= 1f;
            invaderObj.transform.position = position;
        }
   
    }
}
