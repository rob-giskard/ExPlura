using UnityEngine;
using UnityEngine.Audio;

public class EmitterSoundController : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip pickupSound;
    public AudioClip idleSound; // play only while visible

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EmitIdleSound()
    {

    }

    public void EmitPickupSound()
    {
        AudioSource source = audiosource.GetComponent<AudioSource>(); 
        source.PlayOneShot(pickupSound);
    }
}
