using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    public Light2D globalLight;
    private bool isLightOn = true;

    private void Start()
    {
        SetLightState(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle light with "L" key
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLight();
        }
    }

    public void ToggleLight()
    {
        isLightOn = !isLightOn;
        globalLight.intensity = isLightOn ? 1f : 0.2f; // Adjust intensity as needed
    }

    public void SetLightState(bool state)
    {
        isLightOn = state;
        globalLight.intensity = state ? 1f : 0.007f;
    }
}
