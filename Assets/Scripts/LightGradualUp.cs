using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;


public class LightGradualUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Light2D lightSource;
    public bool isHeatingUp = false;
    public float heatUpTime = 10.0f;
    public float maxHeat = 2f;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light2D>(); 
        }
        lightSource.intensity = 0f; // Start with the light off     
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeatingUp) StartCoroutine(HeatUpLight());
    }

    IEnumerator HeatUpLight()
    {
        float elapsedTime = 0f;
        float startIntensity = 0f;

        while (elapsedTime < heatUpTime)
        {
            lightSource.intensity = Mathf.Lerp(startIntensity, maxHeat, elapsedTime / heatUpTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for next frame
        }

        lightSource.intensity = maxHeat; 
    }
}

