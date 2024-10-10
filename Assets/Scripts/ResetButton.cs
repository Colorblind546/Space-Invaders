using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Denna if sats kallar på en metod när man trykcer på knappen R.
        if (Input.GetKey(KeyCode.R))
        {
          ResetButton_01();
        }
    }

    //Denna metod gör så att när man har tryckt på knappen R så reloadas scenen.
    void ResetButton_01()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
