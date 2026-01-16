using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum EnemyState { Patrolling, Chasing, Returning }

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public float chaseSpeed = 3f;
    public float detectionRange = 5f;
    public int damage = 1; // read this by player controller later
    public Transform player;
    public GameObject flashlight; 

    private int currentPatrolIndex = 0;
    private EnemyState currentState = EnemyState.Patrolling;
    public bool interestedInPlayer = true;

    private Rigidbody2D entity;

    void Start()
    {
        entity = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                CheckForPlayer();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Returning:
                ReturnToPatrol();
                break;
        }
    }


    void CheckForPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);


        if (distance > detectionRange && interestedInPlayer)
        {
            FlashlightController flashControllerScript = flashlight.GetComponent<FlashlightController>();
            if (flashControllerScript.turnedOnState)
            {
                currentState = EnemyState.Chasing;
            }
        }
    }

    public void Patrol()
    {
        MoveTowards(patrolPoints[currentPatrolIndex].position, moveSpeed);

        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.01f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position); // Calculate target position
        FlashlightController flashlightControllerScript = flashlight.GetComponent<FlashlightController>();

        if (distance < detectionRange)
        { 
            if (flashlightControllerScript.turnedOnState == false)
            {
                currentState = EnemyState.Returning;
                Debug.Log($"Returning to patrol, dist to player: {distance}");
                return;
            }
        }
        MoveTowards(player.position, chaseSpeed);
        BounceBack();
            
    }

    void BounceBack()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Bounce from player, set player ignore cooldown, deal damage to player
        if (distance < 0.1) 
        {
            PlayerController HPscript = player.GetComponent<PlayerController>();
            HPscript.HP--;
            Debug.Log($"Player was hit and has {HPscript.HP} HP after the collision");
            Patrol();
            Debug.Log($"Returning to patrol after player collision, on cooldown.");
            // entity will ignore player while on cooldown and then reset interested status
            StartCoroutine(PlayerIgnoreCooldown(10));
        }
    }

    void ReturnToPatrol()
    {
        Vector2 target = patrolPoints[currentPatrolIndex].position;
        MoveTowards(target, moveSpeed);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentState = EnemyState.Patrolling;
        }
    }
    IEnumerator PlayerIgnoreCooldown(int forSeconds)
    {
        int i = 0;
        interestedInPlayer = false;

        while (i < forSeconds)
        {
            i++;
            Debug.Log($"Ignoring player for {i}");
            yield return new WaitForSeconds(1);
        }
        Debug.Log("### Ignore period ended. ###");
        interestedInPlayer = true;

    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        entity.linearVelocity = direction * speed;
    }
}


