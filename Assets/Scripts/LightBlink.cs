using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlink : MonoBehaviour
{
    public Light2D terrainLight;
    public float blinkInterval = 2f; // Adjust as needed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (terrainLight == null)
            terrainLight = GetComponent<Light2D>();

        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            terrainLight.enabled = !terrainLight.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
