using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

public class AbilityRewind : MonoBehaviour
{
    Vector2 rewindToPosition;
    bool pointSet = false;
    public Rigidbody2D rb;
    public Rigidbody2D manualPlaced;
    public GameObject unlockObject;

    [Header("VFX")]
    public GameObject startVFX;

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
        if (startVFX)
            Instantiate(startVFX, transform.position, Quaternion.identity);
        rb.position = rewindToPosition;
        pointSet = false;
        RewindToController.Instance.ClearMarker();
    }
}
