using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    // Slide doors by changing its Vector3 x value between closed and open
    public bool isUnlocked = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    public Vector3 openPositionOffset; // Eg (3, 0, 0) moves to right
    private bool isOpen = false;
    private bool isSliding = false;
    public float slideSpeed = 2.0f;

    void Start()
    {
        // Closed pos is currecnt pos
        // Calculated target pos as current + offset
        closedPosition = transform.position;
        openPosition = closedPosition + openPositionOffset;
    }

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            // ToggleDoor(); 
        }
    }

    public void ToggleDoor()
    {
        if (!isUnlocked || isSliding) return;
        // ako argument das odpoved na obe otazky
        StartCoroutine(SlideDoor(isOpen ? closedPosition : openPosition));
        isOpen = !isOpen;
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        isSliding = true;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }

        transform.position = targetPosition;
        isSliding = false;
    }
}
