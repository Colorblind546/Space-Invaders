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

    // Update is called once per frame
    void Update()
    {

        float speed = 1f;
        transform.position += speed * Time.deltaTime * direction;

        Vector3 rightwall = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 leftwall = Camera.main.ViewportToWorldPoint(Vector3.zero);

       
        foreach (GameObject ob in invaders.Invaderss)
        {
            
            
            if(direction == Vector3.right &&  ob.transform.position.x >= rightwall.x -1f )
            {
                Advance();
                break;
            }

           
        }
    }

    public void Advance()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }
}
