using UnityEngine;

public class RewindToController : MonoBehaviour
{
    public static RewindToController Instance;

    public GameObject markerPrefab;

    private GameObject currentMarker;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnMarker(Vector2 position)
    {
        ClearMarker();
        currentMarker = Instantiate(markerPrefab, position, Quaternion.identity);
    }

    public void ClearMarker()
    {   
        if (currentMarker != null)
            Destroy(currentMarker);
    }
}