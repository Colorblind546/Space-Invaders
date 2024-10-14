using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{

    // Columns and rows, columns can be set in inspector, rows is the same as the amount of objects in invaderPrefabs
    int rows;
    public int columns;

    // Space between aliens 
    public float spaceX, spaceY;

    // List of invader prefabs
    public List<GameObject> invaderPrefabs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        rows = invaderPrefabs.Count;
        SpawnInvaders();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Spawns invaders in a grid
    /// </summary>

    public List<GameObject> Invaderss = new List<GameObject>();

    void SpawnInvaders()
    {

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Instantiate(invaderPrefabs[i], transform.position + new Vector3(spaceX * j, -spaceY * i, 0), Quaternion.identity);
                //Invaderss.Add();
            }
        }
    }

   
}
