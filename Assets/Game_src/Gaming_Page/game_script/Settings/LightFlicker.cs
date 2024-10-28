using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D;
    public float minIntensityNeg = 0f;
    public float maxIntensityNeg = 5f;
    public float minIntensityPos = 7f;
    public float maxIntensityPos = 9f;
    public float flickerDuration = 0.2f;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("Light2D component not found on this GameObject.");
            return;
        }

        StartCoroutine(FlickerCoroutine());
    }

    private IEnumerator FlickerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6f, 9f));
            StartCoroutine(Flicker());
        }
    }

    private IEnumerator Flicker()
    {
        float flickerEndTime = Time.time + Random.Range(0.3f, 0.8f); // duration for how long to flicker

        light2D.intensity = Random.Range(minIntensityPos, maxIntensityPos);
        yield return new WaitForSeconds(flickerDuration);

        while (Time.time < flickerEndTime)
        {
            light2D.intensity = Random.Range(minIntensityNeg, maxIntensityNeg);
            yield return new WaitForSeconds(flickerDuration);
            light2D.intensity = Random.Range(minIntensityPos, maxIntensityPos);
            yield return new WaitForSeconds(flickerDuration);
        }

        light2D.intensity = 3.53f;
    }
}
