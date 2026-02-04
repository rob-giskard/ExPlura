using UnityEngine;

public class BlinkAbility : MonoBehaviour
{
    [Header("Blink Settings")]
    public float blinkDistance = 2f;
    public float cooldown = 0.25f;
    public LayerMask obstacleMask;

    [Header("VFX")]
    public GameObject startVFX;
    public GameObject endVFX;

    float lastBlinkTime;

    public bool TryBlink(Vector2 direction)
    {
        if (Time.time < lastBlinkTime + cooldown)
            return false;

        if (direction.sqrMagnitude < 0.01f)
            return false;

        Vector2 origin = transform.position;
        direction.Normalize();

        float distance = blinkDistance;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            blinkDistance,
            obstacleMask
        );

        if (hit.collider != null)
        {
            distance = hit.distance - 0.05f; 
            if (distance <= 0f)
                return false;
        }

        Vector2 target = origin + direction * distance;

        if (startVFX)
            Instantiate(startVFX, origin, Quaternion.identity);

        transform.position = target;

        if (endVFX)
            Instantiate(endVFX, target, Quaternion.identity);

        lastBlinkTime = Time.time;
        return true;
    }
}