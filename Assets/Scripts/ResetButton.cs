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
        //Denna if sats kallar p� en metod n�r man trykcer p� knappen R.
        if (Input.GetKey(KeyCode.R))
        {
          ResetButton_01();
        }
    }

    //Denna metod g�r s� att n�r man har tryckt p� knappen R s� reloadas scenen.
    void ResetButton_01()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
