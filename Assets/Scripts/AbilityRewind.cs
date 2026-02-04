using UnityEngine;
using UnityEngine.Rendering;

public class AbilityRewind : MonoBehaviour
{
    Vector2 rewindToPosition;
    bool pointSet = false;
    public Rigidbody2D rb;
    public Rigidbody2D manualPlaced;
    public GameObject unlockObject;

    void Start()
    {
        rewindToPosition = manualPlaced.position;
        pointSet = true;
        RewindToController.Instance.SpawnMarker(rewindToPosition);

        Debug.Log("Position saved at: " + rewindToPosition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetRewindPoint();  
        }
        if (Input.GetKeyDown(KeyCode.Space) && pointSet)
        {
            RewindToPoint();
            
        }
    }

    void SetRewindPoint()
    {
        rewindToPosition = transform.position;
        pointSet = true;
        RewindToController.Instance.SpawnMarker(rewindToPosition);

        Debug.Log("Position saved at: " + rewindToPosition);
    }

    void RewindToPoint()
    {
        if ((!pointSet) || (unlockObject != null)) return;
        Debug.Log("Leaving position at: " + rb.position);
        rb.position = rewindToPosition;
        pointSet = false;
        RewindToController.Instance.ClearMarker();
    }
}
