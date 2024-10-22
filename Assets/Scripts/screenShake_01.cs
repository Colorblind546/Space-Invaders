using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class screenShake_01 : MonoBehaviour
{
    // Start position for the camera, to reset without any issues of camera getting startposition changed
    Vector3 startPosition;

    // Not used anymore, a relic of the past
    protected bool start = true;

    // A curve to set how the screenshake is throughout the duration of the screenshake
    public AnimationCurve curve;

    // How long the screenshake lasts
    protected float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Sets start position for camera, so it always returns to the same place
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

    /// <summary>
    /// Shakes screen when called
    /// </summary>
    /// <returns></returns>
    public IEnumerator Shaking()
    {

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}