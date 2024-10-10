using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class screenShake_01 : MonoBehaviour
{

    protected bool start = false;
    protected float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        } 
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere;
        }


        yield return null;

        transform.position = startPosition;
    }
    
}
