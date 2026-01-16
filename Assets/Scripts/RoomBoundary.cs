using UnityEngine;

public class RoomBoundary : MonoBehaviour
   
{
    public Transform newCameraPosition; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            CameraTransition.Instance.MoveCamera(newCameraPosition.position);
        }
    }
}

