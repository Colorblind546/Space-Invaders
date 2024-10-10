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
        if (Input.GetKey(KeyCode.R))
        {
          ResetButton_01();
        }
    }

    void ResetButton_01()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
