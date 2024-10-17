using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Invaders invaders;

    Vector3 direction = Vector3.right;
    Vector3 position;


    private static GameManager _instance;

    // Om GameManager inte finns så skriver den ett error i logs 
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


    // Kollar om GameManager redan finns när spelet startar, om det gör det så förstörs det. Arnas så gör det så att den inte förstörs när den laddar. 

    private void Awake()
    {
        position = transform.position;



        if (_instance)

            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Raycastcheck()
    {

    }

    // Update is called once per frame
    void Update()
    {

       


        foreach (GameObject ob in invaders.Invaderss) // Bestämmer vad varje invader gameobject ska göra 
        {
            // skuter en laser rakt ner som kollar om något är i vägen

            RaycastHit2D hitcheck = Physics2D.Raycast(ob.transform.position, -Vector2.up,20f); 
            
            if(hitcheck.collider != null)
            {
                Console.Write("Hit detected");
            }

            // Invaders åker från sida till sida

            float speed = 1f;
            ob.transform.position += speed * Time.deltaTime * direction;

            Vector3 rightwall = Camera.main.ViewportToWorldPoint(Vector3.right);
            Vector3 leftwall = Camera.main.ViewportToWorldPoint(Vector3.zero);

            if(!ob.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right &&  ob.transform.position.x >= rightwall.x -1f )
            {
                Advance();
                break;
            } 
            else if (direction == Vector3.left && ob.transform.position.x <= leftwall.x +1f)
            {
                Advance();
                break;
            }

           
        }
    }

    public void Advance() // Invaders byter håll när de kommer nära väggen 
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }
}
