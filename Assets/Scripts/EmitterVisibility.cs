using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EmitterVisibility : MonoBehaviour
{
    public Light2D globalLight;  
    public Light2D flashlight;   
    public float flashlightRange = 5f; 

    private Renderer collectibleRenderer;
    private ShadowCaster2D shadowCaster;
    private Collider2D collectibleCollider; // do we need this anymore?? 
    private Material collectibleMaterial;
    public GameObject emitterPickupSound;
    
   

    private bool isVisible = false;

    void Start()
    {
        collectibleRenderer = GetComponent<Renderer>();
        shadowCaster = GetComponent<ShadowCaster2D>();
        collectibleCollider = GetComponent<Collider2D>();
        collectibleMaterial = collectibleRenderer.material;

        SetVisibility(false); 
    }

    void Update()
    {
        bool isGlobalLightOff = globalLight.intensity == 0f;
        bool isFlashlightOn = flashlight.enabled;
        bool isFlashlightInRange = Vector3.Distance(flashlight.transform.position, transform.position) <= flashlightRange;
        bool isWithinFlashlightCone = IsWithinFlashlightCone();
        bool isNotBlockedByWall = !IsBlockedByWall();

        isVisible = /* isGlobalLightOff && */ isFlashlightOn && isFlashlightInRange && isWithinFlashlightCone && isNotBlockedByWall;
        SetVisibility(isVisible);

        if (isVisible)
        {
            {
                float distanceToFlashlight = Vector3.Distance(flashlight.transform.position, transform.position);

                // Calculate fade factor (1 = fully visible, 0 = invisible) ...Experiment with this to resolve issues with cone edge TODO
                float fadeFactor = Mathf.Clamp01(1.0f - (distanceToFlashlight / flashlight.pointLightOuterRadius));

                collectibleMaterial.SetVector("_FlashlightPosition", new Vector4(flashlight.transform.position.x, flashlight.transform.position.y, 0, 1.0f));
                collectibleMaterial.SetFloat("_FlashlightRange", flashlight.pointLightOuterRadius);
                collectibleMaterial.color = new Color(1, 1, 1, fadeFactor); 

                SetVisibility(fadeFactor > 0);
            }
        }


    }

    void SetVisibility(bool visible)
    {
        collectibleRenderer.enabled = visible;
        shadowCaster.enabled = visible;
        collectibleCollider.enabled = visible; 
    }

    bool IsWithinFlashlightCone()
    {
        Vector3 directionToCollectible = transform.position - flashlight.transform.position;
        float angle = Vector3.Angle(flashlight.transform.up, directionToCollectible);
        return angle <= flashlight.pointLightOuterAngle / 2.0f;
    }

    bool IsBlockedByWall()
    {
        Vector3 flashlightPos = flashlight.transform.position;
        Vector3 directionToCollectible = (transform.position - flashlightPos).normalized;
        float distance = Vector3.Distance(flashlightPos, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(flashlightPos, directionToCollectible, distance, LayerMask.GetMask("Wall"));
        return hit.collider != null; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isVisible)
        {
            GameHandler.Instance.emittersGotten++;
            EmitterSoundController sound = emitterPickupSound.GetComponent<EmitterSoundController>();
            sound.EmitPickupSound();

            Destroy(gameObject);
            Debug.Log($"Emmiter picked up! Total emmitters in inventory: {GameHandler.Instance.emittersGotten}");
        }
    }
}
