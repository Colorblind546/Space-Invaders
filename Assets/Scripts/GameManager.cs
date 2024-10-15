using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Invaders invaders;

    Vector2 position;


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
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach (GameObject ob in invaders.Invaderss)
        {
            ob.transform.position = new Vector2(-2, 0);
        }
    }

    private void FixedUpdate()
    {

    }
}
