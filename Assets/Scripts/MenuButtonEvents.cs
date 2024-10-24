using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuButtonEvents : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }








    /*
     * #############################################################################################################################################################################################################
     * Any Methods beyond this point should only be used for events when player presses on menu buttons ############################################################################################################
     * #############################################################################################################################################################################################################
     */

    /// <summary>
    /// Starts Game
    /// </summary>
    public void StartGame()
    {
        print("Start");
        SceneManager.LoadScene(1);

    }

    public void RestartGame()
    {
        print("Restart");
        SceneManager.LoadScene(1);

    }

    public void MainMenu()
    {
        print("Main menu");
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Calls MoveToCredits
    /// </summary>
    public void OpenCredits()
    {
        print("Opening credits");
        StartCoroutine(MoveToCredits());
    }

    /// <summary>
    /// Calls MoveFromCredits
    /// </summary>
    public void ReturnFromCredits()
    {
        print("Returning from credits");
        StartCoroutine(MoveFromCredits());
    }

    /// <summary>
    /// Calls MoveToSettings
    /// </summary>
    public void OpenSettings()
    {
        print("Opening settings");
        StartCoroutine(MoveToSettings());
    }

    /// <summary>
    /// Calls MoveFromSettings
    /// </summary>
    public void ReturnFromSettings()
    {
        print("Returning from settings");
        StartCoroutine(MoveFromSettings());
    }

    /// <summary>
    /// Exits application (only works in builds)
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Moves to settings
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveToSettings()
    {
        while (Camera.main.gameObject.transform.position.y < 30)
        {
            Camera.main.gameObject.transform.position += new Vector3(0, 30, 0) * Time.deltaTime;
            yield return null;
        }
        Camera.main.gameObject.transform.position = new Vector3(0, 30, -10);
    }

    /// <summary>
    /// Moves from settings
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveFromSettings()
    {
        while (Camera.main.gameObject.transform.position.y > 0)
        {
            Camera.main.gameObject.transform.position += new Vector3(0, -30, 0) * Time.deltaTime;
            yield return null;
        }
        Camera.main.gameObject.transform.position = new Vector3(0, 0, -10);
    }

    /// <summary>
    /// Moves to credits
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveToCredits()
    {
        while (Camera.main.gameObject.transform.position.y > -30)
        {
            Camera.main.gameObject.transform.position += new Vector3(0, -30, 0) * Time.deltaTime;
            yield return null;
        }
        Camera.main.gameObject.transform.position = new Vector3(0, -30, -10);
    }
    
    /// <summary>
    /// Moves from credits
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveFromCredits()
    {
        while (Camera.main.gameObject.transform.position.y < 0)
        {
            Camera.main.gameObject.transform.position += new Vector3(0, 30, 0) * Time.deltaTime;
            yield return null;
        }
        Camera.main.gameObject.transform.position = new Vector3(0, 0, -10);
    }
}
