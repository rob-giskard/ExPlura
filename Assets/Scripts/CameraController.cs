using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public static CameraTransition Instance;
    public Camera mainCamera;
    private bool isTransitioning = false;
    public float transitionSpeed = 2.0f; // Adjust for smoothness
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void MoveCamera(Vector3 newRoomPosition)
    {
        if (!isTransitioning)
        {
            StartCoroutine(SmoothMove(newRoomPosition));
        }
    }

    private IEnumerator SmoothMove(Vector3 targetPosition)
    {
        isTransitioning = true;

        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // Ensure final position is exact
        isTransitioning = false;
    }
}
