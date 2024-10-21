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

    public void StartGame()
    {
        print("Start");
        SceneManager.LoadScene(1);

    }

    public void OpenCredits()
    {
        print("Opening credits");
        StartCoroutine(MoveToCredits());
    }

    public void ReturnFromCredits()
    {
        print("Returning from credits");
        StartCoroutine(MoveFromCredits());
    }

    IEnumerator MoveToCredits()
    {
        while (Camera.main.gameObject.transform.position.y > -30)
        {
            Camera.main.gameObject.transform.position += new Vector3(0, -30, 0) * Time.deltaTime;
            yield return null;
        }
        Camera.main.gameObject.transform.position = new Vector3(0, -30, -10);
    }

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
