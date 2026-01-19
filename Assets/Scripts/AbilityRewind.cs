using UnityEngine;
using UnityEngine.Rendering;

public class AbilityRewind : MonoBehaviour
{
    Vector2 rewindToPosition;
    bool pointSet = false;
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            setRewindPoint();  
        }
        if (Input.GetKeyDown(KeyCode.H) && pointSet)
        {
            rewindToPoint();
            
        }
    }

    void setRewindPoint()
    {
        rewindToPosition = transform.position;
        pointSet = true;
        RewindToController.Instance.SpawnMarker(rewindToPosition);

        Debug.Log("Position saved at: " + rewindToPosition);
    }

    void rewindToPoint()
    {
        if (!pointSet) return;
        Debug.Log("Leaving position at: " + rb.position);
        rb.position = rewindToPosition;
        pointSet = false;
        RewindToController.Instance.ClearMarker();
    }
}
