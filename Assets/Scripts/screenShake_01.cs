using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class screenShake_01 : MonoBehaviour
{
    
    protected bool start = true;
    public AnimationCurve curve;
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
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere*strength;
            Debug.Log("you are whiled and crazy");
            yield return null;
        }
        transform.position = startPosition;
        Debug.Log("you got too the end, Good job");
    }
    
}
