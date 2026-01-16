using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] Light2D flashlight;
    public bool turnedOnState = false;
    

        void Start()
    {     
        flashlight.enabled = turnedOnState; // Ensure the flashlight starts off
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle flashlight with "F" key
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
            Debug.Log("Toggled flashlight");
        }

    }

    public void ToggleFlashlight()
    {
        turnedOnState = !turnedOnState;
        flashlight.enabled = turnedOnState;
    }

    public void SetFlashlightState(bool state) // To force flashlight on/off in the future
    {
        turnedOnState = state;
        flashlight.enabled = state;
    }

    public bool ReportState()
    {
        return turnedOnState;
    }
}
