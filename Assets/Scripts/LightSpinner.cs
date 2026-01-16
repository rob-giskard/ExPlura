using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform player;

    public float spinSpeed;
    public bool isSpinning = false;
    public bool isActive = false;

    private bool playerInRange;
    public float activeRange = 3.0f; // Adjust for eye to see further


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsPlayerNear();

        if (isActive && isSpinning && playerInRange)
        {
            {
                Vector3 playerPosition = player.position;
                Vector2 direction = (playerPosition - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
    }

    void IsPlayerNear()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            playerInRange = distanceToPlayer < activeRange; 
        }
    }
}
