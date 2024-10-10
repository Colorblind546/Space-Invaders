using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class screenShake_01 : MonoBehaviour
{
    Vector3 startPosition;
    protected bool start = true;
    public AnimationCurve curve;
    protected float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Shaking());
        }
         
    }

    public IEnumerator Shaking()
    {
        
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere*strength;
            yield return null;
        }
        transform.position = startPosition;
    }
    
}
