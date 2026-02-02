using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlink : MonoBehaviour
{
    public Light2D terrainLight;
    public float blinkInterval = 2f; 

    public bool useAsymmetricBlink = false;
    public float onDuration = 0.2f;   
    public float offDuration = 2.5f;

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
            if (useAsymmetricBlink)
            {
                yield return new WaitForSeconds(
                    terrainLight.enabled ? onDuration : offDuration
                );
            }
            else
            {
                yield return new WaitForSeconds(blinkInterval);
            }
        }
    }

    void Update()
    {
        
    }
}
