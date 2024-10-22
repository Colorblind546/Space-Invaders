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

    [SerializeField] GameObject enemylaser;

    private static GameManager _instance;

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


    // Kollar om GameManager redan finns n�r spelet startar, om det g�r det s� f�rst�rs det. Arnas s� g�r det s� att den inte f�rst�rs n�r den laddar. 

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

            RaycastHit2D hitcheck = Physics2D.Raycast(ob.transform.position - Vector3.up, -Vector2.up, 20f);
            print(hitcheck.collider != null);
            
            if(hitcheck.collider != null)
            {
                Instantiate(enemylaser, ob.transform.position, Quaternion.identity);
            }

            // Invaders �ker fr�n sida till sida

            float speed = 1f;
            ob.transform.position += speed * Time.deltaTime * direction;

            Vector3 rightwall = Camera.main.ViewportToWorldPoint(Vector3.right);
            Vector3 leftwall = Camera.main.ViewportToWorldPoint(Vector3.zero);

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

    public void Advance() // Invaders byter h�ll n�r de kommer n�ra v�ggen 
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }
}
