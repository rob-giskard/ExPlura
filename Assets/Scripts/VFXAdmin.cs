using UnityEngine;

public class VFXAdmin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject vfxPrefab;
    public GameObject vfxPrefab1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(vfxPrefab, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Instantiate(vfxPrefab1, transform.position, Quaternion.identity);
        }
    }

}

